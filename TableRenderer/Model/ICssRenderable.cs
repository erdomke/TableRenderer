﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableRenderer.Writer;

namespace TableRenderer.Model
{
  public interface ICssRenderable
  {
    void ToCss(ICssWriter writer);
  }
}
