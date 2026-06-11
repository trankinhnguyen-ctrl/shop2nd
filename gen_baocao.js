#!/usr/bin/env node
'use strict';
const DOCX_PATH = '/usr/local/lib/node_modules_global/lib/node_modules/docx';
const {
  Document, Packer, Paragraph, TextRun, Table, TableRow, TableCell,
  AlignmentType, BorderStyle, WidthType, ShadingType, VerticalAlign,
  PageNumber, PageBreak, Footer, SectionType, UnderlineType
} = require(DOCX_PATH);
const fs = require('fs');

// ─── CONSTANTS ───────────────────────────────────────────────────────────────
const F = "Times New Roman";
const SZ = 26;          // 13pt
const SZ_CH = 32;       // 16pt chapter
const W = 8787;         // content width DXA (A4, margins: L=1985 R=1134 T=1134 B=1418)
const SP15 = { line: 360, lineRule: "auto" };
const SP_PARA  = { before: 0,   after: 80,  ...SP15 };
const SP_CH    = { before: 300, after: 180, ...SP15 };
const SP_H2    = { before: 200, after: 100, ...SP15 };
const SP_H3    = { before: 140, after: 60,  ...SP15 };
const SP_SMALL = { before: 40,  after: 40,  line: 300, lineRule: "auto" };
const CMARG = { top: 80, bottom: 80, left: 120, right: 120 };
const FILL_HDR = "BDD7EE";
const FILL_LABEL = "F2F2F2";

const BORDER1 = { style: BorderStyle.SINGLE, size: 6, color: "000000" };
const BORDS  = { top: BORDER1, bottom: BORDER1, left: BORDER1, right: BORDER1 };

// ─── HELPERS ─────────────────────────────────────────────────────────────────
const run = (text, {bold=false,italic=false,underline=false,size=SZ,color}={}) =>
  new TextRun({ text, font: F, size, bold, italics: italic,
    underline: underline ? { type: UnderlineType.SINGLE } : undefined, color });

const para = (text, {bold=false,italic=false,align=AlignmentType.JUSTIFIED,
  spacing=SP_PARA,indent={firstLine:720},size=SZ,underline=false}={}) =>
  new Paragraph({ alignment: align, spacing, indent,
    children: [run(text,{bold,italic,underline,size})] });

const paraRuns = (runs, {align=AlignmentType.JUSTIFIED,spacing=SP_PARA,indent={firstLine:720}}={}) =>
  new Paragraph({ alignment: align, spacing, indent, children: runs });

const blank = () => new Paragraph({ spacing: SP_PARA, children: [run("")] });

const pageBreak = () => new Paragraph({ children: [new PageBreak()] });

// Chapter heading: CHƯƠNG N. TITLE  — 16pt bold centered
const chH = (n, t) => new Paragraph({ alignment: AlignmentType.CENTER, spacing: SP_CH, indent:{left:0},
  children:[run(`CHƯƠNG ${n}. ${t}`,{bold:true,size:SZ_CH})] });

// h2: X.Y Title — 13pt bold
const h2 = (n, t) => new Paragraph({ spacing: SP_H2, indent:{left:0},
  children:[run(`${n} ${t}`,{bold:true})] });

// h3: X.Y.Z Title — 13pt bold+italic
const h3 = (n, t) => new Paragraph({ spacing: SP_H3, indent:{left:0},
  children:[run(`${n} ${t}`,{bold:true,italic:true})] });

// h4: X.Y.Z.W Title — 13pt underline
const h4 = (n, t) => new Paragraph({ spacing: SP_H3, indent:{left:0},
  children:[run(`${n} ${t}`,{underline:true})] });

// Placeholder for images
const imgPlaceholder = (code, title) => [
  new Paragraph({ alignment: AlignmentType.CENTER, spacing:{before:120,after:0,...SP15}, indent:{left:0},
    children:[run(`[ Bổ sung hình ảnh tại đây ]`,{italic:true,color:"888888"})] }),
  new Paragraph({ alignment: AlignmentType.CENTER, spacing:{before:60,after:160,...SP15}, indent:{left:0},
    children:[run(`${code}: ${title}`,{italic:true})] }),
];

// Table cell helpers
const cell = (text, {bold=false,italic=false,shade=null,w=null,vAlign=VerticalAlign.CENTER,sp=SP_SMALL,align=AlignmentType.LEFT}={}) =>
  new TableCell({
    borders: BORDS, margins: CMARG, verticalAlign: vAlign,
    width: w ? {size:w,type:WidthType.DXA} : undefined,
    shading: shade ? {fill:shade,type:ShadingType.CLEAR} : undefined,
    children: [new Paragraph({alignment:align, spacing:sp, indent:{left:0},
      children:[run(text,{bold,italic})]})],
  });

const cellLines = (lines, {shade=null,w=null}={}) =>
  new TableCell({
    borders: BORDS, margins: CMARG, verticalAlign: VerticalAlign.TOP,
    width: w ? {size:w,type:WidthType.DXA} : undefined,
    shading: shade ? {fill:shade,type:ShadingType.CLEAR} : undefined,
    children: lines.map((line,i) => new Paragraph({
      alignment: AlignmentType.LEFT, spacing:{...SP_SMALL,before:i===0?60:20,after:i===lines.length-1?60:20},
      indent:{left:0}, children:[run(line)]})),
  });

const tbl = (colWidths, rows) => new Table({
  width:{size:W,type:WidthType.DXA}, columnWidths: colWidths, rows,
});

const caption = (text) => new Paragraph({ alignment: AlignmentType.CENTER,
  spacing:{before:60,after:160,...SP15}, indent:{left:0},
  children:[run(text,{italic:true})] });

// Standard table with header
const stdTable = (headers, rows, colW) => tbl(colW, [
  new TableRow({ children: headers.map((h,i) => cell(h,{bold:true,shade:FILL_HDR,w:colW[i]})) }),
  ...rows.map(row => new TableRow({ children: row.map((c,i) => cell(c,{w:colW[i]})) }))
]);

// Use Case table (2-col: label | content)
const ucTable = (ucNum, ucName, actor, pre, mainFlow, altFlow, post) => {
  const LW = 2100, CW = 6687;
  const rLabel = (label, content, shadeLabel=true) => new TableRow({ children: [
    cell(label,{bold:true,shade:shadeLabel?FILL_LABEL:null,w:LW}),
    cell(content,{w:CW}),
  ]});
  const rLines = (label, lines) => new TableRow({ children: [
    cell(label,{bold:true,shade:FILL_LABEL,w:LW}),
    cellLines(lines,{w:CW}),
  ]});
  return tbl([LW,CW], [
    new TableRow({ children: [
      cell("Tên Use Case",{bold:true,shade:FILL_HDR,w:LW}),
      cell(ucName,{bold:true,shade:FILL_HDR,w:CW}),
    ]}),
    rLabel("Actor", actor),
    rLabel("Tiền điều kiện", pre),
    rLines("Luồng sự kiện chính", mainFlow),
    rLines("Luồng ngoại lệ", altFlow),
    rLabel("Hậu điều kiện", post),
  ]);
};

// DB table: STT | Tên cột | Kiểu dữ liệu | Ràng buộc | Mô tả
const dbTable = (tableName, rows) => {
  const colW = [500, 1900, 1500, 1687, 3200];
  const hdr = ["STT","Tên cột","Kiểu dữ liệu","Ràng buộc","Mô tả"];
  return stdTable(hdr, rows, colW);
};

// Bullet list
const bullet = (items, indent=360) => items.map((item,i) =>
  new Paragraph({ spacing: SP_PARA, indent:{left:indent+360,hanging:360},
    children:[run(`•  ${item}`)] }));

const numbered = (items, indent=0) => items.map((item,i) =>
  new Paragraph({ spacing: SP_PARA, indent:{left:indent+360,hanging:360},
    children:[run(`${i+1}.  ${item}`)] }));

// ─── COVER PAGE ──────────────────────────────────────────────────────────────
const buildCover = () => [
  new Paragraph({alignment:AlignmentType.CENTER, spacing:{before:0,after:40,line:300,lineRule:"auto"}, indent:{left:0},
    children:[run("HỌC VIỆN CÔNG NGHỆ THÔNG TIN VÀ THIẾT KẾ VTC",{bold:true,size:24})]}),
  new Paragraph({alignment:AlignmentType.CENTER, spacing:{before:0,after:200,line:300,lineRule:"auto"}, indent:{left:0},
    children:[run("─────────────────────────────",{size:22,color:"888888"})]}),
  ...blank2(3),
  new Paragraph({alignment:AlignmentType.CENTER, spacing:{before:60,after:60,...SP15}, indent:{left:0},
    children:[run("BÁO CÁO ĐỒ ÁN CUỐI KỲ",{bold:true,size:28})]}),
  new Paragraph({alignment:AlignmentType.CENTER, spacing:{before:0,after:60,...SP15}, indent:{left:0},
    children:[run("Module: Thiết Kế Dự Án và Lập Trình App WinForm",{italic:true,size:24})]}),
  ...blank2(2),
  new Paragraph({alignment:AlignmentType.CENTER, spacing:{before:60,after:60,...SP15}, indent:{left:0},
    children:[run("ĐỀ TÀI:",{bold:true,size:SZ_CH})]}),
  new Paragraph({alignment:AlignmentType.CENTER, spacing:{before:0,after:200,...SP15}, indent:{left:0},
    children:[run("XÂY DỰNG PHẦN MỀM",{bold:true,size:SZ_CH+4})]}),
  new Paragraph({alignment:AlignmentType.CENTER, spacing:{before:0,after:60,...SP15}, indent:{left:0},
    children:[run("QUẢN LÝ SHOP ĐỒ SECONDHAND",{bold:true,size:SZ_CH+4})]}),
  ...blank2(4),
  new Paragraph({alignment:AlignmentType.LEFT, spacing:{before:60,after:40,...SP15}, indent:{left:3600},
    children:[run("Sinh viên thực hiện:",{bold:true})]}),
  new Paragraph({alignment:AlignmentType.LEFT, spacing:{before:0,after:20,...SP15}, indent:{left:4200},
    children:[run("............................................")]}),
  new Paragraph({alignment:AlignmentType.LEFT, spacing:{before:20,after:40,...SP15}, indent:{left:3600},
    children:[run("MSSV:  ............................................")]}),
  new Paragraph({alignment:AlignmentType.LEFT, spacing:{before:40,after:40,...SP15}, indent:{left:3600},
    children:[run("Lớp:     ............................................")]}),
  new Paragraph({alignment:AlignmentType.LEFT, spacing:{before:40,after:40,...SP15}, indent:{left:3600},
    children:[run("Giảng viên hướng dẫn:",{bold:true})]}),
  new Paragraph({alignment:AlignmentType.LEFT, spacing:{before:0,after:20,...SP15}, indent:{left:4200},
    children:[run("............................................")]}),
  ...blank2(3),
  new Paragraph({alignment:AlignmentType.CENTER, spacing:{before:60,after:0,...SP15}, indent:{left:0},
    children:[run("TP. Hồ Chí Minh, tháng 6 năm 2026",{italic:true})]}),
];

const blank2 = (n=1) => Array.from({length:n},()=>blank());

// ─── FRONT MATTER PAGES ───────────────────────────────────────────────────────
const buildThanks = () => [
  new Paragraph({alignment:AlignmentType.CENTER, spacing:SP_CH, indent:{left:0},
    children:[run("LỜI CẢM ƠN",{bold:true,size:SZ_CH})]}),
  para("Trong quá trình thực hiện đồ án, tôi đã nhận được sự hỗ trợ và hướng dẫn tận tình từ nhiều phía. Tôi xin gửi lời cảm ơn chân thành đến:"),
  para("Quý thầy cô tại Học viện Công nghệ Thông tin và Thiết kế VTC đã truyền đạt kiến thức chuyên ngành và tạo điều kiện thuận lợi để tôi hoàn thành đồ án này."),
  para("Giảng viên hướng dẫn đã dành thời gian đọc, góp ý và định hướng cho tôi trong suốt quá trình thực hiện."),
  para("Mặc dù đã cố gắng hết sức, báo cáo này vẫn có thể còn nhiều thiếu sót. Tôi rất mong nhận được sự góp ý của quý thầy cô để hoàn thiện hơn."),
  para("Tôi xin chân thành cảm ơn!"),
  ...blank2(2),
  new Paragraph({alignment:AlignmentType.RIGHT, spacing:SP_PARA, indent:{left:0},
    children:[run("TP. Hồ Chí Minh, tháng 6 năm 2026")]}),
  new Paragraph({alignment:AlignmentType.RIGHT, spacing:SP_PARA, indent:{left:0},
    children:[run("Sinh viên thực hiện",{bold:true})]}),
  ...blank2(3),
  new Paragraph({alignment:AlignmentType.RIGHT, spacing:SP_PARA, indent:{left:0},
    children:[run("............................................")]}),
];

const buildCommitment = () => [
  new Paragraph({alignment:AlignmentType.CENTER, spacing:SP_CH, indent:{left:0},
    children:[run("LỜI CAM KẾT",{bold:true,size:SZ_CH})]}),
  para("Tôi xin cam đoan đây là công trình nghiên cứu của riêng tôi. Các nội dung được trình bày trong báo cáo này là trung thực và chưa được công bố trong bất kỳ công trình nào khác."),
  para("Nếu phát hiện có sự gian lận, tôi xin hoàn toàn chịu trách nhiệm về nội dung báo cáo của mình."),
  ...blank2(2),
  new Paragraph({alignment:AlignmentType.RIGHT, spacing:SP_PARA, indent:{left:0},
    children:[run("TP. Hồ Chí Minh, tháng 6 năm 2026")]}),
  new Paragraph({alignment:AlignmentType.RIGHT, spacing:SP_PARA, indent:{left:0},
    children:[run("Sinh viên thực hiện",{bold:true})]}),
  ...blank2(3),
  new Paragraph({alignment:AlignmentType.RIGHT, spacing:SP_PARA, indent:{left:0},
    children:[run("............................................")]}),
];

const buildReviewPage = (role) => [
  new Paragraph({alignment:AlignmentType.CENTER, spacing:SP_CH, indent:{left:0},
    children:[run(`NHẬN XÉT CỦA GIẢNG VIÊN ${role.toUpperCase()}`,{bold:true,size:SZ_CH})]}),
  para("Họ và tên sinh viên: ............................................................................   MSSV: ........................."),
  para("Tên đề tài: Xây Dựng Phần Mềm Quản Lý Shop Đồ Secondhand"),
  blank(),
  para("Nhận xét:"),
  ...Array.from({length:12}, ()=>
    new Paragraph({ spacing:{before:0,after:0,...SP15}, indent:{left:0},
      children:[run("............................................................................................................")] })),
  blank(),
  para("Điểm đánh giá: .............../ 10"),
  blank(),
  new Paragraph({alignment:AlignmentType.RIGHT, spacing:SP_PARA, indent:{left:0},
    children:[run("TP. Hồ Chí Minh, ngày .... tháng .... năm 2026")]}),
  new Paragraph({alignment:AlignmentType.RIGHT, spacing:SP_PARA, indent:{left:0},
    children:[run(`Giảng viên ${role}`,{bold:true})]}),
  ...blank2(3),
  new Paragraph({alignment:AlignmentType.RIGHT, spacing:SP_PARA, indent:{left:0},
    children:[run("............................................")]}),
];

// ─── TOC ─────────────────────────────────────────────────────────────────────
const tocEntry = (text, dotted=true) => new Paragraph({
  alignment: AlignmentType.JUSTIFIED, spacing: {before:0,after:40,...SP15}, indent:{left:0},
  children: [run(text)],
});
const tocH = (text) => new Paragraph({
  alignment: AlignmentType.JUSTIFIED, spacing:{before:80,after:40,...SP15}, indent:{left:0},
  children: [run(text,{bold:true})],
});

const buildTOC = () => [
  new Paragraph({alignment:AlignmentType.CENTER, spacing:SP_CH, indent:{left:0},
    children:[run("MỤC LỤC",{bold:true,size:SZ_CH})]}),
  tocH("LỜI CẢM ƠN"),
  tocH("LỜI CAM KẾT"),
  tocH("NHẬN XÉT CỦA GIẢNG VIÊN HƯỚNG DẪN"),
  tocH("NHẬN XÉT CỦA GIẢNG VIÊN PHẢN BIỆN"),
  tocH("DANH MỤC HÌNH VẼ"),
  tocH("DANH MỤC BẢNG BIỂU"),
  blank(),
  tocH("CHƯƠNG 1. TỔNG QUAN"),
  tocEntry("    1.1  Giới thiệu đề tài"),
  tocEntry("        1.1.1  Bối cảnh và lý do chọn đề tài"),
  tocEntry("        1.1.2  Mục tiêu đề tài"),
  tocEntry("        1.1.3  Phạm vi và giới hạn"),
  tocEntry("    1.2  Môi trường phát triển và công cụ sử dụng"),
  tocEntry("    1.3  Cấu trúc báo cáo"),
  blank(),
  tocH("CHƯƠNG 2. PHÂN TÍCH YÊU CẦU HỆ THỐNG"),
  tocEntry("    2.1  Đặc tả bài toán"),
  tocEntry("    2.2  Phân tích bài toán"),
  tocEntry("        2.2.1  Phát biểu bài toán"),
  tocEntry("        2.2.2  Yêu cầu chức năng"),
  tocEntry("        2.2.3  Yêu cầu phi chức năng"),
  tocEntry("    2.3  Mô hình Use Case"),
  tocEntry("        2.3.1  Sơ đồ Use Case tổng quát"),
  tocEntry("        2.3.2  Xác định Actor và danh sách Use Case"),
  tocEntry("    2.4  Đặc tả Use Case chi tiết"),
  tocEntry("        2.4.1  Thêm sản phẩm mới vào kho"),
  tocEntry("        2.4.2  Chỉnh sửa thông tin sản phẩm"),
  tocEntry("        2.4.3  Xóa sản phẩm"),
  tocEntry("        2.4.4  Tìm kiếm và lọc sản phẩm"),
  tocEntry("        2.4.5  Thêm / Chỉnh sửa thông tin khách hàng"),
  tocEntry("        2.4.6  Tra cứu khách hàng theo số điện thoại"),
  tocEntry("        2.4.7  Xem lịch sử mua hàng của khách hàng"),
  tocEntry("        2.4.8  Tạo đơn hàng mới (POS)"),
  tocEntry("        2.4.9  Áp dụng Voucher / Khuyến mãi"),
  tocEntry("        2.4.10 Thanh toán và hoàn tất đơn hàng"),
  tocEntry("        2.4.11 Chỉnh sửa đơn hàng đã hoàn thành"),
  tocEntry("        2.4.12 Xem Dashboard Tổng Quan"),
  tocEntry("        2.4.13 Phân tích doanh thu theo khoảng thời gian"),
  tocEntry("        2.4.14 Quản lý Voucher / Khuyến mãi"),
  tocEntry("    2.5  Mô hình hoạt động (Activity Diagram)"),
  tocEntry("        2.5.1  Sơ đồ hoạt động tổng quát"),
  tocEntry("        2.5.2  Luồng tạo đơn hàng và thanh toán"),
  tocEntry("        2.5.3  Luồng chỉnh sửa đơn hàng đã hoàn thành"),
  tocEntry("        2.5.4  Luồng áp dụng Voucher"),
  tocEntry("        2.5.5  Luồng khởi động ứng dụng (Migration & Crash Recovery)"),
  blank(),
  tocH("CHƯƠNG 3. THIẾT KẾ HỆ THỐNG"),
  tocEntry("    3.1  Thiết kế cơ sở dữ liệu"),
  tocEntry("        3.1.1  Sơ đồ ERD"),
  tocEntry("        3.1.2  Mô tả chi tiết các bảng"),
  tocEntry("    3.2  Mô hình lớp (Class Diagram)"),
  tocEntry("    3.3  Thiết kế giao diện"),
  tocEntry("        3.3.1  Màn hình Tổng Quan (Dashboard)"),
  tocEntry("        3.3.2  Màn hình Kho Hàng"),
  tocEntry("        3.3.3  Màn hình Khách Hàng"),
  tocEntry("        3.3.4  Màn hình Giao Dịch (POS)"),
  tocEntry("        3.3.5  Màn hình Phân Tích"),
  tocEntry("        3.3.6  Quản lý Khuyến Mãi"),
  blank(),
  tocH("CHƯƠNG 4. KẾT LUẬN VÀ HƯỚNG PHÁT TRIỂN"),
  tocEntry("    4.1  Kết quả đạt được"),
  tocEntry("    4.2  Hạn chế"),
  tocEntry("    4.3  Hướng phát triển"),
  blank(),
  tocH("TÀI LIỆU THAM KHẢO"),
  tocH("PHỤ LỤC"),
];

// ─── FIGURE & TABLE LISTS ────────────────────────────────────────────────────
const buildFigList = () => [
  new Paragraph({alignment:AlignmentType.CENTER, spacing:SP_CH, indent:{left:0},
    children:[run("DANH MỤC HÌNH VẼ",{bold:true,size:SZ_CH})]}),
  ...[
    "Hình 2.1: Sơ đồ Use Case tổng quát hệ thống",
    "Hình 2.2: Sơ đồ hoạt động tổng quát",
    "Hình 2.3: Sơ đồ hoạt động tạo đơn hàng và thanh toán",
    "Hình 2.4: Sơ đồ hoạt động chỉnh sửa đơn hàng đã hoàn thành",
    "Hình 2.5: Sơ đồ hoạt động áp dụng Voucher / Khuyến mãi",
    "Hình 2.6: Sơ đồ hoạt động khởi động ứng dụng (Migration & Crash Recovery)",
    "Hình 3.1: Sơ đồ ERD cơ sở dữ liệu",
    "Hình 3.2: Sơ đồ lớp (Class Diagram)",
    "Hình 3.3: Giao diện màn hình Tổng Quan (Dashboard)",
    "Hình 3.4: Giao diện màn hình Kho Hàng",
    "Hình 3.5: Giao diện màn hình Khách Hàng",
    "Hình 3.6: Giao diện màn hình Giao Dịch (POS)",
    "Hình 3.7: Giao diện màn hình Phân Tích",
    "Hình 3.8: Giao diện Quản lý Khuyến Mãi",
  ].map(t => new Paragraph({spacing:{before:40,after:40,...SP15}, indent:{left:0}, children:[run(t)]})),
];

const buildTblList = () => [
  new Paragraph({alignment:AlignmentType.CENTER, spacing:SP_CH, indent:{left:0},
    children:[run("DANH MỤC BẢNG BIỂU",{bold:true,size:SZ_CH})]}),
  ...[
    "Bảng 1.1: Môi trường phát triển và công cụ sử dụng",
    "Bảng 2.1: Yêu cầu chức năng của hệ thống",
    "Bảng 2.2: Danh sách Actor",
    "Bảng 2.3: Danh sách Use Case",
    "Bảng 2.4: Đặc tả Thêm sản phẩm mới vào kho",
    "Bảng 2.5: Đặc tả Chỉnh sửa thông tin sản phẩm",
    "Bảng 2.6: Đặc tả Xóa sản phẩm",
    "Bảng 2.7: Đặc tả Tìm kiếm và lọc sản phẩm",
    "Bảng 2.8: Đặc tả Thêm / Chỉnh sửa thông tin khách hàng",
    "Bảng 2.9: Đặc tả Tra cứu khách hàng theo số điện thoại",
    "Bảng 2.10: Đặc tả Xem lịch sử mua hàng của khách hàng",
    "Bảng 2.11: Đặc tả Tạo đơn hàng mới (POS)",
    "Bảng 2.12: Đặc tả Áp dụng Voucher / Khuyến mãi",
    "Bảng 2.13: Đặc tả Thanh toán và hoàn tất đơn hàng",
    "Bảng 2.14: Đặc tả Chỉnh sửa đơn hàng đã hoàn thành",
    "Bảng 2.15: Đặc tả Xem Dashboard Tổng Quan",
    "Bảng 2.16: Đặc tả Phân tích doanh thu theo khoảng thời gian",
    "Bảng 2.17: Đặc tả Quản lý Voucher / Khuyến mãi",
    "Bảng 3.1: Mô tả bảng SanPham",
    "Bảng 3.2: Mô tả bảng PhanLoai",
    "Bảng 3.3: Mô tả bảng Khach",
    "Bảng 3.4: Mô tả bảng HoaDon",
    "Bảng 3.5: Mô tả bảng NhapXuat",
    "Bảng 3.6: Mô tả bảng KhuyenMai",
    "Bảng 3.7: Mô tả bảng HoaDon_KhuyenMai",
  ].map(t => new Paragraph({spacing:{before:40,after:40,...SP15}, indent:{left:0}, children:[run(t)]})),
];

// ─── CHAPTER 1 ───────────────────────────────────────────────────────────────
const buildCh1 = () => [
  chH(1,"TỔNG QUAN"),
  h2("1.1","Giới thiệu đề tài"),
  h3("1.1.1","Bối cảnh và lý do chọn đề tài"),
  para("Trong những năm gần đây, thị trường đồ secondhand (hàng cũ, đồ sỉ) tại Việt Nam đang có sự tăng trưởng đáng kể, đặc biệt trong lĩnh vực thời trang. Xu hướng tiêu dùng bền vững và tiết kiệm chi phí thúc đẩy nhiều cửa hàng thời trang cũ ra đời, phần lớn là các shop quy mô nhỏ do một hoặc hai người vận hành."),
  para("Tuy nhiên, hầu hết các cửa hàng này vẫn đang quản lý hoạt động kinh doanh theo cách thủ công: ghi chép sổ tay, sử dụng bảng tính Excel rời rạc hoặc ứng dụng nhắn tin. Cách làm này dẫn đến nhiều hạn chế như dễ nhầm lẫn số lượng tồn kho, khó tra cứu lịch sử giao dịch, không có báo cáo doanh thu trực quan và không thể áp dụng chương trình khuyến mãi một cách hệ thống."),
  para("Từ thực tế đó, đề tài đề xuất xây dựng phần mềm \"Quản Lý Shop Đồ Secondhand\" – một ứng dụng desktop đơn giản, hoạt động hoàn toàn offline, được thiết kế phù hợp với quy mô cửa hàng nhỏ do một quản lý vận hành."),
  h3("1.1.2","Mục tiêu đề tài"),
  para("Mục tiêu chính của đề tài bao gồm:"),
  ...bullet([
    "Xây dựng phần mềm quản lý toàn diện cho cửa hàng đồ secondhand quy mô nhỏ.",
    "Hỗ trợ các nghiệp vụ cốt lõi: quản lý kho hàng, thông tin khách hàng, tạo đơn bán hàng theo dạng POS, quản lý chương trình khuyến mãi và phân tích doanh thu.",
    "Giao diện trực quan, dễ thao tác, phù hợp với người dùng không có chuyên môn kỹ thuật.",
    "Đảm bảo tính toàn vẹn dữ liệu: mọi giao dịch được thực thi trong transaction, hỗ trợ khôi phục khi ứng dụng bị tắt đột ngột.",
  ]),
  h3("1.1.3","Phạm vi và giới hạn"),
  para("Phần mềm được xây dựng trong phạm vi sau:"),
  ...bullet([
    "Ứng dụng desktop chạy trên hệ điều hành Windows.",
    "Một người dùng duy nhất (quản lý cửa hàng), không hỗ trợ đa người dùng đồng thời.",
    "Hoạt động hoàn toàn offline, toàn bộ dữ liệu lưu trên máy tính cục bộ.",
    "Không bao gồm chức năng thương mại điện tử, nhập hàng từ nhà cung cấp, hay in hóa đơn vật lý.",
  ]),
  h2("1.2","Môi trường phát triển và công cụ sử dụng"),
  stdTable(
    ["Thành phần","Phiên bản","Mô tả"],
    [
      ["Visual Studio 2022","17.x","Môi trường phát triển tích hợp (IDE) chính"],
      [".NET 8.0 SDK","8.0","Nền tảng phát triển ứng dụng Windows"],
      ["Windows Forms (WinForms)",".NET 8","Framework xây dựng giao diện desktop"],
      ["SQLite","3.x","Hệ quản trị cơ sở dữ liệu nhúng, không cần cài đặt riêng"],
      ["Dapper","2.x","Micro-ORM ánh xạ kết quả truy vấn SQL sang đối tượng C#"],
      ["ReaLTaiizor","Latest","Thư viện UI Controls mở rộng cho WinForms"],
      ["Microsoft.Data.Sqlite","8.x","Driver kết nối SQLite cho .NET"],
      ["DB Browser for SQLite","3.x","Công cụ xem và kiểm tra cơ sở dữ liệu SQLite"],
    ],
    [2500,1500,4787]
  ),
  caption("Bảng 1.1: Môi trường phát triển và công cụ sử dụng"),
  h2("1.3","Cấu trúc báo cáo"),
  para("Báo cáo được tổ chức thành 4 chương:"),
  ...bullet([
    "Chương 1 – Tổng Quan: Giới thiệu bối cảnh, mục tiêu, phạm vi và công cụ phát triển.",
    "Chương 2 – Phân Tích Yêu Cầu Hệ Thống: Đặc tả bài toán, mô hình Use Case, đặc tả chi tiết từng Use Case và mô hình hoạt động (Activity Diagram).",
    "Chương 3 – Thiết Kế Hệ Thống: Thiết kế cơ sở dữ liệu (ERD), mô hình lớp (Class Diagram) và thiết kế giao diện.",
    "Chương 4 – Kết Luận và Hướng Phát Triển: Tổng kết kết quả, hạn chế và định hướng mở rộng.",
  ]),
];

// ─── CHAPTER 2 ───────────────────────────────────────────────────────────────
const buildCh2 = () => [
  chH(2,"PHÂN TÍCH YÊU CẦU HỆ THỐNG"),
  h2("2.1","Đặc tả bài toán"),
  para("Cửa hàng đồ secondhand quy mô nhỏ thường do một người quản lý đảm nhận toàn bộ hoạt động: nhập hàng, bày bán, bán hàng trực tiếp và theo dõi doanh thu. Hiện tại, các nghiệp vụ này được thực hiện thủ công, gây ra các vấn đề sau:"),
  ...bullet([
    "Tồn kho không được theo dõi chính xác, dễ dẫn đến bán nhầm hàng hết hoặc bỏ sót sản phẩm.",
    "Không có hệ thống lưu trữ thông tin khách hàng và lịch sử mua hàng, khó chăm sóc khách thân thiết.",
    "Không có báo cáo doanh thu tự động, quản lý phải tổng hợp thủ công mất thời gian.",
    "Không thể triển khai chương trình khuyến mãi một cách nhất quán và kiểm soát được.",
  ]),
  para("Phần mềm cần được xây dựng để giải quyết toàn diện các vấn đề trên, đảm bảo dễ sử dụng và tin cậy trong môi trường không có kết nối internet."),
  h2("2.2","Phân tích bài toán"),
  h3("2.2.1","Phát biểu bài toán"),
  para("Thiết kế và cài đặt một ứng dụng Windows Forms quản lý toàn bộ hoạt động kinh doanh của cửa hàng đồ secondhand. Ứng dụng bao gồm năm nhóm chức năng chính: quản lý kho hàng (sản phẩm và tồn kho), quản lý khách hàng, thực hiện giao dịch bán hàng theo mô hình POS, quản lý và áp dụng chương trình khuyến mãi/voucher, và xem báo cáo phân tích doanh thu. Ứng dụng phải hoạt động offline, đảm bảo toàn vẹn dữ liệu thông qua transaction cơ sở dữ liệu và có khả năng tự phục hồi khi bị tắt đột ngột giữa chừng giao dịch."),
  h3("2.2.2","Yêu cầu chức năng"),
  stdTable(
    ["STT","Chức năng","Mô tả"],
    [
      ["1","Quản lý kho hàng","Thêm, sửa, xóa sản phẩm; quản lý số lượng tồn kho, hình ảnh và phân loại sản phẩm"],
      ["2","Tìm kiếm & lọc sản phẩm","Tìm kiếm theo tên/mã sản phẩm và lọc theo phân loại theo thời gian thực"],
      ["3","Quản lý khách hàng","Lưu trữ thông tin khách hàng, xem lịch sử mua hàng, chỉnh sửa thông tin"],
      ["4","Tra cứu khách theo SĐT","Tự động điền thông tin khách hàng khi nhập số điện thoại trong màn hình bán hàng"],
      ["5","Tạo đơn hàng (POS)","Giao diện bán hàng trực tiếp: chọn sản phẩm, điều chỉnh số lượng, tính tổng tiền"],
      ["6","Khuyến mãi / Voucher","Tạo và áp dụng voucher với 4 loại điều kiện và 2 hình thức giảm giá"],
      ["7","Thanh toán đơn hàng","Thực thi toàn bộ giao dịch trong một SQLite transaction; rollback nếu có lỗi"],
      ["8","Chỉnh sửa đơn hàng","Sửa hóa đơn đã hoàn thành: hoàn kho, chỉnh giỏ hàng, tạo hóa đơn thay thế"],
      ["9","Dashboard tổng quan","Hiển thị 4 chỉ số KPI và 10 giao dịch gần nhất"],
      ["10","Phân tích doanh thu","Biểu đồ và thống kê theo khoảng thời gian, top sản phẩm và khách hàng"],
      ["11","Khôi phục sự cố","Phát hiện và xử lý đơn hàng dở dang khi ứng dụng khởi động lại sau sự cố"],
    ],
    [500,2300,5987]
  ),
  caption("Bảng 2.1: Yêu cầu chức năng của hệ thống"),
  h3("2.2.3","Yêu cầu phi chức năng"),
  ...bullet([
    "Hiệu năng: Thao tác tìm kiếm, lọc sản phẩm phản hồi theo thời gian thực (dưới 200ms).",
    "Tính khả dụng: Giao diện trực quan với thẻ card và màu sắc phân biệt rõ ràng, không yêu cầu đào tạo kỹ thuật.",
    "Độ tin cậy: Mọi giao dịch thanh toán được thực thi trong SQLite transaction, đảm bảo rollback toàn bộ khi có lỗi.",
    "Offline: Toàn bộ dữ liệu lưu trên máy cục bộ (file SQLite), không cần kết nối internet.",
    "Khả năng phục hồi: Tự động phát hiện và đề xuất phục hồi đơn hàng bị gián đoạn khi ứng dụng khởi động lại.",
    "Khả năng mở rộng: Schema cơ sở dữ liệu hỗ trợ migration tự động, có thể thêm cột mới mà không làm hỏng dữ liệu cũ.",
  ]),
  h2("2.3","Mô hình Use Case"),
  h3("2.3.1","Sơ đồ Use Case tổng quát"),
  para("Hệ thống có một Actor duy nhất là Quản lý cửa hàng, người thực hiện toàn bộ các chức năng của phần mềm."),
  ...imgPlaceholder("Hình 2.1","Sơ đồ Use Case tổng quát hệ thống"),
  h3("2.3.2","Xác định Actor và danh sách Use Case"),
  stdTable(
    ["STT","Tên Actor","Vai trò"],
    [
      ["1","Quản lý cửa hàng","Người dùng duy nhất, thực hiện toàn bộ chức năng của hệ thống"],
    ],
    [600,2500,5687]
  ),
  caption("Bảng 2.2: Danh sách Actor"),
  blank(),
  stdTable(
    ["Actor tác động","Tên Use Case","Ý nghĩa"],
    [
      ["Quản lý cửa hàng","Thêm sản phẩm mới vào kho","Nhập thông tin và lưu sản phẩm mới vào hệ thống kho hàng"],
      ["Quản lý cửa hàng","Chỉnh sửa thông tin sản phẩm","Cập nhật thông tin sản phẩm đã có: tên, giá, số lượng, hình ảnh, phân loại"],
      ["Quản lý cửa hàng","Xóa sản phẩm","Xóa vĩnh viễn sản phẩm khỏi danh sách kho hàng"],
      ["Quản lý cửa hàng","Tìm kiếm và lọc sản phẩm","Lọc danh sách sản phẩm theo từ khóa tên/mã hoặc phân loại theo thời gian thực"],
      ["Quản lý cửa hàng","Thêm / Chỉnh sửa thông tin khách hàng","Lưu mới hoặc cập nhật thông tin liên lạc và ghi chú của khách hàng"],
      ["Quản lý cửa hàng","Tra cứu khách hàng theo số điện thoại","Tự động điền thông tin khách hàng khi nhập số điện thoại tại màn hình bán hàng"],
      ["Quản lý cửa hàng","Xem lịch sử mua hàng của khách hàng","Xem toàn bộ danh sách hóa đơn và thống kê chi tiêu của một khách hàng cụ thể"],
      ["Quản lý cửa hàng","Tạo đơn hàng mới (POS)","Chọn sản phẩm từ lưới card, điều chỉnh số lượng và lập giỏ hàng tại quầy bán"],
      ["Quản lý cửa hàng","Áp dụng Voucher / Khuyến mãi","Chọn và áp dụng chương trình giảm giá phù hợp vào đơn hàng đang tạo"],
      ["Quản lý cửa hàng","Thanh toán và hoàn tất đơn hàng","Xác nhận thanh toán, ghi hóa đơn và cập nhật tồn kho trong một SQLite transaction"],
      ["Quản lý cửa hàng","Chỉnh sửa đơn hàng đã hoàn thành","Mở lại hóa đơn cũ để chỉnh sửa: tự động hoàn trả kho và tạo hóa đơn thay thế"],
      ["Quản lý cửa hàng","Xem Dashboard Tổng Quan","Xem nhanh 4 chỉ số KPI và 10 giao dịch gần nhất ngay khi khởi động"],
      ["Quản lý cửa hàng","Phân tích doanh thu theo khoảng thời gian","Xem biểu đồ doanh thu, thống kê top 5 sản phẩm và khách hàng theo bộ lọc thời gian"],
      ["Quản lý cửa hàng","Quản lý Voucher / Khuyến mãi","Tạo, chỉnh sửa và vô hiệu hóa các chương trình voucher với nhiều loại điều kiện"],
    ],
    [2000,3000,3787]
  ),
  caption("Bảng 2.3: Danh sách Use Case"),

  h2("2.4","Đặc tả Use Case chi tiết"),
  h3("2.4.1","Thêm sản phẩm mới vào kho"),
  ucTable("UC01","Thêm sản phẩm mới vào kho","Quản lý cửa hàng",
    "Ứng dụng đang chạy, Quản lý đang ở màn hình Kho Hàng.",
    ["Quản lý nhấn nút \"Thêm sản phẩm mới\".","Hệ thống hiển thị form nhập liệu.","Quản lý nhập thông tin: mã sản phẩm, tên, số lượng, giá bán, phân loại.","Quản lý chọn hình ảnh sản phẩm (tùy chọn).","Quản lý nhấn \"Lưu\".","Hệ thống kiểm tra tính hợp lệ và lưu vào cơ sở dữ liệu.","Hệ thống làm mới danh sách, hiển thị sản phẩm vừa thêm."],
    ["Bước 6: Mã sản phẩm đã tồn tại → Thông báo lỗi, yêu cầu nhập lại.","Bước 6: Thông tin bắt buộc còn trống → Hiển thị cảnh báo."],
    "Sản phẩm mới được lưu vào kho và hiển thị trong danh sách."
  ),
  caption("Bảng 2.4: Đặc tả Thêm sản phẩm mới vào kho"),
  blank(),

  h3("2.4.2","Chỉnh sửa thông tin sản phẩm"),
  ucTable("UC02","Chỉnh sửa thông tin sản phẩm","Quản lý cửa hàng",
    "Ứng dụng đang chạy, đã có ít nhất một sản phẩm trong kho.",
    ["Quản lý vào màn hình Kho Hàng.","Quản lý click vào card sản phẩm cần chỉnh sửa.","Hệ thống mở form chỉnh sửa với dữ liệu hiện tại.","Quản lý cập nhật thông tin (tên, số lượng, giá, hình ảnh, phân loại).","Quản lý nhấn \"Lưu\".","Hệ thống cập nhật dữ liệu vào cơ sở dữ liệu và làm mới danh sách."],
    ["Bước 6: Dữ liệu không hợp lệ → Hiển thị thông báo lỗi, giữ nguyên form."],
    "Thông tin sản phẩm được cập nhật thành công trong hệ thống."
  ),
  caption("Bảng 2.5: Đặc tả Chỉnh sửa thông tin sản phẩm"),
  blank(),

  h3("2.4.3","Xóa sản phẩm"),
  ucTable("UC03","Xóa sản phẩm","Quản lý cửa hàng",
    "Ứng dụng đang chạy, đã có ít nhất một sản phẩm trong kho.",
    ["Quản lý click vào card sản phẩm cần xóa.","Hệ thống mở form chỉnh sửa sản phẩm.","Quản lý nhấn nút \"Xóa sản phẩm\".","Hệ thống hiển thị hộp thoại xác nhận.","Quản lý xác nhận xóa.","Hệ thống xóa sản phẩm khỏi cơ sở dữ liệu và làm mới danh sách."],
    ["Bước 5: Quản lý nhấn Hủy → Sản phẩm không bị xóa, đóng hộp thoại."],
    "Sản phẩm bị xóa vĩnh viễn khỏi hệ thống."
  ),
  caption("Bảng 2.6: Đặc tả Xóa sản phẩm"),
  blank(),

  h3("2.4.4","Tìm kiếm và lọc sản phẩm"),
  ucTable("UC04","Tìm kiếm và lọc sản phẩm","Quản lý cửa hàng",
    "Ứng dụng đang chạy, màn hình Kho Hàng đang hiển thị.",
    ["Quản lý nhập từ khóa vào ô tìm kiếm hoặc chọn phân loại từ danh sách lọc.","Hệ thống lọc danh sách sản phẩm theo thời gian thực (không cần nhấn nút).","Hệ thống hiển thị các sản phẩm khớp với điều kiện tìm kiếm."],
    ["Không có sản phẩm khớp → Danh sách trống, hiển thị thông báo \"Không tìm thấy sản phẩm\"."],
    "Danh sách sản phẩm hiển thị đúng theo điều kiện lọc."
  ),
  caption("Bảng 2.7: Đặc tả Tìm kiếm và lọc sản phẩm"),
  blank(),

  h3("2.4.5","Thêm / Chỉnh sửa thông tin khách hàng"),
  ucTable("UC05","Thêm / Chỉnh sửa thông tin khách hàng","Quản lý cửa hàng",
    "Ứng dụng đang chạy, màn hình Khách Hàng đang hiển thị.",
    ["Quản lý nhấn \"Thêm khách hàng\" hoặc click vào nút chỉnh sửa trên thẻ khách hàng.","Hệ thống mở form nhập liệu (với dữ liệu hiện tại nếu chỉnh sửa).","Quản lý nhập/cập nhật: họ tên, số điện thoại, địa chỉ, ghi chú.","Quản lý nhấn \"Lưu\".","Hệ thống lưu thông tin vào cơ sở dữ liệu và làm mới danh sách."],
    ["Bước 5: Số điện thoại đã tồn tại (khi thêm mới) → Thông báo lỗi trùng lặp.","Bước 5: Họ tên còn trống → Hiển thị cảnh báo bắt buộc."],
    "Thông tin khách hàng được lưu/cập nhật thành công."
  ),
  caption("Bảng 2.8: Đặc tả Thêm / Chỉnh sửa thông tin khách hàng"),
  blank(),

  h3("2.4.6","Tra cứu khách hàng theo số điện thoại"),
  ucTable("UC06","Tra cứu khách hàng theo số điện thoại","Quản lý cửa hàng",
    "Màn hình Giao Dịch (POS) đang hiển thị.",
    ["Quản lý nhập số điện thoại vào ô tra cứu khách hàng.","Quản lý rời khỏi ô nhập (nhấn Tab hoặc click ra ngoài).","Hệ thống tìm kiếm số điện thoại trong cơ sở dữ liệu.","Hệ thống tự động điền tên khách hàng vào ô tên."],
    ["Bước 3: Không tìm thấy số điện thoại → Hệ thống hỏi Quản lý có muốn tạo khách hàng mới với số điện thoại này không."],
    "Thông tin khách hàng được điền tự động vào form thanh toán."
  ),
  caption("Bảng 2.9: Đặc tả Tra cứu khách hàng theo số điện thoại"),
  blank(),

  h3("2.4.7","Xem lịch sử mua hàng của khách hàng"),
  ucTable("UC07","Xem lịch sử mua hàng của khách hàng","Quản lý cửa hàng",
    "Màn hình Khách Hàng đang hiển thị, đã có dữ liệu giao dịch.",
    ["Quản lý click vào thẻ khách hàng cần xem.","Hệ thống hiển thị panel bên phải với thống kê tổng quan (tổng đơn, tổng chi tiêu).","Hệ thống hiển thị danh sách các hóa đơn theo thứ tự thời gian giảm dần.","Quản lý click vào hóa đơn để mở rộng và xem chi tiết sản phẩm."],
    ["Khách hàng chưa có giao dịch → Hiển thị thông báo \"Chưa có hóa đơn nào\"."],
    "Quản lý nắm được toàn bộ lịch sử giao dịch của khách hàng."
  ),
  caption("Bảng 2.10: Đặc tả Xem lịch sử mua hàng của khách hàng"),
  blank(),

  h3("2.4.8","Tạo đơn hàng mới (POS)"),
  ucTable("UC08","Tạo đơn hàng mới (POS)","Quản lý cửa hàng",
    "Màn hình Giao Dịch đang hiển thị, có ít nhất một sản phẩm trong kho.",
    ["Quản lý nhập số điện thoại khách hàng (tùy chọn).","Quản lý click vào card sản phẩm để thêm vào giỏ hàng.","Hệ thống thêm sản phẩm vào bảng giỏ hàng với số lượng mặc định là 1.","Quản lý điều chỉnh số lượng trực tiếp trong bảng giỏ hàng.","Hệ thống tự động cập nhật thành tiền và tổng cộng."],
    ["Số lượng yêu cầu vượt tồn kho → Hệ thống cảnh báo và ngăn chặn tại bước nhập số lượng.","Sản phẩm đã có trong giỏ → Tăng số lượng thay vì thêm dòng mới."],
    "Giỏ hàng được chuẩn bị với đầy đủ sản phẩm, số lượng và tổng tiền."
  ),
  caption("Bảng 2.11: Đặc tả Tạo đơn hàng mới (POS)"),
  blank(),

  h3("2.4.9","Áp dụng Voucher / Khuyến mãi"),
  ucTable("UC09","Áp dụng Voucher / Khuyến mãi","Quản lý cửa hàng",
    "Màn hình Giao Dịch đang hiển thị, giỏ hàng có ít nhất một sản phẩm.",
    ["Quản lý nhấn nút \"Chọn Voucher\".","Hệ thống kiểm tra các voucher đang hoạt động và lọc ra những voucher đủ điều kiện áp dụng cho đơn hiện tại (dựa trên tổng tiền, số lượng sản phẩm, hoặc thời gian là khách hàng).","Hệ thống hiển thị danh sách voucher phù hợp.","Quản lý chọn một hoặc nhiều voucher.","Hệ thống tính toán số tiền giảm (theo tiền mặt hoặc phần trăm, có mức trần nếu được cấu hình) và cập nhật tổng tiền phải thanh toán."],
    ["Không có voucher nào đủ điều kiện → Thông báo \"Không có khuyến mãi phù hợp\".","Voucher hết số lượng hoặc hết hạn → Không hiển thị voucher đó."],
    "Tổng tiền thanh toán được giảm theo voucher đã chọn."
  ),
  caption("Bảng 2.12: Đặc tả Áp dụng Voucher / Khuyến mãi"),
  blank(),

  h3("2.4.10","Thanh toán và hoàn tất đơn hàng"),
  ucTable("UC10","Thanh toán và hoàn tất đơn hàng","Quản lý cửa hàng",
    "Giỏ hàng có ít nhất một sản phẩm, tổng tiền đã được xác nhận.",
    ["Quản lý kiểm tra lại giỏ hàng và nhấn \"Thanh toán\".","Hệ thống mở một SQLite transaction.","Hệ thống upsert thông tin khách hàng vào bảng Khach (nếu có).","Hệ thống tạo bản ghi HoaDon với trạng thái \"HoanThanh\".","Hệ thống tạo bản ghi NhapXuat cho từng sản phẩm trong giỏ.","Hệ thống trừ số lượng tồn kho (với kiểm tra optimistic: WHERE so_luong_ton >= @sl).","Hệ thống ghi nhận các voucher đã áp dụng vào bảng HoaDon_KhuyenMai.","Hệ thống commit transaction và hiển thị thông báo thành công.","Hệ thống xóa giỏ hàng, sẵn sàng cho giao dịch tiếp theo."],
    ["Bước 6: Tồn kho không đủ cho bất kỳ sản phẩm nào → Hệ thống rollback toàn bộ transaction, thông báo lỗi cụ thể."],
    "Hóa đơn được tạo thành công, tồn kho cập nhật, giỏ hàng được xóa."
  ),
  caption("Bảng 2.13: Đặc tả Thanh toán và hoàn tất đơn hàng"),
  blank(),

  h3("2.4.11","Chỉnh sửa đơn hàng đã hoàn thành"),
  ucTable("UC11","Chỉnh sửa đơn hàng đã hoàn thành","Quản lý cửa hàng",
    "Màn hình Khách Hàng đang hiển thị, tồn tại hóa đơn với trạng thái \"HoanThanh\".",
    ["Quản lý chọn khách hàng và click \"Sửa\" trên hóa đơn cần chỉnh sửa.","Hệ thống đánh dấu hóa đơn gốc là \"DangChinhSua\".","Hệ thống hoàn trả số lượng tồn kho của tất cả sản phẩm trong đơn gốc.","Hệ thống chuyển sang màn hình Giao Dịch và điền giỏ hàng theo đơn gốc.","Quản lý chỉnh sửa giỏ hàng (thêm/bớt/thay đổi số lượng sản phẩm).","Quản lý hoàn tất thanh toán (theo luồng UC10).","Hệ thống cập nhật hóa đơn gốc thành \"DaChinhSua\" và lưu mã hóa đơn mới."],
    ["Trong bước 4-6: Quản lý cố thoát khỏi màn hình Giao Dịch → Hệ thống hiển thị cảnh báo, yêu cầu xác nhận. Nếu hủy sửa, hệ thống rollback hóa đơn gốc về \"HoanThanh\".","Bước 6: Thanh toán thất bại → Rollback, hóa đơn gốc giữ nguyên \"DangChinhSua\"."],
    "Hóa đơn gốc được đánh dấu \"DaChinhSua\", hóa đơn mới được tạo thay thế."
  ),
  caption("Bảng 2.14: Đặc tả Chỉnh sửa đơn hàng đã hoàn thành"),
  blank(),

  h3("2.4.12","Xem Dashboard Tổng Quan"),
  ucTable("UC12","Xem Dashboard Tổng Quan","Quản lý cửa hàng",
    "Ứng dụng vừa khởi động hoặc Quản lý chọn mục Tổng Quan.",
    ["Hệ thống tự động tải và hiển thị 4 thẻ chỉ số KPI: tổng số sản phẩm, tổng số khách hàng, doanh thu tháng hiện tại, số đơn hàng tháng hiện tại.","Hệ thống hiển thị 10 giao dịch gần nhất dưới dạng thẻ hoạt động.","Quản lý click vào một thẻ hoạt động để điều hướng xem thông tin khách hàng liên quan."],
    ["Chưa có dữ liệu → Các chỉ số hiển thị giá trị 0, danh sách hoạt động trống."],
    "Quản lý nắm được tổng quan tình hình kinh doanh hiện tại."
  ),
  caption("Bảng 2.15: Đặc tả Xem Dashboard Tổng Quan"),
  blank(),

  h3("2.4.13","Phân tích doanh thu theo khoảng thời gian"),
  ucTable("UC13","Phân tích doanh thu theo khoảng thời gian","Quản lý cửa hàng",
    "Màn hình Phân Tích đang hiển thị, có ít nhất một hóa đơn trong hệ thống.",
    ["Quản lý chọn khoảng thời gian (ngày bắt đầu – ngày kết thúc).","Hệ thống tính toán và cập nhật 3 chỉ số KPI: tổng doanh thu, số đơn hàng, số lượng sản phẩm bán được.","Hệ thống vẽ biểu đồ đường thể hiện doanh thu theo từng ngày trong khoảng chọn.","Hệ thống hiển thị bảng Top 5 sản phẩm bán chạy nhất.","Hệ thống hiển thị bảng Top 5 khách hàng có doanh thu cao nhất.","Quản lý click vào điểm trên biểu đồ để xem chi tiết các đơn hàng trong ngày đó."],
    ["Không có dữ liệu trong khoảng thời gian → Hiển thị giá trị 0 và biểu đồ trống."],
    "Quản lý nắm được xu hướng kinh doanh và các sản phẩm/khách hàng tiêu biểu."
  ),
  caption("Bảng 2.16: Đặc tả Phân tích doanh thu theo khoảng thời gian"),
  blank(),

  h3("2.4.14","Quản lý Voucher / Khuyến mãi"),
  ucTable("UC14","Quản lý Voucher / Khuyến mãi","Quản lý cửa hàng",
    "Ứng dụng đang chạy, Quản lý truy cập chức năng Quản lý Khuyến Mãi.",
    ["Hệ thống hiển thị danh sách tất cả voucher (đang hoạt động và đã hết hạn).","Quản lý nhấn \"Thêm mới\" để tạo voucher: nhập mã, tên, loại điều kiện (không điều kiện / tổng tiền tối thiểu / số lượng sản phẩm / thời gian là khách hàng), giá trị điều kiện, loại giảm (tiền mặt / phần trăm), giá trị giảm, mức trần (nếu giảm %), ngày hiệu lực.","Quản lý nhấn \"Lưu\".","Hệ thống lưu voucher vào cơ sở dữ liệu.","Quản lý có thể chỉnh sửa hoặc vô hiệu hóa voucher bất kỳ lúc nào."],
    ["Bước 3: Mã voucher đã tồn tại → Thông báo lỗi trùng mã.","Dữ liệu không hợp lệ (ví dụ giá trị âm) → Hiển thị thông báo lỗi."],
    "Voucher được tạo/cập nhật và sẵn sàng sử dụng trong giao dịch."
  ),
  caption("Bảng 2.17: Đặc tả Quản lý Voucher / Khuyến mãi"),

  h2("2.5","Mô hình hoạt động (Activity Diagram)"),
  h3("2.5.1","Sơ đồ hoạt động tổng quát"),
  para("Sơ đồ hoạt động tổng quát mô tả luồng làm việc chính của Quản lý khi sử dụng ứng dụng, từ lúc khởi động đến khi thực hiện các nghiệp vụ cốt lõi."),
  ...imgPlaceholder("Hình 2.2","Sơ đồ hoạt động tổng quát"),
  h3("2.5.2","Luồng tạo đơn hàng và thanh toán"),
  para("Đây là luồng nghiệp vụ phức tạp nhất của hệ thống. Toàn bộ bước thanh toán được thực thi bên trong một SQLite transaction, đảm bảo tính toàn vẹn dữ liệu: nếu bất kỳ bước nào thất bại (ví dụ tồn kho không đủ), hệ thống rollback toàn bộ và giữ nguyên trạng thái trước đó."),
  ...imgPlaceholder("Hình 2.3","Sơ đồ hoạt động tạo đơn hàng và thanh toán"),
  h3("2.5.3","Luồng chỉnh sửa đơn hàng đã hoàn thành"),
  para("Luồng chỉnh sửa đơn hàng sử dụng cơ chế đánh dấu trạng thái (\"DangChinhSua\") để ngăn điều hướng ra ngoài giữa chừng, và hoàn tác tồn kho trước khi cho phép chỉnh sửa. Khi hoàn tất, hóa đơn gốc được đánh dấu \"DaChinhSua\" và hóa đơn mới được tạo thay thế."),
  ...imgPlaceholder("Hình 2.4","Sơ đồ hoạt động chỉnh sửa đơn hàng đã hoàn thành"),
  h3("2.5.4","Luồng áp dụng Voucher"),
  para("Hệ thống voucher hỗ trợ bốn loại điều kiện: (1) không điều kiện, (2) tổng tiền đơn hàng tối thiểu, (3) số lượng sản phẩm tối thiểu, (4) thời gian là khách hàng (tính theo tháng). Chỉ những voucher thỏa điều kiện mới được hiển thị để Quản lý lựa chọn."),
  ...imgPlaceholder("Hình 2.5","Sơ đồ hoạt động áp dụng Voucher / Khuyến mãi"),
  h3("2.5.5","Luồng khởi động ứng dụng (Migration & Crash Recovery)"),
  para("Mỗi lần khởi động, ứng dụng thực hiện hai tác vụ quan trọng trước khi hiển thị giao diện: (1) Migration – tự động thêm các cột mới vào schema cơ sở dữ liệu bằng lệnh ALTER TABLE ADD COLUMN bọc trong try/catch (idempotent, không gây lỗi nếu cột đã tồn tại); (2) Crash Recovery – quét các hóa đơn còn đang ở trạng thái \"DangChinhSua\" từ phiên trước và đề xuất Quản lý phục hồi hoặc hủy bỏ."),
  ...imgPlaceholder("Hình 2.6","Sơ đồ hoạt động khởi động ứng dụng (Migration & Crash Recovery)"),
];

// ─── CHAPTER 3 ───────────────────────────────────────────────────────────────
const buildCh3 = () => [
  chH(3,"THIẾT KẾ HỆ THỐNG"),
  h2("3.1","Thiết kế cơ sở dữ liệu"),
  h3("3.1.1","Sơ đồ ERD"),
  para("Cơ sở dữ liệu của hệ thống được tổ chức thành 7 bảng với quan hệ được thể hiện trong sơ đồ ERD sau."),
  ...imgPlaceholder("Hình 3.1","Sơ đồ ERD cơ sở dữ liệu"),
  h3("3.1.2","Mô tả chi tiết các bảng"),
  para("Bảng SanPham lưu trữ thông tin sản phẩm trong kho hàng:"),
  dbTable("SanPham",[
    ["1","id","INTEGER","PK, AUTOINCREMENT","Định danh sản phẩm"],
    ["2","ma_sp","TEXT","UNIQUE, NOT NULL","Mã sản phẩm (do Quản lý tự đặt)"],
    ["3","ten_sp","TEXT","NOT NULL","Tên sản phẩm"],
    ["4","so_luong_ton","INTEGER","NOT NULL","Số lượng tồn kho hiện tại"],
    ["5","gia_ban","NUMERIC","NOT NULL","Giá bán (đơn vị: VNĐ)"],
    ["6","hinh_anh","TEXT","","Đường dẫn tương đối đến file ảnh (Images/<guid>.ext)"],
    ["7","phanloai_id","INTEGER","FK → PhanLoai","Phân loại sản phẩm"],
  ]),
  caption("Bảng 3.1: Mô tả bảng SanPham"),
  blank(),
  para("Bảng PhanLoai lưu trữ các phân loại sản phẩm:"),
  dbTable("PhanLoai",[
    ["1","id","INTEGER","PK, AUTOINCREMENT","Định danh phân loại"],
    ["2","ten","TEXT","NOT NULL","Tên phân loại (ví dụ: Áo, Quần, Phụ kiện)"],
    ["3","ma","TEXT","UNIQUE, NOT NULL","Mã phân loại"],
  ]),
  caption("Bảng 3.2: Mô tả bảng PhanLoai"),
  blank(),
  para("Bảng Khach lưu trữ thông tin khách hàng:"),
  dbTable("Khach",[
    ["1","id","INTEGER","PK, AUTOINCREMENT","Định danh khách hàng"],
    ["2","ho_ten","TEXT","NOT NULL","Họ và tên đầy đủ"],
    ["3","so_dien_thoai","TEXT","UNIQUE, NOT NULL","Số điện thoại (dùng làm khóa tra cứu)"],
    ["4","dia_chi","TEXT","","Địa chỉ khách hàng"],
    ["5","ghi_chu","TEXT","","Ghi chú thêm"],
    ["6","ngay_tao","TEXT","","Ngày tạo tài khoản (ISO 8601), dùng tính điều kiện voucher theo thời gian"],
  ]),
  caption("Bảng 3.3: Mô tả bảng Khach"),
  blank(),
  para("Bảng HoaDon lưu trữ tiêu đề của mỗi hóa đơn bán hàng:"),
  dbTable("HoaDon",[
    ["1","id","INTEGER","PK, AUTOINCREMENT","Định danh hóa đơn"],
    ["2","ma_hd","TEXT","UNIQUE, NOT NULL","Mã hóa đơn (định dạng: HD{yyyyMMdd}{seq})"],
    ["3","KHACH_id","INTEGER","FK → Khach","Khách hàng của đơn hàng"],
    ["4","ngay_tao","TEXT","NOT NULL","Ngày giờ tạo hóa đơn"],
    ["5","tong_tien","NUMERIC","NOT NULL","Tổng tiền gốc (trước giảm giá)"],
    ["6","tong_tien_giam","NUMERIC","DEFAULT 0","Tổng số tiền được giảm"],
    ["7","tien_phai_thanh","NUMERIC","NOT NULL","Số tiền khách phải trả"],
    ["8","ghi_chu","TEXT","","Ghi chú đơn hàng"],
    ["9","TrangThai","TEXT","","'HoanThanh' | 'DangChinhSua' | 'DaChinhSua'"],
    ["10","MaHoaDonMoi","TEXT","","Mã hóa đơn thay thế (khi hóa đơn bị sửa)"],
  ]),
  caption("Bảng 3.4: Mô tả bảng HoaDon"),
  blank(),
  para("Bảng NhapXuat lưu trữ từng dòng chi tiết sản phẩm trong hóa đơn:"),
  dbTable("NhapXuat",[
    ["1","id","INTEGER","PK, AUTOINCREMENT","Định danh dòng giao dịch"],
    ["2","SAPHAM_id","INTEGER","FK → SanPham","Sản phẩm liên quan"],
    ["3","KHACH_id","INTEGER","FK → Khach","Khách hàng liên quan"],
    ["4","hoadon_id","INTEGER","FK → HoaDon","Hóa đơn chứa dòng này"],
    ["5","loai_giao_dich","TEXT","","Loại giao dịch (hiện tại: 'Xuat')"],
    ["6","so_luong","INTEGER","NOT NULL","Số lượng sản phẩm"],
    ["7","ngay_tao","TEXT","","Ngày giờ giao dịch"],
    ["8","ghi_chu","TEXT","","Ghi chú"],
  ]),
  caption("Bảng 3.5: Mô tả bảng NhapXuat"),
  blank(),
  para("Bảng KhuyenMai lưu trữ thông tin các chương trình voucher/khuyến mãi:"),
  dbTable("KhuyenMai",[
    ["1","id","INTEGER","PK, AUTOINCREMENT","Định danh khuyến mãi"],
    ["2","ma_km","TEXT","UNIQUE, NOT NULL","Mã voucher (ví dụ: CHAO50)"],
    ["3","ten_km","TEXT","NOT NULL","Tên mô tả chương trình"],
    ["4","so_luong_ton","INTEGER","","Số lượt còn lại (NULL = không giới hạn)"],
    ["5","ngay_bat_dau","TEXT","","Ngày bắt đầu hiệu lực"],
    ["6","ngay_ket_thuc","TEXT","","Ngày kết thúc (NULL = không giới hạn)"],
    ["7","loai_dieu_kien","INTEGER","DEFAULT 0","0: Không điều kiện; 1: Tổng tiền ≥; 2: Số SP ≥; 3: Tuổi KH (tháng) ≥"],
    ["8","gia_tri_dieu_kien","NUMERIC","","Giá trị của điều kiện"],
    ["9","loai_giam_gia","INTEGER","DEFAULT 1","1: Giảm tiền mặt cố định; 2: Giảm theo phần trăm"],
    ["10","gia_tri_giam","NUMERIC","NOT NULL","Giá trị giảm (tiền hoặc %)"],
    ["11","giam_toi_da","NUMERIC","","Mức giảm tối đa áp dụng cho loại %"],
    ["12","trang_thai","INTEGER","DEFAULT 1","1: Đang hoạt động; 0: Đã vô hiệu hóa"],
  ]),
  caption("Bảng 3.6: Mô tả bảng KhuyenMai"),
  blank(),
  para("Bảng HoaDon_KhuyenMai là bảng trung gian ghi nhận voucher đã được áp dụng cho từng hóa đơn:"),
  dbTable("HoaDon_KhuyenMai",[
    ["1","id","INTEGER","PK, AUTOINCREMENT","Định danh"],
    ["2","hoadon_id","INTEGER","FK → HoaDon","Hóa đơn được áp dụng"],
    ["3","khuyenmai_id","INTEGER","FK → KhuyenMai","Voucher được áp dụng"],
    ["4","so_tien_giam","NUMERIC","NOT NULL","Số tiền thực tế đã giảm"],
  ]),
  caption("Bảng 3.7: Mô tả bảng HoaDon_KhuyenMai"),

  h2("3.2","Mô hình lớp (Class Diagram)"),
  para("Sơ đồ lớp mô tả các lớp đối tượng chính trong hệ thống và mối quan hệ giữa chúng. Các lớp Model được thiết kế theo kiểu Active Record – mỗi lớp tự chứa các phương thức tương tác với cơ sở dữ liệu."),
  ...imgPlaceholder("Hình 3.2","Sơ đồ lớp (Class Diagram)"),
  para("Các lớp chính trong hệ thống bao gồm:"),
  stdTable(
    ["Tên lớp","Thuộc tính chính","Phương thức chính","Ghi chú"],
    [
      ["SanPham","id, MaSP, TenSP, SoLuong, GiaBan, HinhAnh, PhanLoaiId","LuuVaoDatabase(), CapNhatDatabase(), XoaKhoiDatabase()","Active Record, ánh xạ bảng SanPham"],
      ["KhachHang","id, HoTen, SoDienThoai, DiaChi, GhiChu","LuuVaoDatabase(), CapNhatDatabase(), XoaKhoiDatabase()","Active Record, ánh xạ bảng Khach"],
      ["KhuyenMai","id, MaKM, TenKM, LoaiDieuKien, GiaTriDieuKien, LoaiGiamGia, GiaTriGiam, GiamToiDa","ConHieuLuc(), TinhTienGiam(), LuuVaoDatabase(), LayDanhSach()","Chứa logic tính toán giảm giá"],
      ["HoaDonKhuyenMai","id, HoaDonId, KhuyenMaiId, SoTienGiam","–","Bảng liên kết, không có phương thức riêng"],
      ["EditOrderContext","MaHoaDonGoc, KhachHangInfo, DanhSachSanPham","–","DTO truyền dữ liệu khi chỉnh sửa đơn hàng"],
      ["PhanLoaiItem","Id, Ten, Ma","–","Model phụ cho phân loại sản phẩm"],
    ],
    [1800,2500,2700,1787]
  ),

  h2("3.3","Thiết kế giao diện"),
  h3("3.3.1","Màn hình Tổng Quan (Dashboard)"),
  para("Màn hình Tổng Quan là màn hình mặc định khi khởi động ứng dụng. Màn hình hiển thị 4 thẻ chỉ số KPI ở phía trên (Tổng sản phẩm, Tổng khách hàng, Doanh thu tháng, Số đơn tháng) và danh sách 10 hoạt động gần nhất phía dưới. Quản lý có thể click vào bất kỳ thẻ hoạt động nào để điều hướng sang màn hình Khách Hàng và xem chi tiết khách hàng đó."),
  ...imgPlaceholder("Hình 3.3","Giao diện màn hình Tổng Quan (Dashboard)"),
  h3("3.3.2","Màn hình Kho Hàng"),
  para("Màn hình Kho Hàng hiển thị toàn bộ sản phẩm dưới dạng các thẻ card có hình ảnh, tên, mã và giá sản phẩm. Ô tìm kiếm và bộ lọc phân loại nằm phía trên cho phép lọc realtime. Quản lý click vào một card để mở form chỉnh sửa/xóa, hoặc click \"Thêm sản phẩm mới\" để thêm sản phẩm."),
  ...imgPlaceholder("Hình 3.4","Giao diện màn hình Kho Hàng"),
  h3("3.3.3","Màn hình Khách Hàng"),
  para("Màn hình Khách Hàng chia thành hai phần: danh sách khách hàng bên trái (dạng card) và panel chi tiết bên phải. Panel bên phải hiển thị thống kê tổng quan và danh sách lịch sử hóa đơn của khách hàng được chọn. Mỗi hóa đơn có thể mở rộng để xem chi tiết sản phẩm và có nút \"Sửa\" để chỉnh sửa."),
  ...imgPlaceholder("Hình 3.5","Giao diện màn hình Khách Hàng"),
  h3("3.3.4","Màn hình Giao Dịch (POS)"),
  para("Màn hình Giao Dịch là giao diện bán hàng trực tiếp. Phần lớn màn hình là lưới sản phẩm dạng card để Quản lý chọn nhanh. Phần bên phải là khu vực thông tin đơn hàng: ô nhập SĐT khách, bảng giỏ hàng có thể chỉnh sửa số lượng trực tiếp, khu vực hiển thị voucher đã áp dụng, và nút Thanh Toán."),
  ...imgPlaceholder("Hình 3.6","Giao diện màn hình Giao Dịch (POS)"),
  h3("3.3.5","Màn hình Phân Tích"),
  para("Màn hình Phân Tích cung cấp bộ lọc thời gian, 3 thẻ KPI (doanh thu, số đơn, số sản phẩm bán được), biểu đồ đường doanh thu theo ngày và hai bảng Top 5 sản phẩm/khách hàng. Quản lý có thể click vào điểm trên biểu đồ để xem chi tiết các hóa đơn trong ngày đó."),
  ...imgPlaceholder("Hình 3.7","Giao diện màn hình Phân Tích"),
  h3("3.3.6","Quản lý Khuyến Mãi"),
  para("Form Quản lý Khuyến Mãi cho phép Quản lý xem danh sách toàn bộ voucher (đang hoạt động và đã hết hạn), thêm voucher mới với đầy đủ cấu hình điều kiện và giá trị giảm, hoặc chỉnh sửa/vô hiệu hóa voucher hiện có."),
  ...imgPlaceholder("Hình 3.8","Giao diện Quản lý Khuyến Mãi"),
];

// ─── CHAPTER 4 ───────────────────────────────────────────────────────────────
const buildCh4 = () => [
  chH(4,"KẾT LUẬN VÀ HƯỚNG PHÁT TRIỂN"),
  h2("4.1","Kết quả đạt được"),
  para("Sau quá trình phân tích, thiết kế và cài đặt, đề tài đã xây dựng thành công phần mềm \"Quản Lý Shop Đồ Secondhand\" với đầy đủ các chức năng đã đề ra:"),
  ...bullet([
    "Quản lý kho hàng: Thêm, sửa, xóa sản phẩm với hình ảnh và phân loại; tìm kiếm và lọc realtime.",
    "Quản lý khách hàng: Lưu trữ thông tin và tra cứu theo số điện thoại; xem lịch sử mua hàng chi tiết.",
    "Giao dịch bán hàng (POS): Giao diện card trực quan, giỏ hàng linh hoạt, thanh toán transaction đảm bảo toàn vẹn dữ liệu.",
    "Khuyến mãi / Voucher: Engine tính toán hỗ trợ 4 loại điều kiện và 2 hình thức giảm giá với mức trần.",
    "Chỉnh sửa đơn hàng: Cơ chế an toàn với trạng thái \"DangChinhSua\", hoàn kho tự động và ngăn điều hướng giữa chừng.",
    "Phân tích doanh thu: Biểu đồ tương tác, KPI và thống kê top sản phẩm/khách hàng.",
    "Khôi phục sự cố: Tự động phát hiện và xử lý đơn hàng dở dang khi khởi động lại.",
  ]),
  h2("4.2","Hạn chế"),
  para("Bên cạnh kết quả đạt được, phần mềm vẫn còn một số hạn chế sau:"),
  ...bullet([
    "Chỉ hỗ trợ một người dùng, chưa có phân quyền cho nhiều tài khoản khác nhau.",
    "Không hỗ trợ in hóa đơn hoặc xuất báo cáo ra file (PDF, Excel).",
    "Chưa có chức năng nhập hàng từ nhà cung cấp và quản lý lịch sử nhập kho.",
    "Cơ sở dữ liệu SQLite chỉ phù hợp với quy mô nhỏ, chưa hỗ trợ truy cập đồng thời.",
    "Không có chức năng sao lưu và phục hồi dữ liệu tự động.",
  ]),
  h2("4.3","Hướng phát triển"),
  para("Trong tương lai, phần mềm có thể được phát triển thêm theo các hướng sau:"),
  ...bullet([
    "Bổ sung chức năng xuất hóa đơn ra PDF và xuất báo cáo doanh thu ra Excel.",
    "Phát triển chức năng quản lý nhập kho: theo dõi lịch sử nhập hàng, nhà cung cấp.",
    "Thêm tính năng sao lưu/phục hồi dữ liệu tự động lên cloud hoặc ổ cứng ngoài.",
    "Mở rộng lên ứng dụng web hoặc đa nền tảng (Windows/Android/iOS) để quản lý từ xa.",
    "Tích hợp thanh toán điện tử (QR code, ví điện tử) và quản lý đa chi nhánh.",
    "Bổ sung module quản lý nhân viên và phân quyền truy cập.",
  ]),
];

// ─── REFERENCES ──────────────────────────────────────────────────────────────
const buildRefs = () => [
  new Paragraph({alignment:AlignmentType.CENTER, spacing:SP_CH, indent:{left:0},
    children:[run("TÀI LIỆU THAM KHẢO",{bold:true,size:SZ_CH})]}),
  ...[
    "[1]  Microsoft Corporation (2023). Windows Forms documentation for .NET. <https://learn.microsoft.com/en-us/dotnet/desktop/winforms> (truy cập tháng 6/2026).",
    "[2]  SQLite Consortium (2024). SQLite Documentation. <https://www.sqlite.org/docs.html> (truy cập tháng 6/2026).",
    "[3]  Dapper GitHub (2024). Dapper – A simple object mapper for .NET. <https://github.com/DapperLib/Dapper> (truy cập tháng 6/2026).",
    "[4]  ReaLTaiizor GitHub (2024). ReaLTaiizor – Windows Forms UI Library. <https://github.com/Taiizor/ReaLTaiizor> (truy cập tháng 6/2026).",
    "[5]  Jacobson, I., Booch, G. & Rumbaugh, J. (1999). The Unified Software Development Process. Addison-Wesley.",
  ].map(t => new Paragraph({spacing:SP_PARA, indent:{firstLine:0, hanging:360, left:360},
    children:[run(t)]})),
];

// ─── APPENDIX ────────────────────────────────────────────────────────────────
const buildAppendix = () => [
  new Paragraph({alignment:AlignmentType.CENTER, spacing:SP_CH, indent:{left:0},
    children:[run("PHỤ LỤC",{bold:true,size:SZ_CH})]}),
  para("Mã nguồn đầy đủ của dự án được lưu trữ trong thư mục nộp bài kèm theo báo cáo. Cấu trúc thư mục dự án như sau:"),
  ...numbered([
    "dosi/ – Thư mục dự án chính (.NET 8 WinForms)",
    "dosi/Quan Ly Shop Do Si.cs – Form chính (shell navigation)",
    "dosi/SanPham.cs – Model sản phẩm (Active Record)",
    "dosi/KhachHang.cs – Model khách hàng (Active Record)",
    "dosi/KhuyenMai.cs – Model và logic tính toán voucher",
    "dosi/ViewGiaoDich.cs – Màn hình Giao Dịch (POS, cart, voucher, checkout)",
    "dosi/ViewKhoHang.cs – Màn hình Kho Hàng",
    "dosi/ViewKhachHang.cs – Màn hình Khách Hàng",
    "dosi/ViewTongQuan.cs – Màn hình Dashboard",
    "dosi/ViewPhanTich.cs – Màn hình Phân Tích",
    "QuanlyKho_setup.sql – Script khởi tạo dữ liệu mẫu",
  ]),
];

// ─── FOOTER ──────────────────────────────────────────────────────────────────
const makeFooter = () => new Footer({
  children: [new Paragraph({
    alignment: AlignmentType.CENTER,
    children: [
      new TextRun({ children: [PageNumber.CURRENT], font: F, size: SZ }),
    ],
  })],
});

// ─── ASSEMBLE DOCUMENT ───────────────────────────────────────────────────────
const frontChildren = [
  ...buildCover(),
  pageBreak(),
  ...buildThanks(),
  pageBreak(),
  ...buildCommitment(),
  pageBreak(),
  ...buildReviewPage("Hướng Dẫn"),
  pageBreak(),
  ...buildReviewPage("Phản Biện"),
  pageBreak(),
  ...buildTOC(),
  pageBreak(),
  ...buildFigList(),
  pageBreak(),
  ...buildTblList(),
];

const mainChildren = [
  ...buildCh1(),
  pageBreak(),
  ...buildCh2(),
  pageBreak(),
  ...buildCh3(),
  pageBreak(),
  ...buildCh4(),
  pageBreak(),
  ...buildRefs(),
  pageBreak(),
  ...buildAppendix(),
];

const PAGE_PROPS = {
  page: {
    size: { width: 11906, height: 16838 }, // A4
    margin: { top: 1134, right: 1134, bottom: 1418, left: 1985 },
  },
};

const doc = new Document({
  styles: {
    default: {
      document: { run: { font: F, size: SZ } },
    },
  },
  sections: [
    {
      properties: { ...PAGE_PROPS },
      children: frontChildren,
    },
    {
      properties: { ...PAGE_PROPS, type: SectionType.NEXT_PAGE },
      footers: { default: makeFooter() },
      children: mainChildren,
    },
  ],
});

Packer.toBuffer(doc).then(buf => {
  const out = '/sessions/loving-sleepy-sagan/mnt/shop2nd/BaoCao_QuanLyShopDo2nd.docx';
  fs.writeFileSync(out, buf);
  console.log('Done:', out, Math.round(buf.length/1024) + 'KB');
}).catch(e => { console.error(e); process.exit(1); });


