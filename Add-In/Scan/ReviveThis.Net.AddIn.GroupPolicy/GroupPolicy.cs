using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.GroupPolicy.Entities;
using ReviveThis.AddIn.GroupPolicy.Enums;
using ReviveThis.Entities;
using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.AddIn.GroupPolicy
{

  #region [06] CheckOther6Item

  [Export(typeof(IDetectionAddIn))]
  public class GroupPolicy : IDetectionAddIn
  {

    #region private
    #region consts
    private const string IE_RESTRICTIONS = @"Software\Policies\Microsoft\Internet Explorer\Restrictions";
    private const string IE_CONTROL_PANEL = @"Software\Policies\Microsoft\Internet Explorer\Control Panel";
    private const string IE_TOOLBARS_RESTRICTIONS = @"Software\Policies\Microsoft\Internet Explorer\Toolbars\Restrictions";
    #endregion

    #region Group Policy Registry Parsers
    private static Tuple<RegistryParser, GroupPolicyResultType>[] _policiesRegistryParsers;

    private static IEnumerable<Tuple<RegistryParser, GroupPolicyResultType>> PoliciesRegistryParsers
    {
      get
      {
        if (_policiesRegistryParsers != null && _policiesRegistryParsers.Any())
          return _policiesRegistryParsers;

        return _policiesRegistryParsers = new[]
        {
          #region LocalMachine
          new Tuple<RegistryParser, GroupPolicyResultType>(
            new RegistryParser(RegistryHive.LocalMachine, IE_RESTRICTIONS,
              new[] {RegistryView.Registry32, RegistryView.Registry64}), GroupPolicyResultType.InternetExplorer),

          new Tuple<RegistryParser, GroupPolicyResultType>(
            new RegistryParser(RegistryHive.LocalMachine, IE_CONTROL_PANEL,
              new[] {RegistryView.Registry32, RegistryView.Registry64}),
            GroupPolicyResultType.InternetExplorerControlPanel),

          new Tuple<RegistryParser, GroupPolicyResultType>(
            new RegistryParser(RegistryHive.LocalMachine, IE_TOOLBARS_RESTRICTIONS,
              new[] {RegistryView.Registry32, RegistryView.Registry64}),
            GroupPolicyResultType.InternetExplorerToolbars),

          #endregion

          #region CurrentUser

          new Tuple<RegistryParser, GroupPolicyResultType>(
            new RegistryParser(RegistryHive.CurrentUser, IE_RESTRICTIONS,
              new[] {RegistryView.Registry32}), GroupPolicyResultType.InternetExplorer),

          new Tuple<RegistryParser, GroupPolicyResultType>(
            new RegistryParser(RegistryHive.CurrentUser, IE_CONTROL_PANEL,
              new[] {RegistryView.Registry32}),
            GroupPolicyResultType.InternetExplorerControlPanel),

          new Tuple<RegistryParser, GroupPolicyResultType>(
            new RegistryParser(RegistryHive.CurrentUser, IE_TOOLBARS_RESTRICTIONS,
              new[] {RegistryView.Registry32}),
            GroupPolicyResultType.InternetExplorerToolbars),

          #endregion
        };
      }
    }
    #endregion
    #endregion

    public string Author
    {
      get { return @"William David Cossey"; }
    }

    private Version _version;

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
      get { return @"Group Policy Settings"; }
    }

    public string[] Description
    {
      get
      {
        return new[]
      {
        "Group policies are used to control the working environment of user accounts and computer accounts.",
        string.Empty,
        "For more information visit: http://en.wikipedia.org/wiki/Group_Policies",
         string.Empty,
         "Supported group policies: Internet Explorer."
      };
      }
    }

    public void Dispose()
    {
      //Nothing to dispose?
    }

    public async Task<ICollection<IDetectionResultItem>> Scan()
    {
      //await Task.FromResult(0);
      
      var result = new Collection<IDetectionResultItem>();

      foreach (var item in PoliciesRegistryParsers)
      {
        foreach (var view in item.Item1.RegistryViews)
        {
          using (var regKey = RegistryKey.OpenBaseKey(item.Item1.RegistryHive, view).OpenSubKey(item.Item1.SubKey, false))
          {
            if (regKey == null || regKey.ValueCount <= 0)
              continue;

            var valueNames = regKey.GetValueNames();

            foreach (var valueName in valueNames/*.Where(w => w.StartsWith("No", StringComparison.InvariantCultureIgnoreCase))*/)
            {
              var valueKind = regKey.GetValueKind(valueName);
              if (valueKind == RegistryValueKind.DWord)
              {
                var value = regKey.GetValue(valueName, (Int32?) null) as Int32?;
                if (value.HasValue)
                {
                  result.Add(new GroupPolicyResult(item.Item2, item.Item1.RegistryHive, regKey.View, regKey.Name,
                    valueName, value.Value.Equals(1)));
                }
              }
            }

            //foreach (var valueName in valueNames.Where(w => !w.StartsWith("No", StringComparison.InvariantCultureIgnoreCase)))
            //{
            //  var valueKind = regKey.GetValueKind(valueName);
            //  if (valueKind == RegistryValueKind.DWord)
            //  {
            //    var value = regKey.GetValue(valueName, (Int32?)null) as Int32?;
            //    if (value.HasValue && value.Value.Equals(0))
            //    {
            //      result.Add(new GroupPolicyResult(item.Item2, item.Item1.RegistryHive, regKey.View, regKey.Name,
            //        valueName, value));
            //    }
            //  }
            //}
            //result.Add(new GroupPolicyResult(item.Item2, item.Item1.RegistryHive, regKey.View, regKey.Name));

          }
        }
      }

      return result;
    }

    public ScanResultType ResultType
    {
      get { return ScanResultType.GroupPolicySettings; }
    }
  }

  #endregion

  #region Original Visual Basic (6.0) Code Block

  /*
    Private Sub CheckOther6Item()
        'HKEY_CURRENT_USER\ software\ policies\ microsoft\
        'internet explorer. If there are sub folders called
        '"restrictions" and/or "control panel", delete them
    
        Dim sHit$
        On Error GoTo Error:
        If RegKeyExists(HKEY_CURRENT_USER, "Software\Policies\Microsoft\Internet Explorer\Restrictions") And _
           RegKeyHasValues(HKEY_CURRENT_USER, "Software\Policies\Microsoft\Internet Explorer\Restrictions") Then
            sHit = "O6 - HKCU\Software\Policies\Microsoft\Internet Explorer\Restrictions present"
            If Not IsOnIgnoreList(sHit) Then frmMain.lstResults.AddItem sHit
        End If
        If RegKeyExists(HKEY_CURRENT_USER, "Software\Policies\Microsoft\Internet Explorer\Control Panel") And _
           RegKeyHasValues(HKEY_CURRENT_USER, "Software\Policies\Microsoft\Internet Explorer\Control Panel") Then
            sHit = "O6 - HKCU\Software\Policies\Microsoft\Internet Explorer\Control Panel present"
            If Not IsOnIgnoreList(sHit) Then frmMain.lstResults.AddItem sHit
        End If
        If RegKeyExists(HKEY_CURRENT_USER, "Software\Policies\Microsoft\Internet Explorer\Toolbars\Restrictions") And _
           RegKeyHasValues(HKEY_CURRENT_USER, "Software\Policies\Microsoft\Internet Explorer\Toolbars\Restrictions") Then
            sHit = "O6 - HKCU\Software\Policies\Microsoft\Internet Explorer\Toolbars\Restrictions present"
            If Not IsOnIgnoreList(sHit) Then frmMain.lstResults.AddItem sHit
        End If
    
        If RegKeyExists(HKEY_LOCAL_MACHINE, "Software\Policies\Microsoft\Internet Explorer\Restrictions") And _
           RegKeyHasValues(HKEY_LOCAL_MACHINE, "Software\Policies\Microsoft\Internet Explorer\Restrictions") Then
            sHit = "O6 - HKLM\Software\Policies\Microsoft\Internet Explorer\Restrictions present"
            If Not IsOnIgnoreList(sHit) Then frmMain.lstResults.AddItem sHit
        End If
        If RegKeyExists(HKEY_LOCAL_MACHINE, "Software\Policies\Microsoft\Internet Explorer\Control Panel") And _
           RegKeyHasValues(HKEY_LOCAL_MACHINE, "Software\Policies\Microsoft\Internet Explorer\Control Panel") Then
            sHit = "O6 - HKLM\Software\Policies\Microsoft\Internet Explorer\Control Panel present"
            If Not IsOnIgnoreList(sHit) Then frmMain.lstResults.AddItem sHit
        End If
        If RegKeyExists(HKEY_LOCAL_MACHINE, "Software\Policies\Microsoft\Internet Explorer\Toolbars\Restrictions") And _
           RegKeyHasValues(HKEY_LOCAL_MACHINE, "Software\Policies\Microsoft\Internet Explorer\Toolbars\Restrictions") Then
            sHit = "O6 - HKLM\Software\Policies\Microsoft\Internet Explorer\Toolbars\Restrictions present"
            If Not IsOnIgnoreList(sHit) Then frmMain.lstResults.AddItem sHit
        End If
        Exit Sub
    
    Error:
        ErrorMsg "modMain_CheckOther6Item", Err.Number, Err.Description
    End Sub

   */
  #endregion

}