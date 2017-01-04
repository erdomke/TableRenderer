using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableRenderer.Writer;

namespace TableRenderer.Model
{
  /// <summary>Represents padding or margin information associated with a user interface (UI) element.</summary>
  public struct Padding : IEquatable<Padding>, IFormattable, ICssRenderable
  {
    public UnitValue Bottom { get; set; }
    public UnitValue Left { get; set; }
    public UnitValue Right { get; set; }
    public UnitValue Top { get; set; }

    public Padding(UnitValue all)
    {
      this.Bottom = all;
      this.Left = all;
      this.Right = all;
      this.Top = all;
    }

    public UnitValue Horizontal { get { return this.Left + this.Right; } }
    public UnitValue Vertical { get { return this.Top + this.Bottom; } }

    public bool IsEmpty()
    {
      return Bottom.Unit == UnitType.Empty
        && Left.Unit == UnitType.Empty
        && Right.Unit == UnitType.Empty
        && Top.Unit == UnitType.Empty;
    }

    public override int GetHashCode()
    {
      return this.Bottom.GetHashCode()
        ^ this.Left.GetHashCode()
        ^ this.Right.GetHashCode()
        ^ this.Top.GetHashCode();
    }
    public override bool Equals(object obj)
    {
      if (obj is Padding)
        return Equals((Padding)obj);
      return false;
    }
    public bool Equals(Padding other)
    {
      return this.Bottom == other.Bottom
        && this.Left == other.Left
        && this.Right == other.Right
        && this.Top == other.Top;
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
      using (var writer = new Writer.CssWriter())
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
      if (this.IsEmpty())
      {
        writer.Value("inherit");
      }
      else if (this.Bottom == this.Top && this.Left == this.Right)
      {
        if (this.Bottom == this.Left)
        {
          Top.ToCss(writer, format, formatProvider);
        }
        else
        {
          Top.ToCss(writer, format, formatProvider);
          writer.Value(" ");
          Right.ToCss(writer, format, formatProvider);
        }
      }
      else
      {
        Top.ToCss(writer, format, formatProvider);
        writer.Value(" ");
        Right.ToCss(writer, format, formatProvider);
        writer.Value(" ");
        Bottom.ToCss(writer, format, formatProvider);
        writer.Value(" ");
        Left.ToCss(writer, format, formatProvider);
      }
    }

    public static bool operator ==(Padding a, Padding b)
    {
      return a.Equals(b);
    }

    public static bool operator !=(Padding a, Padding b)
    {
      return !a.Equals(b);
    }

    public static Padding operator +(Padding a, Padding b)
    {
      return new Padding()
      {
        Bottom = a.Bottom + b.Bottom,
        Left = a.Left + b.Left,
        Right = a.Right + b.Right,
        Top = a.Top + b.Top
      };
    }

    public static Padding operator -(Padding a, Padding b)
    {
      return new Padding()
      {
        Bottom = a.Bottom - b.Bottom,
        Left = a.Left - b.Left,
        Right = a.Right - b.Right,
        Top = a.Top - b.Top
      };
    }
  }
}
