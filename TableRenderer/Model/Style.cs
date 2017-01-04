using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableRenderer.Writer;

namespace TableRenderer.Model
{
  public class Style : IEquatable<Style>, ICssRenderable
  {
    public IBackground Background { get; set; }
    public Borders Borders { get; set; }
    public Font Font { get; set; }
    public string Format { get; set; }
    public UnitValue Height { get; set; }
    public string Name { get; set; }
    public Padding Padding { get; set; }
    public TextAlign TextAlign { get; set; }
    public UnitValue TextIndent { get; set; }
    public VerticalAlign VerticalAlign { get; set; }
    public Visibility Visibility { get; set; }
    public WhiteSpace WhiteSpace { get; set; }
    public UnitValue Width { get; set; }

    public static Style GetMerged(Style general, Style specific)
    {
      if (general == null && specific == null)
        return null;
      if (general == null)
        return specific;
      if (specific == null)
        return general;

      var result = new Style();
      result.Background = specific.Background ?? general.Background;
      result.Borders = specific.Borders ?? general.Borders;
      result.Font = Font.GetMerged(general.Font, specific.Font);
      result.Format = specific.Format ?? general.Format;
      result.Height = UnitValue.GetMerged(general.Height, specific.Height);
      result.Name = specific.Name ?? general.Name;
      result.Padding = specific.Padding.IsEmpty() ? general.Padding : specific.Padding;
      result.TextAlign = specific.TextAlign == TextAlign.Inherit ? general.TextAlign : specific.TextAlign;
      result.TextIndent = UnitValue.GetMerged(general.TextIndent, specific.TextIndent);
      result.VerticalAlign = specific.VerticalAlign == VerticalAlign.Inherit ? general.VerticalAlign : specific.VerticalAlign;
      result.Visibility = specific.Visibility == Visibility.Inherit ? general.Visibility : specific.Visibility;
      result.WhiteSpace = specific.WhiteSpace == WhiteSpace.Inherit ? general.WhiteSpace : specific.WhiteSpace;
      result.Width = UnitValue.GetMerged(general.Width, specific.Width);
      return result;
    }

    public override bool Equals(object obj)
    {
      if (obj is Style)
        return Equals((Style)obj);
      return false;
    }

    public bool Equals(Style other)
    {
      return Utils.IsEqualTo(this.Background, other.Background)
        && Utils.IsEqualTo(this.Borders, other.Borders)
        && this.Font == other.Font
        && Utils.IsEqualTo(this.Format, other.Format)
        && this.Height == other.Height
        && Utils.IsEqualTo(this.Name, other.Name)
        && this.Padding == other.Padding
        && this.TextAlign == other.TextAlign
        && this.TextIndent == other.TextIndent
        && this.VerticalAlign == other.VerticalAlign
        && this.Visibility == other.Visibility
        && this.WhiteSpace == other.WhiteSpace
        && this.Width == other.Width;
    }

    public override int GetHashCode()
    {
      return Utils.GetHashCode(this.Background)
        ^ Utils.GetHashCode(this.Borders)
        ^ this.Font.GetHashCode()
        ^ Utils.GetHashCode(this.Format)
        ^ this.Height.GetHashCode()
        ^ Utils.GetHashCode(this.Name)
        ^ this.Padding.GetHashCode()
        ^ this.TextAlign.GetHashCode()
        ^ this.TextIndent.GetHashCode()
        ^ this.VerticalAlign.GetHashCode()
        ^ this.Visibility.GetHashCode()
        ^ this.WhiteSpace.GetHashCode()
        ^ this.Width.GetHashCode();
    }

    public void ToCss(ICssWriter writer)
    {
      if (this.Background != null)
      {
        writer.Property("background");
        this.Background.ToCss(writer);
      }

      if (this.Borders != null)
      {
        this.Borders.ToCss(writer);
      }

      this.Font.ToCss(writer);

      if (this.Height.Unit != UnitType.Empty)
      {
        writer.Property("height");
        Height.ToCss(writer);
      }

      if (!this.Padding.IsEmpty())
      {
        writer.Property("padding");
        Padding.ToCss(writer);
      }

      if (TextAlign != TextAlign.Inherit)
      {
        writer.Property("text-align");
        writer.Value(TextAlign.ToString().ToLowerInvariant());
      }

      if (TextIndent.Unit != UnitType.Empty)
      {
        writer.Property("text-indent");
        TextIndent.ToCss(writer);
      }

      if (VerticalAlign != VerticalAlign.Inherit)
      {
        writer.Property("vertical-align");
        writer.Value(VerticalAlign.ToString().ToLowerInvariant());
      }

      if (Visibility != Visibility.Inherit)
      {
        writer.Property("visibility");
        writer.Value(Visibility.ToString().ToLowerInvariant());
      }

      if (WhiteSpace != WhiteSpace.Inherit)
      {
        writer.Property("white-space");
        writer.Value(WhiteSpace.ToString().ToLowerInvariant());
      }

      if (Width.Unit != UnitType.Empty)
      {
        writer.Property("width");
        Width.ToCss(writer);
      }
    }

    public static bool operator ==(Style a, Style b)
    {
      return Utils.IsEqualTo(a, b);
    }

    public static bool operator !=(Style a, Style b)
    {
      return !Utils.IsEqualTo(a, b);
    }
  }
}
