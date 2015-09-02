using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ReviveThis.AddIn.UefiBinary.LenovoServiceEngine.Entities;
using ReviveThis.AddIn.UefiBinary.LenovoServiceEngine.Enums;
using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.AddIn.UefiBinary.LenovoServiceEngine
{
  #region
  [Export(typeof(IDetectionAddIn))]
  public class LenovoServiceEngine : IDetectionAddIn
  {
    public string Author
    {
      get { return @"William David Cossey / Mark van Tilburg"; }
    }

    private Version _version = null;

    public Version Version
    {
      get
      {
        if (_version != null)
          return _version;

        return _version = new Version(1, 0, 0, 0);
      }
    }

    public string Name
    {
      get { return @"Lenovo Service Engine (LSE)"; }
    }

    private string[] _description = new string[0];

    public string[] Description
    {
      get
      {
        if (_description.Any())
          return _description;

        return
          _description =
            new[] { String.Format("Scans \"{0}\" for Lenovo Service Engine (LSE) file(s).", Environment.GetFolderPath(Environment.SpecialFolder.System)) };
      }
    }

    public async Task<ICollection<IDetectionResultItem>> Scan()
    {


      var result = new List<IDetectionResultItem>();

      var lseBinaryList =
        Directory.EnumerateFiles(Environment.GetFolderPath(Environment.SpecialFolder.System), "*.exe", SearchOption.TopDirectoryOnly)
          .Where(w => w.EndsWith("LSEDT.exe") || w.EndsWith("LSEPreDownloader.exe")).ToList();

      foreach (var fileName in lseBinaryList)
      {
        result.Add(new LenovoServiceEngineResult(LenovoServiceEngineTypes.Binary, fileName, fileName));
      }


      var lseLogList = Directory.EnumerateFiles(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "*.log", SearchOption.TopDirectoryOnly)
          .Where(w => w.EndsWith("lseupload.log"));


      foreach (var fileName in lseLogList)
      {
        result.Add(new LenovoServiceEngineResult(LenovoServiceEngineTypes.Log, fileName, fileName));
      }


      return result;
    }

    public ScanResultType ResultType
    {
      get { return ScanResultType.CustomAddIn; }
    }

    public void Dispose()
    {
      //Nothing to dispose?
    }
  }
  #endregion

}
