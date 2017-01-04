using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableRenderer.Model
{
  public struct UnitValue : IEquatable<UnitValue>, IComparable<UnitValue>, IFormattable, ICssRenderable
  {
    public double Value { get; set; }
    public UnitType Unit { get; set; }

    public UnitValue(double value, UnitType unit)
    {
      this.Value = value;
      this.Unit = unit;
    }

    public double ToPx(UnitContext context = default(UnitContext))
    {
      switch (Unit)
      {
        // Absolute units
        case UnitType.Centimeter:
          return this.Value * 96.0 / 2.54;
        case UnitType.Inch:
          return this.Value * 96.0;
        case UnitType.Millimeter:
          return this.Value * 96.0 / 25.4;
        case UnitType.Pica:
          return this.Value * 96.0 / 6;
        case UnitType.Pixel:
          return this.Value;
        case UnitType.Point:
          return this.Value * 96.0 / 72;
        // Font units
        case UnitType.Ch:
          return this.Value * (context.FontHeight <= 0 ? 16 : context.FontHeight) * 0.5;
        case UnitType.Em:
          return this.Value * (context.FontHeight <= 0 ? 16 : context.FontHeight);
        case UnitType.Ex:
          return this.Value * (context.FontHeight <= 0 ? 16 : context.FontHeight) * 0.5;
        case UnitType.Rem:
          return this.Value * (context.RootFontHeight <= 0 ? 16 : context.RootFontHeight);
        // Other relative unites
        case UnitType.Percent:
          return this.Value * (context.PercentageBasis <= 0 ? 100 : context.PercentageBasis) / 100.0;
        case UnitType.ViewportHeight:
          return this.Value * (context.ViewportHeight <= 0 ? 100 : context.ViewportHeight) / 100.0;
        case UnitType.ViewportMax:
          var max = Math.Max(context.ViewportHeight, context.ViewportWidth) / 100.0;
          return this.Value * (max <= 0 ? 100 : max);
        case UnitType.ViewportMin:
          var min = Math.Min(context.ViewportHeight, context.ViewportWidth) / 100.0;
          return this.Value * (min <= 0 ? 100 : min);
        case UnitType.ViewportWidth:
          return this.Value * (context.ViewportWidth <= 0 ? 100 : context.ViewportWidth) / 100.0;
        case UnitType.Empty:
          return 0;
        case UnitType.Enumeration:
          return this.Value;
        default:
          throw new InvalidOperationException();
      }
    }

    private static UnitValue _empty = new UnitValue();
    public static UnitValue Empty { get { return _empty; } }

    public double ToPt(UnitContext context = default(UnitContext))
    {
      return ToPx(context) * 72.0 / 96;
    }
    public double ToEm(UnitContext context = default(UnitContext))
    {
      return ToPx(context) / 16.0;
    }

    public static implicit operator UnitValue(double? value)
    {
      if (value.HasValue)
        return new UnitValue(value.Value, UnitType.Pixel);
      return UnitValue.Empty;
    }
    public static explicit operator UnitValue(string value)
    {
      return UnitValue.FromNumberString(value);
    }

    public static Border operator +(UnitValue width, BorderStyle style)
    {
      return new Border()
      {
        Width = width,
        Style = style,
        Color = ColorValue.Transparent
      };
    }

    public static UnitValue FromNumberString(string value)
    {
      if (string.IsNullOrEmpty(value))
        return UnitValue.Empty;

      int i = 0;
      while (i < value.Length && (char.IsDigit(value[i]) || value[i] == '.')) i++;

      if (i < 1) throw new FormatException("No number found");
      var dbl = double.Parse(value.Substring(0, i));
      if (i >= value.Length) return new UnitValue(dbl, UnitType.Pixel);
      return new UnitValue(dbl, ConvertStringToUnitType(value.Substring(i).Trim()));
    }

    internal static UnitType ConvertStringToUnitType(string unit)
    {
      switch (unit)
      {
        case "%":
          return UnitType.Percent;
        case "em":
          return UnitType.Em;
        case "cm":
          return UnitType.Centimeter;
        case "ex":
          return UnitType.Ex;
        case "rem":
          return UnitType.Rem;
        case "ch":
          return UnitType.Ch;
        case "in":
          return UnitType.Inch;
        case "mm":
          return UnitType.Millimeter;
        case "pc":
          return UnitType.Pica;
        case "pt":
          return UnitType.Point;
        case "px":
          return UnitType.Pixel;
        case "vw":
          return UnitType.ViewportWidth;
        case "vh":
          return UnitType.ViewportHeight;
        case "vmin":
          return UnitType.ViewportMin;
        case "vmax":
          return UnitType.ViewportMax;
      }

      throw new FormatException("Invalid unit string");
    }

    internal static string ConvertUnitTypeToString(UnitType type)
    {
      switch (type)
      {
        case UnitType.Centimeter:
          return "cm";
        case UnitType.Ch:
          return "ch";
        case UnitType.Em:
          return "em";
        case UnitType.Empty:
          return "";
        case UnitType.Ex:
          return "ex";
        case UnitType.Inch:
          return "in";
        case UnitType.Inherit:
          return "inherit";
        case UnitType.Millimeter:
          return "mm";
        case UnitType.Percent:
          return "%";
        case UnitType.Pica:
          return "pc";
        case UnitType.Point:
          return "pt";
        case UnitType.Rem:
          return "rem";
        case UnitType.ViewportHeight:
          return "vh";
        case UnitType.ViewportMax:
          return "vmax";
        case UnitType.ViewportMin:
          return "vmin";
        case UnitType.ViewportWidth:
          return "vw";
      }
      return "px";
    }

    public override bool Equals(object obj)
    {
      if (obj is UnitValue)
        return Equals((UnitValue)obj);
      return false;
    }

    public bool Equals(UnitValue other)
    {
      return this.Unit == other.Unit
        && this.Value == other.Value;
    }

    public static bool operator ==(UnitValue a, UnitValue b)
    {
      return a.Equals(b);
    }

    public static bool operator !=(UnitValue a, UnitValue b)
    {
      return !a.Equals(b);
    }

    public static bool operator <(UnitValue a, UnitValue b)
    {
      return a.CompareTo(b) < 0;
    }

    public static bool operator >(UnitValue a, UnitValue b)
    {
      return a.CompareTo(b) > 0;
    }

    public static bool operator <=(UnitValue a, UnitValue b)
    {
      return a.CompareTo(b) <= 0;
    }

    public static bool operator >=(UnitValue a, UnitValue b)
    {
      return a.CompareTo(b) >= 0;
    }

    public static UnitValue operator +(UnitValue a, UnitValue b)
    {
      if (a.Unit == b.Unit)
        return new UnitValue(a.Value + b.Value, a.Unit);
      return new UnitValue(a.ToPx() + b.ToPx(), UnitType.Pixel);
    }

    public static UnitValue operator -(UnitValue a, UnitValue b)
    {
      if (a.Unit == b.Unit)
        return new UnitValue(a.Value - b.Value, a.Unit);
      return new UnitValue(a.ToPx() - b.ToPx(), UnitType.Pixel);
    }

    public static UnitValue operator *(UnitValue a, double b)
    {
      return new UnitValue(a.Value * b, a.Unit);
    }

    public static UnitValue operator /(UnitValue a, double b)
    {
      return new UnitValue(a.Value / b, a.Unit);
    }

    public override int GetHashCode()
    {
      return this.Unit.GetHashCode() ^ this.Value.GetHashCode();
    }

    public int CompareTo(UnitValue other)
    {
      return this.ToPx().CompareTo(other.ToPx());
    }


    public static UnitValue GetMerged(UnitValue general, UnitValue specific)
    {
      return specific.Unit == UnitType.Inherit ? general : specific;
    }

    public override string ToString()
    {
      return ToString("G");
    }

    public string ToString(string format)
    {
      return ToString(format, System.Globalization.CultureInfo.CurrentCulture);
    }

    public string ToString(IFormatProvider formatProvider)
    {
      return ToString("G", formatProvider);
    }

    public string ToString(string format, IFormatProvider formatProvider)
    {
      using (var writer = new Writer.CssWriter())
      {
        ToCss(writer, format, formatProvider);
        return writer.ToString();
      }
    }

    public void ToCss(Writer.ICssWriter writer)
    {
      ToCss(writer, "G", System.Globalization.CultureInfo.CurrentCulture);
    }

    public void ToCss(Writer.ICssWriter writer, string format, IFormatProvider formatProvider)
    {
      switch (Unit)
      {
        case UnitType.Empty:
          writer.Value("");
          break;
        case UnitType.Inherit:
          writer.Value("inherit");
          break;
        default:
          writer.Value(Value.ToString(format, formatProvider));
          writer.Value(ConvertUnitTypeToString(Unit));
          break;
      }
    }
  }
}
