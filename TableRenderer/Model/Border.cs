using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableRenderer.Writer;

namespace TableRenderer.Model
{
  public struct Border : IEquatable<Border>, IFormattable, ICssRenderable
  {
    public ColorValue Color { get; set; }
    public UnitValue Width { get; set; }
    public BorderStyle Style { get; set; }

    public Border(UnitValue width, BorderStyle style, ColorValue color)
    {
      this.Width = width;
      this.Style = style;
      this.Color = color;
    }

    public bool IsVisible()
    {
      return this.Style != BorderStyle.Hidden
        && this.Style != BorderStyle.Inherit
        && this.Style != BorderStyle.None
        && this.Width >= 0.001;
    }

    private static UnitValue _thin = new UnitValue(1, UnitType.Enumeration);
    public static UnitValue Thin { get { return _thin; } }
    private static UnitValue _medium = new UnitValue(3, UnitType.Enumeration);
    public static UnitValue Medium { get { return _medium; } }
    private static UnitValue _thick = new UnitValue(5, UnitType.Enumeration);
    public static UnitValue Thick { get { return _thick; } }

    public override int GetHashCode()
    {
      return this.Color.GetHashCode()
        ^ this.Width.GetHashCode()
        ^ this.Style.GetHashCode();
    }
    public override bool Equals(object obj)
    {
      if (obj is Border)
        return Equals((Border)obj);
      return false;
    }
    public bool Equals(Border other)
    {
      return this.Color == other.Color
        && this.Width == other.Width
        && this.Style == other.Style;
    }

    public static bool operator ==(Border a, Border b)
    {
      return a.Equals(b);
    }

    public static bool operator !=(Border a, Border b)
    {
      return !a.Equals(b);
    }

    public static Border operator +(Border border, UnitValue width)
    {
      return new Border()
      {
        Width = width,
        Style = border.Style,
        Color = border.Color
      };
    }
    public static Border operator +(Border border, BorderStyle style)
    {
      return new Border()
      {
        Width = border.Width,
        Style = style,
        Color = border.Color
      };
    }
    public static Border operator +(Border border, ColorValue color)
    {
      return new Border()
      {
        Width = border.Width,
        Style = border.Style,
        Color = color
      };
    }

    public override string ToString()
    {
      return ToString("G", System.Globalization.CultureInfo.CurrentCulture);
    }

    public string ToString(string format)
    {
      return ToString(format, System.Globalization.CultureInfo.CurrentCulture);
    }

    public string ToString(string format, IFormatProvider formatProvider)
    {
      using (var writer = new CssWriter())
      {
        ToCss(writer, format, formatProvider);
        return writer.ToString();
      }
    }

    public void ToCss(ICssWriter writer)
    {
      ToCss(writer, "G", System.Globalization.CultureInfo.CurrentCulture);
    }

    public void ToCss(ICssWriter writer, string format, IFormatProvider formatProvider)
    {
      if (Style == BorderStyle.Inherit
        || Style == BorderStyle.None)
      {
        writer.Value(Style.ToString().ToLowerInvariant());
      }
      else
      {
        Width.ToCss(writer, format, formatProvider);
        writer.Value(" ");
        writer.Value(Style.ToString().ToLowerInvariant());
        writer.Value(" ");
        Color.ToCss(writer);
      }
    }
  }
}
