using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableRenderer.Model;
using TableRenderer.Writer;

namespace TableRenderer.Writer.Tests
{
  static class TableSample
  {
    public static void DataTypes(ITableWriter writer)
    {
      writer.StartPart(new Table() { AutoFilter = true, RepeatRowsEachPage = 1 });

      writer.StartPart(new TableHead());

      writer.StartPart(new Row());
      writer.StartPart(new Cell() { Style = new Style() { Width = 200 } }).Value("String").EndPart();
      writer.StartPart(new Cell() { Style = new Style() { Width = 200 } }).Value("Date").EndPart();
      writer.StartPart(new Cell() { Style = new Style() { Width = 200 } }).Value("Number").EndPart();
      writer.StartPart(new Cell() { Style = new Style() { Width = 20 } }).Value("Other").EndPart();
      writer.EndPart();

      writer.EndPart();

      writer.StartPart(new TableBody());

      writer.StartPart(new Row());
      writer.StartPart(new Cell()).Value("a thing").EndPart();
      writer.StartPart(new Cell()).Value(new DateTime(2000, 11, 4, 14, 30, 20)).EndPart();
      writer.StartPart(new Cell()).Value(52.3).EndPart();
      using (var imgData = new MemoryStream(Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAd1JREFUeNrEU79rFEEU/ubX7eTckPM0nSAaVA4xhHBwxICSRkgVYa0EDTZ2Wvkv+Cdok2uClbhpU9iolQRtUliYHOpqUiSeIiq57N3urO/NJhaCEEzhLG+Yfe/73rxfI4qiwGGWxCGXvte+jt00jZ1z0UGjEUJASrlkg+CazrIsnp5sRWdPnQM5OVjYUmLt/dtoZfV1rIEimmveBDnyxjKIv0Ui6Pby1Gi2sLL6KtJKSyI53HlwC0pIjNaPeycMFHtoTm1f9/lrFznhH95dBHO1kgoD10d9pIZhW8WVqRl//7uNBD9Th8Ggj6OhxekTJ8Hunr58hp205znMlUYppC7FaO0YZi/NQqgKJEnnY4LJ8020Ji6i8ynxOrYxpj5S9xzmSk1blu+iogwVR+PxcgytKwiPhBS78xJWQ69jG2MYyxzm+hQy5LA2gDEWN67O75XLUc7y91lK422KSNZazylT0ORA5Nj60sVCvAATWCgToHHmAjrJGy98Zh3b2k/a2NjeRE4c5mqjDQoq7xSBvvd66H5Yh6OSW6rYUBCWXej30U3WIQnXaowjrFjPMcZA3H90+3l1OLhcNlj4GfCzIP4Ygf3W0lfwD4F2fqQvGDZGUvvHp/BN/PfX+EuAAQDe/6+PbBTYZAAAAABJRU5ErkJggg==")))
      {
        writer.StartPart(new Cell()).Value(new System.Drawing.Bitmap(imgData)).EndPart();
      }
      writer.EndPart();

      writer.StartPart(new Row());
      writer.StartPart(new Cell()).Value("a second thing").EndPart();
      writer.StartPart(new Cell()).Value(new DateTime(2000, 10, 3)).EndPart();
      writer.StartPart(new Cell()).Value(0xf).EndPart();
      writer.StartPart(new Cell()).Value(new Uri("http://www.google.com")).EndPart();
      writer.EndPart();

      writer.EndPart();

      writer.EndPart();
    }

    public static void MergedCells(ITableWriter writer)
    {
      writer.StartPart(new Table());

      writer.StartPart(new Row());
      writer.StartPart(new Cell() { RowSpan = 2 }).Value("First").EndPart();
      writer.StartPart(new Cell() { ColumnSpan = 2 }).Value("Second").EndPart();
      writer.StartPart(new Cell() { ColumnSpan = 2, RowSpan = 2 }).Value("Third").EndPart();
      writer.EndPart();

      writer.StartPart(new Row());
      writer.StartPart(new Cell() { ColumnSpan = 2 }).Value("Fourth").EndPart();
      writer.EndPart();

      writer.EndPart();
    }

    public static void StyleTest(ITableWriter writer)
    {
      writer.StartPart(new Table());

      writer.StartPart(new Row());
      writer.StartPart(new Cell()
      {
        Style = new Style() { Font = new Font() + (ColorValue)"#0070C0" + TextDecoration.Underline + FontWeight.Bold, Width = 64 }
      }).Value("Color").EndPart();
      writer.StartPart(new Cell() { Style = new Style() { Background = (ColorValue)"#6600FF", Width = 64 } }).EndPart();
      writer.StartPart(new Cell() { Style = new Style() { Width = 146, WhiteSpace = WhiteSpace.Normal } }).Value("Only\r\nNew line").EndPart();
      writer.EndPart();

      writer.StartPart(new Row());
      writer.StartPart(new Cell() { Style = new Style() { VerticalAlign = VerticalAlign.Middle, TextAlign = TextAlign.Left, TextIndent = new UnitValue(1, UnitType.Ch) } })
        .Value("Text").EndPart();
      writer.StartPart(new Cell() { Style = new Style() { WhiteSpace = WhiteSpace.Normal } }).Value("Some really long text that should wrap").EndPart();
      writer.StartPart(new Cell() { Style = new Style() { WhiteSpace = WhiteSpace.Normal } })
        .Value("Rich ")
        .StartPart(new Span() { Style = new Style() { Font = new Font() + FontWeight.Bold + TextDecoration.Underline } })
        .Value("text\r\n")
        .EndPart()
        .Value("- New line\r\n- ")
        .StartPart(new Span() { Style = new Style() { Font = new Font() + TextDecoration.LineThrough } })
        .Value("Strike")
        .EndPart()
        .StartPart(new Span() { Style = new Style() { Font = new Font() + VerticalTextAlign.Super } })
        .Value("Super")
        .EndPart()
        .StartPart(new Span() { Style = new Style() { Font = new Font() + VerticalTextAlign.Sub } })
        .Value("Sub\r\n")
        .EndPart()
        .StartPart(new Span() { Style = new Style() { Font = new Font() + (ColorValue)"#C00000" + FontStyle.Italic } })
        .Value("- Color")
        .EndPart()
        .EndPart();
      writer.EndPart();

      writer.StartPart(new Row());
      writer.StartPart(new Cell() { Style = new Style() { Font = new Font() { Decoration = TextDecoration.Underline | TextDecoration.Double | TextDecoration.LineThrough, VerticalAlign = VerticalTextAlign.Super } } })
        .Value("Weird Text").EndPart();
      writer.StartPart(new Cell() { Style = new Style() { Borders = new Borders()
      {
        Left = Border.Thin + BorderStyle.Solid + ColorValue.Black,
        Right = Border.Thin + BorderStyle.Dotted + ColorValue.Black,
        Top = Border.Medium + BorderStyle.Solid + ColorValue.Black,
        Bottom = Border.Thin + BorderStyle.Double + (ColorValue)"#00B050"
      } } })
        .Value("Border").EndPart();
      writer.EndPart();

      writer.EndPart();
    }

    public static void MultipleSheets(ITableWriter writer)
    {
      writer.StartPart(new Sheet() { Name = "First" });
      DataTypes(writer);
      writer.EndPart();

      writer.StartPart(new Sheet() { Name = "Second" });
      MergedCells(writer);
      writer.EndPart();

      writer.StartPart(new Sheet() { Name = "Third" });
      StyleTest(writer);
      writer.EndPart();
    }
  }
}
