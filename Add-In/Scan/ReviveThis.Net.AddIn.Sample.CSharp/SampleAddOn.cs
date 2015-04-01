using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Sample.CSharp.Classes;

namespace ReviveThis.Sample.CSharp
{
  [Export(typeof (IDetectionAddIn))]
  public class SampleAddOn : IDetectionAddIn
  {

    public string Author
    {
      get { return @"C# Author"; }
    }

    private Version _version;

    public Version Version
    {
      get
      {
        if (_version != null)
          return _version;

        return _version = new Version(0, 0, 0, 1);
      }
    }

    public string Name
    {
      get { return @"Add-On Sample (C#)"; }
    }

    public string[] Description
    {
      get { return new[]
      {
        "A Sample Add-On for ReviveThis.Net written in C#",
        string.Empty,
         "Hey look, another line in the description."
      };}
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }

    public async Task<ICollection<IDetectionResultItem>> Scan()
    {
      //await Task.FromResult(0);

      //throw new NotImplementedException();

      var result = new List<IDetectionResultItem>
      {
        new SampleResult(ScanResultType.CustomAddIn, "This is from my Add-On Sample (C#)"),
        new SampleResult(ScanResultType.CustomAddIn, "This appears to be another result from my Add-On Sample (C#)")
      };

      return result;
    }

    public ScanResultType ResultType
    {
      get { return ScanResultType.CustomAddIn; }
    }
  }
}