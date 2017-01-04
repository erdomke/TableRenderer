using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableRenderer.Model
{
  [Flags()]
  public enum TextDecoration
  {
    Inherit = 0,
    /// <summary>
    /// Produces no text decoration.
    /// </summary>
    None = 1,
    /// <summary>
    /// Each line of text is underlined.
    /// </summary>
    Underline = 2,
    /// <summary>
    /// Each line of text has a line above it.
    /// </summary>
    Overline = 4,
    /// <summary>
    /// Each line of text has a line through the middle.
    /// </summary>
    LineThrough = 8,
    /// <summary>
    /// The text blinks (alternates between visible and invisible).
    /// </summary>
    Blink = 16,
    /// <summary>
    /// The specified decoration is rendered as two lines
    /// </summary>
    Double = 32
  }
}
