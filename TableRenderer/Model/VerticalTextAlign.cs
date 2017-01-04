using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableRenderer.Model
{
  /// <summary>
  /// Specifies the vertical alignment of an inline.
  /// </summary>
  public enum VerticalTextAlign
  {
    /// <summary>
    /// Use the style of the first parent where it is set
    /// </summary>
    Inherit,
    /// <summary>
    /// Aligns the baseline of the element with the baseline of its parent. The
    /// baseline of some replaced elements, like <textarea>, is not specified
    /// by the HTML specification, meaning that their behavior with this
    /// keyword may change from one browser to the other.
    /// </summary>
    Baseline,
    /// <summary>
    /// Aligns the baseline of the element with the subscript-baseline of its
    /// parent.
    /// </summary>
    Sub,
    /// <summary>
    /// Aligns the baseline of the element with the superscript-baseline of
    /// its parent.
    /// </summary>
    Super
  }
}
