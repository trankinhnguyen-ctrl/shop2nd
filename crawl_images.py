#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Crawl clothing images via Flickr public feed and populate QuanLyKho.db.
No API key required.

Requirements:
    pip install requests

Usage:
    python crawl_images.py
"""

import hashlib
import os
import re
import sqlite3
import sys
import time
import uuid
from pathlib import Path

import requests

if sys.stdout.encoding and sys.stdout.encoding.lower() != "utf-8":
    sys.stdout.reconfigure(encoding="utf-8", errors="replace")
if sys.stderr.encoding and sys.stderr.encoding.lower() != "utf-8":
    sys.stderr.reconfigure(encoding="utf-8", errors="replace")

# ── Config ────────────────────────────────────────────────────────────────────
DB_PATH    = r"D:\Dev\vtc\2Hand\shop2nd\dosi\bin\Debug\net8.0-windows\QuanLyKho.db"
IMAGES_DIR = r"D:\Dev\vtc\2Hand\shop2nd\dosi\bin\Debug\net8.0-windows\Images"
BASE_DIR   = r"D:\Dev\vtc\2Hand\shop2nd\dosi\bin\Debug\net8.0-windows"
DELAY      = 1.2

# ── Tag map for existing products (ma_sp → Flickr tags) ──────────────────────
# Keep to 2-3 common tags — Flickr AND-matches all of them, so fewer = more results.
EXISTING_TAGS: dict[str, str] = {
    "AT-001": "tshirt,vintage",
    "AT-002": "tshirt,streetwear",
    "HD-001": "hoodie,streetwear",
    "JK-001": "varsity,jacket",
    "SM-001": "flannel,shirt",
    "QN-001": "jeans,streetwear",
    "AT-003": "tshirt,white",
    "HD-002": "hoodie,zip",
}

# ── New streetwear / secondhand products ──────────────────────────────────────
# (MaSP, TenSP, GiaBan VND, SoLuong, Flickr tags — max 3, common English words)
NEW_PRODUCTS = [
    ("SW001", "Áo Hoodie Oversize Vintage",        250_000, 1, "hoodie,vintage"),
    ("SW002", "Áo Thun Graphic Tee Y2K",            120_000, 2, "tshirt,graphic"),
    ("SW003", "Quần Cargo Baggy",                   320_000, 1, "cargo,pants"),
    ("SW004", "Áo Bomber Jacket Cổ Điển",           450_000, 1, "bomber,jacket"),
    ("SW005", "Áo Flannel Lumber Jack",             180_000, 2, "flannel,shirt"),
    ("SW006", "Quần Jeans Wide Leg",                280_000, 1, "jeans,streetwear"),
    ("SW007", "Áo Varsity Jacket USA",              520_000, 1, "varsity,jacket"),
    ("SW008", "Áo Tank Top Gym",                     90_000, 2, "tank,top"),
    ("SW009", "Áo Denim Jacket Wash Trắng",         380_000, 1, "denim,jacket"),
    ("SW010", "Áo Crewneck Sweatshirt",             200_000, 2, "crewneck,sweatshirt"),
    ("SW011", "Quần Short Cargo",                   150_000, 2, "cargo,shorts"),
    ("SW012", "Áo Zip Hoodie Boxy",                 300_000, 1, "hoodie,streetwear"),
    ("SW013", "Áo Polo Retro 90s",                  160_000, 2, "polo,shirt"),
    ("SW014", "Quần Track Pants Jogger",            220_000, 1, "track,pants"),
    ("SW015", "Áo Coach Jacket Windbreaker",        400_000, 1, "windbreaker,jacket"),
    ("SW016", "Áo Flannel Overshirt Caro",          200_000, 2, "flannel,overshirt"),
    ("SW017", "Quần Jean Baggy Rách",               350_000, 1, "jeans,ripped"),
    ("SW018", "Áo Thun Trắng Basic Heavyweight",     80_000, 2, "tshirt,white"),
    ("SW019", "Áo Corduroy Button-Up",              260_000, 1, "corduroy,shirt"),
    ("SW020", "Áo Puffer Vest",                     340_000, 1, "puffer,vest"),
]

# Combined tag lookup: existing products + new products
ALL_TAGS: dict[str, str] = {
    **EXISTING_TAGS,
    **{ma: tags for ma, _, _, _, tags in NEW_PRODUCTS},
}

S = requests.Session()
S.headers.update({
    "User-Agent": (
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) "
        "AppleWebKit/537.36 (KHTML, like Gecko) "
        "Chrome/124.0.0.0 Safari/537.36"
    ),
})


# ── Flickr public feed (no API key) ──────────────────────────────────────────
def _flickr_query(tags: str, tagmode: str = "all") -> list[str]:
    """Single Flickr feed request. tagmode='all' (AND) or 'any' (OR)."""
    urls: list[str] = []
    try:
        r = S.get(
            "https://api.flickr.com/services/feeds/photos_public.gne",
            params={"tags": tags, "tagmode": tagmode, "format": "json", "nojsoncallback": "1"},
            timeout=10,
        )
        r.raise_for_status()
        for item in r.json().get("items", []):
            thumb = item.get("media", {}).get("m", "")
            if thumb:
                large = thumb.replace("_m.", "_b.")
                if large not in urls:
                    urls.append(large)
    except Exception as e:
        print(f"    [Flickr] {e}")
    return urls


def search_flickr(tags: str, limit: int = 20) -> list[str]:
    """
    Query Flickr's public photo feed by comma-separated English tags.
    Tries AND (all tags) first; falls back to OR (any tag) if empty.
    Returns _b (large) image URLs. Free, no API key required.
    """
    urls = _flickr_query(tags, tagmode="all")
    if not urls:
        # Fall back: match photos that have ANY of the tags
        urls = _flickr_query(tags, tagmode="any")
    if not urls:
        # Last resort: just the first tag alone
        first_tag = tags.split(",")[0]
        urls = _flickr_query(first_tag + ",fashion", tagmode="any")
    return urls[:limit]


# ── Download ──────────────────────────────────────────────────────────────────
def download_image(url: str) -> str | None:
    """Download url to IMAGES_DIR. Returns 'Images\\uuid.ext' or None."""
    os.makedirs(IMAGES_DIR, exist_ok=True)
    try:
        r = S.get(url, timeout=20, stream=True)
        r.raise_for_status()
        ct = r.headers.get("Content-Type", "")
        if "text" in ct or "html" in ct:
            return None
        ext = ".png" if "png" in ct else ".webp" if "webp" in ct else ".jpg"
        filename = f"{uuid.uuid4()}{ext}"
        full_path = os.path.join(IMAGES_DIR, filename)
        with open(full_path, "wb") as f:
            for chunk in r.iter_content(8192):
                f.write(chunk)
        if os.path.getsize(full_path) < 5_000:
            os.remove(full_path)
            return None
        return os.path.join("Images", filename)
    except Exception as e:
        print(f"    [Download] {url[:70]}: {e}")
        return None


def fetch_image(tags: str) -> str | None:
    """Search Flickr by tags and download the first valid image."""
    print(f"    ↳ Flickr tags: [{tags}]")
    for url in search_flickr(tags):
        path = download_image(url)
        if path:
            return path
        time.sleep(0.15)
    print("    ✗ No image found")
    return None


# ── Detect and remove duplicate/bad images ───────────────────────────────────
def cleanup_duplicate_images(conn: sqlite3.Connection) -> int:
    """
    Any image shared byte-for-byte by 4+ products is a bot-trap promo image.
    Delete those files and clear the DB path so they get re-downloaded.
    """
    rows = conn.execute("SELECT id, hinh_anh FROM SanPham WHERE hinh_anh != ''").fetchall()
    hash_map: dict[str, list[tuple[int, str, str]]] = {}
    for pid, img_rel in rows:
        full = os.path.join(BASE_DIR, img_rel.replace("/", os.sep))
        if not os.path.exists(full):
            continue
        with open(full, "rb") as f:
            h = hashlib.md5(f.read()).hexdigest()
        hash_map.setdefault(h, []).append((pid, img_rel, full))

    cleaned = 0
    for h, items in hash_map.items():
        if len(items) >= 4:
            print(f"  Bad image (md5={h[:8]}, repeated {len(items)}x) — removing...")
            for pid, _, full_path in items:
                try:
                    os.remove(full_path)
                except OSError:
                    pass
                conn.execute("UPDATE SanPham SET hinh_anh='' WHERE id=?", (pid,))
                cleaned += 1
            conn.commit()
    return cleaned


# ── Re-fetch missing images ───────────────────────────────────────────────────
def fix_missing_images(conn: sqlite3.Connection) -> None:
    rows = conn.execute("SELECT id, ma_sp, ten_sp, hinh_anh FROM SanPham").fetchall()
    broken = []
    for pid, ma, ten, img in rows:
        if not img:
            broken.append((pid, ma, ten))
            continue
        full = os.path.join(BASE_DIR, img.replace("/", os.sep))
        if not os.path.exists(full):
            broken.append((pid, ma, ten))

    if not broken:
        print("  All products have valid images.")
        return

    print(f"  {len(broken)} product(s) need images.")
    for pid, ma, ten in broken:
        print(f"\n  → {ma} | {ten}")
        # Use known tag map first; fall back to extracting English words from name
        tags = ALL_TAGS.get(ma) or _name_to_tags(ten)
        path = fetch_image(tags)
        if path:
            conn.execute("UPDATE SanPham SET hinh_anh=? WHERE id=?", (path, pid))
            conn.commit()
            print(f"    ✓ {path}")
        else:
            print("    ✗ skipped")
        time.sleep(DELAY)


def _name_to_tags(name: str) -> str:
    """Extract English words from a (possibly Vietnamese) product name for Flickr tags."""
    english_words = re.findall(r"[A-Za-z]{3,}", name)
    stop = {"the", "and", "for", "with", "from", "that", "this", "are", "was"}
    tags = [w.lower() for w in english_words if w.lower() not in stop][:4]
    if "streetwear" not in tags:
        tags.append("streetwear")
    return ",".join(tags) if tags else "streetwear,fashion,clothing"


# ── Insert new products ───────────────────────────────────────────────────────
def insert_new_products(conn: sqlite3.Connection) -> None:
    existing = {r[0] for r in conn.execute("SELECT ma_sp FROM SanPham").fetchall()}
    to_add   = [p for p in NEW_PRODUCTS if p[0] not in existing]

    if not to_add:
        print("  All new products already exist.")
        return

    print(f"  {len(to_add)} new product(s) to insert.")
    for ma, ten, gia, sl, tags in to_add:
        print(f"\n  + {ma} | {ten}")
        path = fetch_image(tags)
        conn.execute(
            "INSERT INTO SanPham (ma_sp, ten_sp, so_luong_ton, gia_ban, hinh_anh) VALUES (?,?,?,?,?)",
            (ma, ten, sl, gia, path or ""),
        )
        conn.commit()
        print(f"    {'✓ ' + path if path else '✗ inserted without image'}")
        time.sleep(DELAY)


# ── Main ──────────────────────────────────────────────────────────────────────
def main() -> None:
    sep = "─" * 60
    print(sep)
    print("  Shop Image Crawler  (source: Flickr)")
    print(sep)

    Path(IMAGES_DIR).mkdir(parents=True, exist_ok=True)
    conn = sqlite3.connect(DB_PATH)

    print("\n[Step 1] Removing duplicate / bad images from previous run...")
    n = cleanup_duplicate_images(conn)
    print(f"  Cleaned {n} bad image(s)." if n else "  Nothing to clean.")

    print("\n[Step 2] Fetching images for products that need them...")
    fix_missing_images(conn)

    print("\n[Step 3] Inserting new streetwear products...")
    insert_new_products(conn)

    conn.close()
    print(f"\n{sep}")
    print("  Done! Restart the shop app to see updated images.")
    print(sep)


if __name__ == "__main__":
    main()
