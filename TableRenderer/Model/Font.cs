using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableRenderer.Model
{
  public struct Font : IEquatable<Font>, ICssRenderable
  {
    public ColorValue Color { get; set; }
    public TextDecoration Decoration { get; set; }
    public string Family { get; set; }
    public UnitValue Size { get; set; }
    public FontStyle Style { get; set; }
    public VerticalTextAlign VerticalAlign { get; set; }
    public FontWeight Weight { get; set; }

    public string GetFamily(FontSet set = null)
    {
      set = set ?? FontSet.Standard;
      if (string.IsNullOrEmpty(Family))
        return set.SansSerif;

      switch (Family.ToLowerInvariant())
      {
        case "serif":
          return set.Serif;
        case "sans-serif":
          return set.SansSerif;
        case "cursive":
          return set.Cursive;
        case "fantasy":
          return set.Fantasy;
        case "monospace":
          return set.Monospace;
      }
      return Family;
    }

    public bool IsBold()
    {
      return this.Weight == FontWeight.Bolder
        || this.Weight >= FontWeight.Bold;
    }
    public bool IsEmpty()
    {
      return Color == ColorValue.Empty
        && Decoration == TextDecoration.Inherit
        && string.IsNullOrEmpty(Family)
        && Size.Unit == UnitType.Empty
        && Style == FontStyle.Inherit
        && VerticalAlign == VerticalTextAlign.Inherit
        && Weight == FontWeight.Inherit;
    }
    public bool IsItalic()
    {
      return this.Style == FontStyle.Italic
        || this.Style == FontStyle.Oblique;
    }
    public bool IsStrikeThrough()
    {
      return (this.Decoration & TextDecoration.LineThrough) != 0;
    }
    public bool IsUnderlined()
    {
      return (this.Decoration & TextDecoration.Underline) != 0;
    }

    public static Font GetMerged(Font general, Font specific)
    {
      var result = new Font();
      result.Color = specific.Color.IsEmpty() ? general.Color : specific.Color;
      result.Decoration = specific.Decoration == TextDecoration.Inherit ? general.Decoration : specific.Decoration;
      result.Family = string.Equals(specific.Family, "inherit", StringComparison.OrdinalIgnoreCase) ? general.Family : specific.Family;
      result.Size = UnitValue.GetMerged(general.Size, specific.Size);
      result.Style = specific.Style == FontStyle.Inherit ? general.Style : specific.Style;
      result.VerticalAlign = specific.VerticalAlign == VerticalTextAlign.Inherit ? general.VerticalAlign : specific.VerticalAlign;
      result.Weight = specific.Weight == FontWeight.Inherit ? general.Weight : specific.Weight;
      return result;
    }

    public static bool operator ==(Font a, Font b)
    {
      return a.Equals(b);
    }

    public static bool operator !=(Font a, Font b)
    {
      return !a.Equals(b);
    }

    public static Font operator +(Font font, ColorValue color)
    {
      return new Font()
      {
        Color = color,
        Decoration = font.Decoration,
        Family = font.Family,
        Size = font.Size,
        Style = font.Style,
        VerticalAlign = font.VerticalAlign,
        Weight = font.Weight
      };
    }

    public static Font operator +(Font font, TextDecoration decoration)
    {
      return new Font()
      {
        Color = font.Color,
        Decoration = decoration,
        Family = font.Family,
        Size = font.Size,
        Style = font.Style,
        VerticalAlign = font.VerticalAlign,
        Weight = font.Weight
      };
    }


    public static Font operator +(Font font, FontWeight weight)
    {
      return new Font()
      {
        Color = font.Color,
        Decoration = font.Decoration,
        Family = font.Family,
        Size = font.Size,
        Style = font.Style,
        VerticalAlign = font.VerticalAlign,
        Weight = weight
      };
    }

    public static Font operator +(Font font, FontStyle style)
    {
      return new Font()
      {
        Color = font.Color,
        Decoration = font.Decoration,
        Family = font.Family,
        Size = font.Size,
        Style = style,
        VerticalAlign = font.VerticalAlign,
        Weight = font.Weight
      };
    }

    public static Font operator +(Font font, UnitValue size)
    {
      return new Font()
      {
        Color = font.Color,
        Decoration = font.Decoration,
        Family = font.Family,
        Size = size,
        Style = font.Style,
        VerticalAlign = font.VerticalAlign,
        Weight = font.Weight
      };
    }

    public static Font operator +(Font font, string family)
    {
      return new Font()
      {
        Color = font.Color,
        Decoration = font.Decoration,
        Family = family,
        Size = font.Size,
        Style = font.Style,
        VerticalAlign = font.VerticalAlign,
        Weight = font.Weight
      };
    }

    public static Font operator +(Font font, VerticalTextAlign align)
    {
      return new Font()
      {
        Color = font.Color,
        Decoration = font.Decoration,
        Family = font.Family,
        Size = font.Size,
        Style = font.Style,
        VerticalAlign = align,
        Weight = font.Weight
      };
    }

    public override bool Equals(object obj)
    {
      if (obj is Font)
        return Equals((Font)obj);
      return false;
    }

    public bool Equals(Font other)
    {
      return this.Color == other.Color
        && this.Decoration == other.Decoration
        && this.Family == other.Family
        && this.Size == other.Size
        && this.Style == other.Style
        && this.VerticalAlign == other.VerticalAlign
        && this.Weight == other.Weight;
    }

    public override int GetHashCode()
    {
      return this.Color.GetHashCode()
        ^ this.Decoration.GetHashCode()
        ^ (this.Family ?? "").GetHashCode()
        ^ this.Size.GetHashCode()
        ^ this.Style.GetHashCode()
        ^ this.VerticalAlign.GetHashCode()
        ^ this.Weight.GetHashCode();
    }

    public override string ToString()
    {
      return this.ToCss();
    }

    public void ToCss(Writer.ICssWriter writer)
    {
      if (!Color.IsEmpty())
      {
        writer.Property("color");
        Color.ToCss(writer);
      }

      var values = new List<string>();
      if ((Decoration & TextDecoration.Blink) > 0) values.Add("blink");
      if ((Decoration & TextDecoration.LineThrough) > 0) values.Add("line-through");
      if ((Decoration & TextDecoration.Overline) > 0) values.Add("overline");
      if ((Decoration & TextDecoration.Underline) > 0) values.Add("underline");
      if (values.Any())
      {
        writer.Property("text-decoration");
        for (var i = 0; i < values.Count; i++)
        {
          if (i > 0) writer.Value(" ");
          writer.Value(values[i]);
        }
      }

      if (!string.IsNullOrEmpty(Family))
      {
        writer.Property("font-family");
        if (Family.IndexOf(' ') > 0)
        {
          writer.Value('"');
          writer.Value(Family);
          writer.Value('"');
        }
        else
        {
          writer.Value(Family);
        }
      }

      if (Size.Unit != UnitType.Empty)
      {
        writer.Property("font-size");
        Size.ToCss(writer);
      }

      if (Style != FontStyle.Inherit)
      {
        writer.Property("font-style");
        writer.Value(Style.ToString().ToLowerInvariant());
      }

      if (VerticalAlign != VerticalTextAlign.Inherit)
      {
        writer.Property("vertical-align");
        writer.Value(VerticalAlign.ToString().ToLowerInvariant());
      }

      if (Weight != FontWeight.Inherit)
      {
        writer.Property("font-weight");
        writer.Value(Weight.ToString().TrimStart('w').ToLowerInvariant());
      }
    }
  }
}
