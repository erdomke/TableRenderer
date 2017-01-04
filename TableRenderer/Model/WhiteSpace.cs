using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableRenderer.Model
{
  /// <summary>
  /// white-space is used to describe how whitespace inside the element is
  /// handled.
  /// </summary>
  public enum WhiteSpace
  {
    /// <summary>
    /// Use the style of the first parent where it is set
    /// </summary>
    Inherit,
    /// <summary>
    /// Sequences of whitespace are collapsed. Newline characters in the source
    /// are handled as other whitespace. Breaks lines as necessary to fill line
    /// boxes.
    /// </summary>
    Normal,
    /// <summary>
    /// Sequences of whitespace are preserved, lines are only broken at newline
    /// characters in the source and at <br> elements.
    /// </summary>
    Pre,
    /// <summary>
    /// Collapses whitespace as for normal, but suppresses line breaks (text
    /// wrapping) within text.
    /// </summary>
    NoWrap,
    /// <summary>
    /// Sequences of whitespace are preserved. Lines are broken at newline
    /// characters, at <br>, and as necessary to fill line boxes.
    /// </summary>
    PreWrap,
    /// <summary>
    /// Sequences of whitespace are collapsed. Lines are broken at newline
    /// characters, at <br>, and as necessary to fill line boxes.
    /// </summary>
    PreLine
  }
}
