using Microsoft.VisualStudio.TestTools.UnitTesting;
using TableRenderer.Writer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TableRenderer.Model;
using System.IO.Compression;

namespace TableRenderer.Writer.Tests
{
  [TestClass()]
  public class ExcelWriterTests
  {
    private void RunTest(Action<ITableWriter> action, string expectedHash)
    {
      var generateFile = false;

      using (var stream = generateFile
        ? (Stream)new FileStream(@"C:\Users\eric.domke\Documents\test.xlsx", FileMode.Create, FileAccess.ReadWrite)
        : new MemoryStream())
      using (var uncompressed = new MemoryStream())
      using (var writer = new ExcelWriter(stream))
      using (var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider())
      {
        action.Invoke(writer);
        writer.Close();

        if (!generateFile)
        {
          stream.Position = 0;
          var archive = new ZipArchive(stream);
          foreach (var entry in archive.Entries.OrderBy(e => e.FullName))
          {
            entry.Open().CopyTo(uncompressed);
          }
          uncompressed.Position = 0;
          var hash = Convert.ToBase64String(md5.ComputeHash(uncompressed));
          Assert.AreEqual(expectedHash, hash);
        }
      }
    }

    [TestMethod()]
    public void DataTypes_Excel()
    {
      RunTest(TableSample.DataTypes, "qzmYk1PlpPgkKSa8HG0gag==");
    }

    [TestMethod()]
    public void MergedCells_Excel()
    {
      RunTest(TableSample.MergedCells, "8+V6cjVnoAIVQBloYjgjcQ==");
    }

    [TestMethod()]
    public void Sheets_Excel()
    {
      RunTest(TableSample.MultipleSheets, "lwXjVSyizrdtHRedfsiRng==");
    }

    [TestMethod()]
    public void Styles_Excel()
    {
      RunTest(TableSample.StyleTest, "Cplw4nDi2CRCMKJTLAudbQ==");
    }
  }
}
