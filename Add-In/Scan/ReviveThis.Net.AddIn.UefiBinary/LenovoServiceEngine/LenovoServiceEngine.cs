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


      var lseServiceList = System.ServiceProcess.ServiceController.GetServices().Where(w => w.ServiceName == "LSEDT");

      foreach (var service in lseServiceList)
      {
        result.Add(new LenovoServiceEngineResult(LenovoServiceEngineTypes.Service, service.ServiceName,
          string.Format("{0} ({1})", service.DisplayName, service.Status)));
      }

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

      var lsePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Lenovo\\LSE");

      if (Directory.Exists(lsePath))
      {
        result.Add(new LenovoServiceEngineResult(LenovoServiceEngineTypes.Directory, lsePath, lsePath));
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

  #region RemoveLSEDT.bat by Lenovo Corp.
  /*
    @echo off

    rem This batch script will remove all files and programs related to LSE windows part.
    rem c:\windows\system32\LSEDT.exe is the main service.
    rem c:\windows\system32\LSEPreDownloader.exe is the function-related program.
    rem C:\Windows\lseupload.log store the MTM and UUID of the machine.
    rem C:\ProgramData\Lenovo\LSE is the directory used to store the log files.


    for /f "skip=3 tokens=4" %%i in ('sc query LSEDT') do set "status=%%i" goto :next

    :next
    if /i "%status%"=="RUNNING" (goto r)
    if /i "%status%"=="STOPPED" (goto s)

    :r
    net stop LSEDT
    sc delete LSEDT
    goto common

    :s
    sc delete LSEDT
    goto common

    :common
    if exist C:\Windows\System32\LSEDT.exe (del C:\Windows\System32\LSEDT.exe)
    if exist C:\Windows\System32\LSEPreDownloader.exe (del C:\Windows\System32\LSEPreDownloader.exe)

    if exist C:\Windows\lseupload.log (del C:\Windows\lseupload.log)
    rd /q /s C:\ProgramData\Lenovo\LSE

    ver|find "6.1.">nul && goto win7
    exit

    :win7
    if exist C:\Windows\System32\0409\zz_sec\autobin.exe (
	    copy /Y C:\Windows\System32\0409\zz_sec\autobin.exe C:\Windows\System32\autochk.exe
	    )
    exit
  */
  #endregion
}
