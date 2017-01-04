using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableRenderer.Writer
{
  public class CssWriter : ICssWriter
  {
    private bool _needsPropClose;
    private TextWriter _writer;

    public bool NewLineBetweenProperties { get; set; }

    public CssWriter()
    {
      _writer = new StringWriter();
    }
    public CssWriter(TextWriter writer)
    {
      _writer = writer;
    }
    public CssWriter(StringBuilder writer)
    {
      _writer = new StringWriter(writer);
    }
    public CssWriter(Stream writer)
    {
      _writer = new StreamWriter(writer);
    }

    public void Close()
    {
      if (_needsPropClose)
      {
        _writer.Write(";");
        _needsPropClose = false;
      }
    }

    public void Property(string name)
    {
      if (_needsPropClose)
      {
        _writer.Write(";");
        if (NewLineBetweenProperties)
          _writer.WriteLine();
      }
      _writer.Write(name);
      _writer.Write(": ");
      _needsPropClose = true;
    }

    public void Value(char value)
    {
      _writer.Write(value);
    }
    public void Value(string value)
    {
      _writer.Write(value);
    }

    public override string ToString()
    {
      if (_writer is StringWriter)
        return _writer.ToString();
      return base.ToString();
    }

    public void Dispose()
    {
      Close();
    }
  }
}
