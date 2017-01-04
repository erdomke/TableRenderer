using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableRenderer.Model
{
  /// <summary>
  /// Specifies the vertical alignment of table-cell box.
  /// </summary>
  public enum VerticalAlign
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
    /// Aligns the middle of the element with the baseline plus half the
    /// x-height of the parent.
    /// </summary>
    Middle,
    /// <summary>
    /// Align the top of the element and its descendants with the top of the
    /// entire line.
    /// </summary>
    Top,
    /// <summary>
    /// Align the bottom of the element and its descendants with the bottom
    /// of the entire line.
    /// </summary>
    Bottom,
    /// <summary>
    /// Justify the text vertically
    /// </summary>
    Justify,
    /// <summary>
    /// Distribute the text vertically
    /// </summary>
    Distributed
  }
}
