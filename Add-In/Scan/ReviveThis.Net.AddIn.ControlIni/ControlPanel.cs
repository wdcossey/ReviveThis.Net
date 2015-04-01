using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ReviveThis.AddIn.ControlPanel.Entities;
using ReviveThis.Entities;
using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.AddIn.ControlPanel
{
  #region [O5] Hidden Control Panel Items / CheckOther5Item
  [Export(typeof (IDetectionAddIn))]
  public class ControlPanel : IDetectionAddIn
  {
    #region private const(s)

    private const string INI_SECTION = "don't load";

    #endregion

    public string Author
    {
      get { return @"William David Cossey"; }
    }

    public Version Version
    {
      get { return Assembly.GetExecutingAssembly().GetName().Version; }
    }

    public string Name
    {
      get { return @"Control Panel"; }
    }

    public string[] Description
    {
      get { return new[]
      {
        "Searches for hidden Control Panel items, namely \"inetcpl.cpl\" as this is used to configure Internet Explorer settings."
      }; }
    }

    public void Dispose()
    {
      //Nothing to dispose?
    }

    public async Task<ICollection<IDetectionResultItem>> Scan()
    {
      //await Task.FromResult(0);
      
      var result = new Collection<IDetectionResultItem>();

      //"control.ini"
      //Should only be valid for Windows 95, 98, ME, 2000 and XP.
      //It is used to customize and change how the Control Panel operates and what is seen in the Control Panel.

      var path = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
      var fileName = Path.Combine(path, "control.ini");

      if (File.Exists(fileName))
      {
        using (var iniParser = new IniParser(fileName))
        {
          var keys = iniParser.EnumSection(INI_SECTION);

          if (keys.Any())
          {
            foreach (var key in keys)
            {
              //Anything hidden (that isn't "yes") should be flagged...
              var value = iniParser.GetSetting(INI_SECTION, key);
              if (!string.IsNullOrEmpty(value) && !value.Equals("yes", StringComparison.InvariantCultureIgnoreCase))
              {
                result.Add(new ControlIniResult(fileName, key, value));
              }
            }
          }
        }
      }

      return result;

    }

    public ScanResultType ResultType
    {
      get { return ScanResultType.ControlIni; }
    }
  }
  #endregion

  #region Original Visual Basic (6.0) Code Block
  /*
   Private Sub CheckOther5Item()
     Dim sControlIni$, sDummy$, sHit$
     On Error GoTo Error:
 
     sControlIni = String(255, 0)
     GetWindowsDirectory sControlIni, 255
     sControlIni = Left(sControlIni, InStr(sControlIni, Chr(0)) - 1) & "\control.ini"
     If sControlIni = "\control.ini" Then Exit Sub
     If Dir(sControlIni) = vbNullString Then Exit Sub
 
     sDummy = String(5, " ")
     'GetPrivateProfileString "don't load", "inetcpl.cpl", "", sDummy, 5, sControlIni
     IniGetString sControlIni, "don't load", "inetcpl.cpl"
     sDummy = RTrim(sDummy)
     If Right(sDummy, 1) = Chr(0) Then sDummy = Left(sDummy, Len(sDummy) - 1)
     If sDummy <> vbNullString Then
         sHit = "O5 - control.ini: inetcpl.cpl=" & sDummy
         If IsOnIgnoreList(sHit) Then Exit Sub
         frmMain.lstResults.AddItem sHit
     End If
     Exit Sub
 
   Error:
       ErrorMsg "modMain_CheckOther5Item", Err.Number, Err.Description
   End Sub
  */
  #endregion
}
