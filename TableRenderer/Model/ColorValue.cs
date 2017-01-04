using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableRenderer.Model
{
  public struct ColorValue : IEquatable<ColorValue>, IBackground
  {
    private bool _hasValue;
    public byte A { get; set; }
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }

    public ColorValue(byte r, byte g, byte b)
    {
      _hasValue = true;
      A = 255;
      R = r;
      B = b;
      G = g;
    }

    public ColorValue(byte a, byte r, byte g, byte b)
    {
      _hasValue = true;
      A = a;
      R = r;
      B = b;
      G = g;
    }

    public ColorValue(Double a, byte r, byte g, byte b)
    {
      _hasValue = true;
      A = (byte)Math.Max(Math.Min(Math.Ceiling(255 * a), 255), 0);
      R = r;
      B = b;
      G = g;
    }

    public ColorValue(System.Drawing.Color color)
    {
      _hasValue = true;
      A = color.A;
      R = color.R;
      B = color.B;
      G = color.G;
    }

    public static ColorValue FromRgba(byte r, byte g, byte b, Single a)
    {
      return new ColorValue(a, r, g, b);
    }

    public static ColorValue FromRgba(byte r, byte g, byte b, Double a)
    {
      return new ColorValue(a, r, g, b);
    }

    public static ColorValue FromRgb(byte r, byte g, byte b)
    {
      return new ColorValue(r, g, b);
    }

    public static ColorValue FromHsl(Single h, Single s, Single l)
    {
      const Single third = 1f / 3f;

      var m2 = l <= 0.5f ? (l * (s + 1f)) : (l + s - l * s);
      var m1 = 2f * l - m2;
      var r = (Byte)Math.Round(255 * HueToRgb(m1, m2, h + third));
      var g = (Byte)Math.Round(255 * HueToRgb(m1, m2, h));
      var b = (Byte)Math.Round(255 * HueToRgb(m1, m2, h - third));
      return new ColorValue(r, g, b);
    }

    public bool IsEmpty()
    {
      return !_hasValue;
    }

    public static explicit operator ColorValue(string value)
    {
      return ColorValue.FromString(value);
    }

    public static implicit operator string(ColorValue value)
    {
      return value.ToString();
    }

    public static ColorValue FromString(string color)
    {
      color = color.Trim();
      ColorValue result;
      if (color.StartsWith("#") && TryFromHex(color.Substring(1), out result))
      {
        return result;
      }
      else if (color.StartsWith("rgba("))
      {
        var parts = color.Substring(5, color.Length - 6).Split(',');
        if (parts.Length != 4)
          throw new FormatException("Cannot convert the given string.");
        return new ColorValue(double.Parse(parts[3].Trim())
          , byte.Parse(parts[0].Trim()), byte.Parse(parts[1].Trim()), byte.Parse(parts[2].Trim()));
      }
      else if (color.StartsWith("rgb("))
      {
        var parts = color.Substring(4, color.Length - 5).Split(',');
        if (parts.Length != 3)
          throw new FormatException("Cannot convert the given string.");
        if (parts.All(p => p.Trim().EndsWith("%")))
        {
          return new ColorValue(FromPercentage(parts[0]), FromPercentage(parts[1]), FromPercentage(parts[2]));
        }
        else
        {
          return new ColorValue(byte.Parse(parts[0].Trim()), byte.Parse(parts[1].Trim()), byte.Parse(parts[2].Trim()));
        }
      }
      else if (string.Equals(color, "transparent", StringComparison.OrdinalIgnoreCase))
      {
        return ColorValue.Transparent;
      }
      else
      {
        int value;
        if (_knownColors.TryGetValue(color, out value))
        {
          return new ColorValue((byte)(value >> 16 & 255L),
            (byte)(value >> 8 & 255L),
            (byte)(value & 255L));
        }
        else
        {
          throw new FormatException("Cannot convert the given string.");
        }
      }
    }

    private static byte FromPercentage(string percent)
    {
      var perc = double.Parse(percent.Trim().TrimEnd('%')) / 100.0;
      return (byte)Math.Min(255, perc * 256);
    }

    public static ColorValue FromHex(string color)
    {
      ColorValue result;
      if (TryFromHex(color, out result))
        return result;

      throw new ArgumentException("Invalid color format" + color, "color");
    }

    public static bool TryFromHex(string value, out ColorValue color)
    {
      color = ColorValue.Empty;

      for (var i = 0; i < value.Length; i++)
      {
        if (!value[i].IsHex())
          return false;
      }

      if (value.Length == 3 || value.Length == 4)
      {
        var r = value[0].FromHex();
        r += r * 16;

        var g = value[1].FromHex();
        g += g * 16;

        var b = value[2].FromHex();
        b += b * 16;

        if (value.Length == 4)
        {
          var a = value[3].FromHex();
          a += a * 16;

          color = new ColorValue((byte)a, (byte)r, (byte)g, (byte)b);
          return true;
        }

        color = new ColorValue((byte)r, (byte)g, (byte)b);
        return true;
      }

      if (value.Length == 6 || value.Length == 8)
      {
        var r = 16 * value[0].FromHex();
        var g = 16 * value[2].FromHex();
        var b = 16 * value[4].FromHex();

        r += value[1].FromHex();
        g += value[3].FromHex();
        b += value[5].FromHex();

        if (value.Length == 8)
        {
          var a = 16 * value[6].FromHex();
          a += value[7].FromHex();
          color = new ColorValue((byte)a, (byte)r, (byte)g, (byte)b);
          return true;
        }

        color = new ColorValue((byte)r, (byte)g, (byte)b);
        return true;
      }

      return false;
    }

    public double Alpha
    {
      get { return A / 255.0; }
    }

    public static bool operator ==(ColorValue a, ColorValue b)
    {
      return a.GetHashCode() == b.GetHashCode();
    }

    public static bool operator !=(ColorValue a, ColorValue b)
    {
      return a.GetHashCode() != b.GetHashCode();
    }

    public override bool Equals(Object obj)
    {
      if (obj is ColorValue)
      {
        return Equals((ColorValue)obj);
      }

      return false;
    }

    public override int GetHashCode()
    {
      return unchecked(A + (R << 8) + (G << 16) + (B << 24));
    }

    public override string ToString()
    {
      return this.ToCss();
    }

    public void ToCss(Writer.ICssWriter writer)
    {
      if (A == 255 && ((R >> 4) == (R & 0x0F)) && ((G >> 4) == (G & 0x0F)) && ((B >> 4) == (B & 0x0F)))
      {
        writer.Value('#');
        writer.Value(R.ToHexChar());
        writer.Value(G.ToHexChar());
        writer.Value(B.ToHexChar());
      }
      else if (A == 255)
      {
        writer.Value('#');
        writer.Value(R.ToHex());
        writer.Value(G.ToHex());
        writer.Value(B.ToHex());
      }
      else
      {
        writer.Value("rgba(");
        writer.Value(R.ToString());
        writer.Value(",");
        writer.Value(G.ToString());
        writer.Value(",");
        writer.Value(B.ToString());
        writer.Value(",");
        writer.Value(Alpha.ToString("0.##"));
        writer.Value(")");
      }
    }

    public bool Equals(ColorValue other)
    {
      return this._hasValue == other._hasValue
        && this.GetHashCode() == other.GetHashCode();
    }

    private static Single HueToRgb(Single m1, Single m2, Single h)
    {
      const Single sixth = 1f / 6f;
      const Single third2 = 2f / 3f;

      if (h < 0f)
      {
        h += 1f;
      }
      else if (h > 1f)
      {
        h -= 1f;
      }

      if (h < sixth)
      {
        return m1 + (m2 - m1) * h * 6f;
      }
      if (h < 0.5)
      {
        return m2;
      }
      if (h < third2)
      {
        return m1 + (m2 - m1) * (third2 - h) * 6f;
      }

      return m1;
    }

    public bool Equals(IBackground other)
    {
      if (other is ColorValue)
        return Equals((ColorValue)other);
      return false;
    }

    public static ColorValue Black { get { return new ColorValue(255, 0, 0, 0); } }
    public static ColorValue Empty { get { return new ColorValue(); } }
    public static ColorValue Transparent { get { return new ColorValue(0, 0, 0, 0); } }
    public static ColorValue White { get { return new ColorValue(255, 255, 255, 255); } }

    private static Dictionary<string, int> _knownColors = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
    {
      { "AliceBlue", -984833},
      { "AntiqueWhite", -332841},
      { "Aqua", -16711681},
      { "Aquamarine", -8388652},
      { "Azure", -983041},
      { "Beige", -657956},
      { "Bisque", -6972},
      { "Black", -16777216},
      { "BlanchedAlmond", -5171},
      { "Blue", -16776961},
      { "BlueViolet", -7722014},
      { "Brown", -5952982},
      { "BurlyWood", -2180985},
      { "CadetBlue", -10510688},
      { "Chartreuse", -8388864},
      { "Chocolate", -2987746},
      { "Coral", -32944},
      { "CornflowerBlue", -10185235},
      { "Cornsilk", -1828},
      { "Crimson", -2354116},
      { "Cyan", -16711681},
      { "DarkBlue", -16777077},
      { "DarkCyan", -16741493},
      { "DarkGoldenrod", -4684277},
      { "DarkGray", -5658199},
      { "DarkGreen", -16751616},
      { "DarkKhaki", -4343957},
      { "DarkMagenta", -7667573},
      { "DarkOliveGreen", -11179217},
      { "DarkOrange", -29696},
      { "DarkOrchid", -6737204},
      { "DarkRed", -7667712},
      { "DarkSalmon", -1468806},
      { "DarkSeaGreen", -7357301},
      { "DarkSlateBlue", -12042869},
      { "DarkSlateGray", -13676721},
      { "DarkTurquoise", -16724271},
      { "DarkViolet", -7077677},
      { "DeepPink", -60269},
      { "DeepSkyBlue", -16728065},
      { "DimGray", -9868951},
      { "DodgerBlue", -14774017},
      { "Firebrick", -5103070},
      { "FloralWhite", -1296},
      { "ForestGreen", -14513374},
      { "Fuchsia", -65281},
      { "Gainsboro", -2302756},
      { "GhostWhite", -460545},
      { "Gold", -10496},
      { "Goldenrod", -2448096},
      { "Gray", -8355712},
      { "Green", -16744448},
      { "GreenYellow", -5374161},
      { "Honeydew", -983056},
      { "HotPink", -38476},
      { "IndianRed", -3318692},
      { "Indigo", -11861886},
      { "Ivory", -16},
      { "Khaki", -989556},
      { "Lavender", -1644806},
      { "LavenderBlush", -3851},
      { "LawnGreen", -8586240},
      { "LemonChiffon", -1331},
      { "LightBlue", -5383962},
      { "LightCoral", -1015680},
      { "LightCyan", -2031617},
      { "LightGoldenrodYellow", -329006},
      { "LightGray", -2894893},
      { "LightGreen", -7278960},
      { "LightPink", -18751},
      { "LightSalmon", -24454},
      { "LightSeaGreen", -14634326},
      { "LightSkyBlue", -7876870},
      { "LightSlateGray", -8943463},
      { "LightSteelBlue", -5192482},
      { "LightYellow", -32},
      { "Lime", -16711936},
      { "LimeGreen", -13447886},
      { "Linen", -331546},
      { "Magenta", -65281},
      { "Maroon", -8388608},
      { "MediumAquamarine", -10039894},
      { "MediumBlue", -16777011},
      { "MediumOrchid", -4565549},
      { "MediumPurple", -7114533},
      { "MediumSeaGreen", -12799119},
      { "MediumSlateBlue", -8689426},
      { "MediumSpringGreen", -16713062},
      { "MediumTurquoise", -12004916},
      { "MediumVioletRed", -3730043},
      { "MidnightBlue", -15132304},
      { "MintCream", -655366},
      { "MistyRose", -6943},
      { "Moccasin", -6987},
      { "NavajoWhite", -8531},
      { "Navy", -16777088},
      { "OldLace", -133658},
      { "Olive", -8355840},
      { "OliveDrab", -9728477},
      { "Orange", -23296},
      { "OrangeRed", -47872},
      { "Orchid", -2461482},
      { "PaleGoldenrod", -1120086},
      { "PaleGreen", -6751336},
      { "PaleTurquoise", -5247250},
      { "PaleVioletRed", -2396013},
      { "PapayaWhip", -4139},
      { "PeachPuff", -9543},
      { "Peru", -3308225},
      { "Pink", -16181},
      { "Plum", -2252579},
      { "PowderBlue", -5185306},
      { "Purple", -8388480},
      { "Red", -65536},
      { "RosyBrown", -4419697},
      { "RoyalBlue", -12490271},
      { "SaddleBrown", -7650029},
      { "Salmon", -360334},
      { "SandyBrown", -744352},
      { "SeaGreen", -13726889},
      { "SeaShell", -2578},
      { "Sienna", -6270419},
      { "Silver", -4144960},
      { "SkyBlue", -7876885},
      { "SlateBlue", -9807155},
      { "SlateGray", -9404272},
      { "Snow", -1286},
      { "SpringGreen", -16711809},
      { "SteelBlue", -12156236},
      { "Tan", -2968436},
      { "Teal", -16744320},
      { "Thistle", -2572328},
      { "Tomato", -40121},
      { "Turquoise", -12525360},
      { "Violet", -1146130},
      { "Wheat", -663885},
      { "White", -1},
      { "WhiteSmoke", -657931},
      { "Yellow", -256},
      { "YellowGreen", -6632142}
    };
  }
}
