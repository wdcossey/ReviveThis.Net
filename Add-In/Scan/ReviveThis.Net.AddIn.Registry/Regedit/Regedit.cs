using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.Registry.Regedit.Entities;
using ReviveThis.Entities;
using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.AddIn.Registry.Regedit
{

  #region [O7] Regiedit Disabled / CheckOther7Item

  [Export(typeof (IDetectionAddIn))]
  public class Regedit : IDetectionAddIn
  {
    #region private consts

    private const string VALUE_NAME = "DisableRegistryTools";
    const string REGISTRY_PATH = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";

    #endregion

    private static Collection<RegistryParser> _registryParserCollection = null;

    private static IEnumerable<RegistryParser> RegistryParsers
    {
      get
      {
        if (_registryParserCollection != null)
          return _registryParserCollection;

        return _registryParserCollection = new Collection<RegistryParser>(new[]
        {
          #region LocalMachine
          new RegistryParser(RegistryHive.LocalMachine, REGISTRY_PATH,
            new[] {RegistryView.Registry32, RegistryView.Registry64}),

          #endregion

          #region CurrentUser
          new RegistryParser(RegistryHive.CurrentUser, REGISTRY_PATH,
            new[] {RegistryView.Default}),

          #endregion
        });
      }
    }

    public string Author
    {
      get { return "William David Cossey"; }
    }

    public Version Version
    {
      get { return new Version(1, 0, 0, 0); }
    }

    public string Name
    {
      get { return "Registry Tools"; }
    }

    public string[] Description
    {
      get { return new[]
      {
        "Check if Registry editing (\"regedit.exe\") has been disabled.",
        string.Empty,
        "*Note: Registry editing could be disabled by your computer Administrator (for security purposes)."
      }; }
    }

    public void Dispose()
    {
      //Nothing to dispose.
    }

    public async Task<ICollection<IDetectionResultItem>> Scan()
    {
      //await Task.FromResult(0);
      
      var result = new Collection<IDetectionResultItem>();

      foreach (var parser in RegistryParsers)
      {
        foreach (var view in parser.RegistryViews)
        {
          using (var regKey = RegistryKey.OpenBaseKey(parser.RegistryHive, view).OpenSubKey(parser.SubKey))
          {
            if (regKey == null || regKey.ValueCount <= 0)
              continue;


            var value = regKey.GetValue(VALUE_NAME, (Int32?) null) as Int32?;

            if (value.HasValue &&
#if DEBUG
              true
#else
              value.Value.Equals(1)
#endif
              )
            {
              result.Add(new RegeditResult(parser.RegistryHive, regKey.View, regKey.Name, VALUE_NAME, value));
            }
          }
        }
      }

      return result;
    }

    public ScanResultType ResultType
    {
      get { return ScanResultType.RegeditDisabled; }
    }
  }

  #endregion

  #region Original Visual Basic (6.0) Code Block

  /*
   Private Sub CheckOther7Item()
       Dim lValue&, sHit$
       On Error GoTo Error:
     
       lValue = RegGetDword(HKEY_CURRENT_USER, "Software\Microsoft\Windows\CurrentVersion\Policies\System", "DisableRegistryTools")
       If lValue = 1 Then
           sHit = "O7 - HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\System, DisableRegedit=1"
           If Not IsOnIgnoreList(sHit) Then frmMain.lstResults.AddItem sHit
       End If
       lValue = RegGetDword(HKEY_LOCAL_MACHINE, "Software\Microsoft\Windows\CurrentVersion\Policies\System", "DisableRegistryTools")
       If lValue = 1 Then
           sHit = "O7 - HKLM\Software\Microsoft\Windows\CurrentVersion\Policies\System, DisableRegedit=1"
           If Not IsOnIgnoreList(sHit) Then frmMain.lstResults.AddItem sHit
       End If
       Exit Sub
     
   Error:
       ErrorMsg "modMain_CheckOther7Item", Err.Number, Err.Description
   End Sub
   */

  #endregion
}
