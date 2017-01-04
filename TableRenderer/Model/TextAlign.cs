using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableRenderer.Model
{
  public enum TextAlign
  {
    Inherit,
    Left,
    Right,
    Center,
    Justify,
    Fill,
    Distributed,
    /// <summary>
    /// Center align text across multiple non-merged cells
    /// </summary>
    CenterContinuous
  }
}
