using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using ReviveThis.Entities.Collections;

namespace ReviveThis.Entities.Modules
{
  public static class Uninstall
  {
    public static List<UninstallItem> ParseUninstallList()
    {

      var registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);

      try
      {
        var is64Bit = Environment.Is64BitOperatingSystem;
        var loopFor64 = false;
        var loopForCurrentUser = false;
        var result = new UninstallItemCollection();
        
      regLoop:

        using (var regKey = registryKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\"))
        {
          if (regKey != null)
          {
            var subKeys = regKey.GetSubKeyNames();
            foreach (var key in subKeys)
            {
              using (var subKey = regKey.OpenSubKey(key))
              {
                if (subKey == null)
                  continue;

                try
                {
                  var sName = subKey.GetValue("DisplayName", null) as string;
                  var sUninst = subKey.GetValue("UninstallString", null) as string;

                  if (!string.IsNullOrEmpty(sName) && !string.IsNullOrEmpty(sUninst))
                  {
                    result.Add(new UninstallItem(regKey, Path.Combine(regKey.Name, key), sName, sUninst));
                  }
                }
                catch
                {
                  continue;
                }
              }
            }
          }
        }

        if (is64Bit && !loopFor64)
        {
          registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
          loopFor64 = true;
          goto regLoop;
        }

        if (!loopForCurrentUser)
        {
          registryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default);
          loopForCurrentUser = true;
          goto regLoop;
        }

        return result.OrderBy(o => o.DisplayName).ToList();
      }
      finally 
      {
        registryKey.Close();
        registryKey.Dispose();
        
      }
      
    }

    public static bool DeleteRegistryKey(UninstallItem item)
    {
      var registryKey = RegistryKey.OpenBaseKey(item.RegistryHive, item.RegistryView);
      registryKey.DeleteSubKeyTree(item.RegistryPath, true);
      return true;
    }

    public static void ExeccuteUninstall(UninstallItem item)
    {
      var command = item.UninstallString.Trim();
      var fileName = command;
      var arguments = string.Empty;

      if (command.StartsWith("\""))
      {
        var index = command.IndexOf("\"", 1, StringComparison.InvariantCultureIgnoreCase) + 1;
        fileName = command.Substring(0, index).Trim();
        arguments = command.Substring(index, command.Length - index).Trim();
      }
      else if (command.Contains(" "))
      {
        if (!File.Exists(fileName))
        {
          var index = command.IndexOf(" ", 1, StringComparison.InvariantCultureIgnoreCase) + 1;

          fileName = command.Substring(0, index).Trim();
          arguments = command.Substring(index, command.Length - index).Trim();
        }
      }

      Process.Start(new ProcessStartInfo(fileName)
      {
        Arguments = arguments,
        WorkingDirectory = Environment.SystemDirectory,
        Verb = "open"
      });
    }

    public static void OpenControlPanel()
    {
      Process.Start(
        new ProcessStartInfo("control.exe")
        {
          Arguments = "appwiz.cpl",
          WorkingDirectory = Environment.SystemDirectory,
          Verb = "open"
        });
    }
    public static void SaveList(IEnumerable<UninstallItem> uninstallItems)
    {

      using (var dialog = new SaveFileDialog())
      {
        dialog.Title = @"Save Software list to file..";
        dialog.Filter = @"Text files (*.txt)|*.txt|All files (*.*)|*.*";
        dialog.FileName = "uninstall_list.txt";

        if (dialog.ShowDialog() != DialogResult.OK)
          return;

        var newFile = !File.Exists(dialog.FileName);

        using (
          var fileStream = new FileStream(dialog.FileName, newFile ? FileMode.CreateNew : FileMode.Truncate,
            FileAccess.Write))
        using (var writer = new StreamWriter(fileStream))
        {
          writer.Write(string.Join("\r\n", uninstallItems.Select(s => s.DisplayName).ToArray()));
        }

        if (File.Exists(dialog.FileName))
        {
          Process.Start(new ProcessStartInfo(dialog.FileName));
        }

      }

    /*
    Dim sList$, i&, sUninst$, sFile$
    If lstUninstMan.ListCount = 0 Then Exit Sub
    sFile = CmnDlgSaveFile("Save Add/Remove Software list to disk...", "Text files (*.txt)|*.txt|All files (*.*)|*.*", "uninstall_list.txt")
    If sFile = vbNullString Then Exit Sub
    For i = 0 To lstUninstMan.ListCount - 1
        sList = sList & lstUninstMan.List(i) & vbCrLf
    Next i
    
    Open sFile For Output As #1
        Print #1, sList
    Close #1
    ShellExecute 0, "open", "notepad.exe", sFile, vbNullString, 1
    */

      //Process.Start(
      //  new ProcessStartInfo("control.exe")
      //  {
      //    Arguments = "appwiz.cpl",
      //    WorkingDirectory = Environment.SystemDirectory,
      //    Verb = "open"
      //  });
    }
  }
}