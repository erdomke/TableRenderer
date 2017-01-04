using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableRenderer
{
  static class Utils
  {
    public static string ToCss(this Model.ICssRenderable renderable)
    {
      using (var writer = new Writer.CssWriter())
      {
        renderable.ToCss(writer);
        return writer.ToString();
      }
    }

    public static bool IsEqualTo(object x, object y)
    {
      if (System.Object.ReferenceEquals(x, y))
        return true;
      if (x == null || y == null)
        return false;
      return x.Equals(y);
    }

    public static int GetHashCode(object x)
    {
      if (x == null)
        return 0;
      return x.GetHashCode();
    }

    public static int FromHex(this char character)
    {
      return char.IsDigit(character) ? character - 0x30 : character - (char.IsLower(character) ? 0x57 : 0x37);
    }

    public static bool IsHex(this char c)
    {
      return char.IsDigit(c) || (c >= 0x41 && c <= 0x46) || (c >= 0x61 && c <= 0x66);
    }

    public static string ToHex(this byte num)
    {
      var characters = new char[2];
      var rem = num >> 4;

      characters[0] = (char)(rem + (rem < 10 ? 48 : 55));
      rem = num - 16 * rem;
      characters[1] = (char)(rem + (rem < 10 ? 48 : 55));

      return new string(characters);
    }

    public static char ToHexChar(this byte num)
    {
      var rem = num & 0x0F;
      return (char)(rem + (rem < 10 ? 48 : 55));
    }
  }
}
