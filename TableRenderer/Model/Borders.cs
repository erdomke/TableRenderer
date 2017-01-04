using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableRenderer.Model
{
  public class Borders : IEquatable<Borders>, ICssRenderable
  {
    public Border Bottom { get; set; }
    public Border Left { get; set; }
    public Border Right { get; set; }
    public Border Top { get; set; }

    public Borders() { }
    public Borders(UnitValue width, BorderStyle style, ColorValue color)
    {
      var border = new Border(width, style, color);
      this.Bottom = border;
      this.Left = border;
      this.Right = border;
      this.Top = border;
    }

    public override bool Equals(object obj)
    {
      if (obj is Borders)
        return Equals((Borders)obj);
      return false;
    }

    public bool Equals(Borders other)
    {
      return this.Bottom == other.Bottom
        && this.Left == other.Left
        && this.Right == other.Right
        && this.Top == other.Top;
    }

    public override int GetHashCode()
    {
      return this.Bottom.GetHashCode()
        ^ this.Left.GetHashCode()
        ^ this.Right.GetHashCode()
        ^ this.Top.GetHashCode();
    }

    public override string ToString()
    {
      return this.ToCss();
    }

    public void ToCss(Writer.ICssWriter writer)
    {
      ToCss(writer, "G", System.Globalization.CultureInfo.CurrentCulture);
    }

    public void ToCss(Writer.ICssWriter writer, string format, IFormatProvider formatProvider)
    {
      if (this.Top == this.Right && this.Right == this.Bottom && this.Bottom == this.Left)
      {
        if (Top.Style != BorderStyle.Inherit)
        {
          writer.Property("border");
          this.Top.ToCss(writer, format, formatProvider);
        }
      }
      else
      {
        if (Bottom.Style != BorderStyle.Inherit)
        {
          writer.Property("border-bottom");
          this.Bottom.ToCss(writer, format, formatProvider);
        }
        if (Left.Style != BorderStyle.Inherit)
        {
          writer.Property("border-left");
          this.Left.ToCss(writer, format, formatProvider);
        }
        if (Right.Style != BorderStyle.Inherit)
        {
          writer.Property("border-right");
          this.Right.ToCss(writer, format, formatProvider);
        }
        if (Top.Style != BorderStyle.Inherit)
        {
          writer.Property("border-top");
          this.Top.ToCss(writer, format, formatProvider);
        }
      }
    }

    public static bool operator ==(Borders a, Borders b)
    {
      return Utils.IsEqualTo(a, b);
    }

    public static bool operator !=(Borders a, Borders b)
    {
      return !Utils.IsEqualTo(a, b);
    }
  }
}
