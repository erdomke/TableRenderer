using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableRenderer.Model
{
  public class Hyperlink : Uri, IHyperlink
  {
    public Uri Target { get { return this; } }
    public string Text { get; set; }

    public Hyperlink(string uriString) : base(uriString) { }
    public Hyperlink(string uriString, UriKind kind) : base(uriString, kind) { }
    public Hyperlink(Uri baseUri, string relativeUri) : base(baseUri, relativeUri) { }


    public static implicit operator string(Hyperlink value)
    {
      return value.ToString();
    }
    public static implicit operator Hyperlink(string value)
    {
      return new Hyperlink(value);
    }
  }
}
