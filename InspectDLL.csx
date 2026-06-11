using System;
using System.Reflection;

var asm = Assembly.LoadFrom(@"D:\Dev\vtc\2Hand\shop2nd\dosi\bin\Debug\net8.0-windows\ReaLTaiizor.dll");
var t = asm.GetType("ReaLTaiizor.Controls.MaterialButton");
if (t == null) { Console.WriteLine("Not found"); return; }
foreach (var p in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
    Console.WriteLine($"{p.PropertyType.Name,-20} {p.Name}");
