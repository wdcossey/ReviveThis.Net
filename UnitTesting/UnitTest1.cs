using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviveThis.Consts;
using ReviveThis.Entities.Modules;

namespace UnitTesting
{
  [TestClass]
  public class UselesssTest
  {
    [TestMethod]
    public void TestExpansion()
    {
      //Debug.WriteLine(Environment.SpecialFolder.Windows ExpandVariables(@"%MyDocuments%\Foo"););
    }
  }

  [TestClass]
  public class UtilsTest
  {
    [TestMethod]
    public void GetChromeVersion()
    {
      Debug.WriteLine(Utils.GetChromeVersion("Google Chrome: "));
    }

    [TestMethod]
    public void GetFireFoxVersion()
    {
      Debug.WriteLine(Utils.GetFireFoxVersion("Mozilla FireFox: "));
    }

    [TestMethod]
    public void GetOperaVersion()
    {
      Debug.WriteLine(Utils.GetOperaVersion("Opera: "));
    }
  }

  [TestClass]
  public class HostsTest
  {
    [TestMethod]
    public void ListHostsFile()
    {
      var result = Hosts.ListHostsFile();
      
      if (result == null) 
        return;

      foreach (var line in result.Result.Lines)
      {
        Debug.WriteLine(line);
      }
    }

    [TestMethod]
    public void CreateDefaultHostsFile()
    {
      Hosts.CreateDefaultHostsFile();
    }

    [TestMethod]
    public void HostsDeleteLine()
    {
      var hostsFile = Hosts.ListHostsFile().Result;
      Hosts.HostsDeleteLine(ref hostsFile, new[] { 0, 1, 2 }, "file.txt");
    }

    [TestMethod]
    public void HostsToggleLine()
    {
      var hostsFile = Hosts.ListHostsFile().Result;
      Hosts.HostsToggleLine(ref hostsFile, new[] { 0, 1, 2 }, "file.txt");
    }
  }

  [TestClass]
  public class ProcessManagerTest
  {
    [TestMethod]
    public void RefreshProcessList()
    {
      var result = ProcessManager.RefreshProcessList();

      if (result == null) 
        return;

      foreach (var process in result.OrderBy(o => o.Id))
      {
        try
        {
          Debug.WriteLine(string.Format("{0}\t{1}", process.Id, process.MainModule.FileName));
        }
        catch (Exception ex)
        {
          //throw;
        }
          
      }
    }

    [TestMethod]
    public void KillProcess()
    {
      ProcessManager.KillProcess(592);
    }

    [TestMethod]
    public void RefreshDllList()
    {
      var result = ProcessManager.RefreshDllList(101010101);
      if (result == null) 
        return;

      foreach (var module in result.Cast<ProcessModule>().OrderBy(o => o.FileName))
      {
        Debug.WriteLine(module.FileName);
      }
    }

    [TestMethod]
    public void SaveProcessList()
    {
      ProcessManager.SaveProcessList(ProcessManager.RefreshProcessList());
    }

    [TestMethod]
    public void CopyProcessList()
    {
      ProcessManager.CopyProcessList(ProcessManager.RefreshProcessList());
    }

    [TestMethod]
    public void ShowFileProperties()
    {
      ProcessManager.ShowFileProperties("notepad.exe");
    }
  }

  [TestClass]
  public class InternetTest
  {

    [TestMethod]
    public void CheckForUpdate()
    {
      Internet.CheckForUpdate();;
    }

  }

  [TestClass]
  public class UninstallTest
  {

    [TestMethod]
    public void ParseUninstallList()
    {
      Debug.WriteLine(string.Join("\r\n", Uninstall.ParseUninstallList().Select(s => s.FancyName)));

    }

  }

  [TestClass]
  public class PInvoke
  {

    [TestMethod]
    public void Shell32()
    {
      //ReviveThis.Entities.PInvoke.Privilege.CallExitWindowsEx(); Shell32.SHRestartSystemMB(0, null, 0);
    }

    [TestMethod]
    public void StringLoader()
    {
      using (var x = new StringLoader(@"C:\Windows\WindowsMobile\INetRepl.dll"))
      {
        var s = x.Load(223);
        Debug.WriteLine(s);
      }
    }

  }

  [TestClass]
  public class EncryptTest
  {

    [TestMethod]
    public void Crypt()
    {
      Debug.WriteLine(Encrypt.Crypt(@"HKCU\Software\Microsoft\Internet Explorer,Default_Page_URL,,", @"THOU SHALT NOT STEAL"));
    }
    
    [TestMethod]
    public void DeCrypt()
    {
      const string decryptKey = @"THOU SHALT NOT STEAL";

      foreach (var item in SafeDomains.Array)
      {
        Debug.WriteLine(Encrypt.Decrypt(item, decryptKey));
      }

      foreach (var item in RegistryValues.Array)
      {
        Debug.WriteLine(Encrypt.Decrypt(item, decryptKey));
      }

      foreach (var item in SafeProtocols.Array)
      {
        Debug.WriteLine(Encrypt.Decrypt(item, decryptKey));
      }
    }

  }
}
