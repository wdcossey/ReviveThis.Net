using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ReviveThis.AddIn.UefiBinary.UefiBinary.Entities;
using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.AddIn.UefiBinary.UefiBinary
{
  #region
  [Export(typeof(IDetectionAddIn))]
  public class UefiBinary : IDetectionAddIn
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
      get { return @"Microsoft Windows Platform Binary Table"; }
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
            new[] { "Scans the system event log for UEFI Binary Loaders." };
      }
    }

    public async Task<ICollection<IDetectionResultItem>> Scan()
    {
      //EventLog.CreateEventSource("Microsoft-Windows-Subsys-SMSS", "MyNewLog");
      //EventLog myLog = new EventLog("System", ".", "Microsoft-Windows-Subsys-SMSS");
      //myLog.WriteEntry("A platform binary was successfully executed.");

      var result = new List<IDetectionResultItem>();

      var eventLog = new EventLog("System", ".", "Microsoft-Windows-Subsys-SMSS");

      foreach (EventLogEntry entry in eventLog.Entries.Cast<EventLogEntry>().Where(w => w.Source == "Microsoft-Windows-Subsys-SMSS"))
      {
        //Console.WriteLine(entry.Message);

        result.Add(new UefiBinaryResult(ScanResultType.CustomAddIn, entry.Message));
        
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
