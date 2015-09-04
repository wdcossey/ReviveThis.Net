using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ReviveThis.AddIn.AutoRun.AutoRun.Interfaces;
using ReviveThis.Entities;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Entities.PInvoke;
using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.AddIn.AutoRun.AutoRun.Entities
{
  public class AutoRunStartUpResult : IAutoRunStartUpResult, IDetectionItemToolTip
  {
    public ScanResultType ResultType { get; private set; }

    private string _string;
    private bool _validShortCut;

    public string LegacyString
    {
      get
      {
        if (!string.IsNullOrEmpty(_string))
          return _string;

        return _string = string.Format("{0} - {1}: [{2}] {3}", ResultType.FormatToString(), FancyName,
          System.IO.Path.GetFileName(ShortCut), FileName);
      }
    }

    public ICollection<IDetectionItemContextMenu> ContextMenus { get; private set; }

    public string Path { get; private set; }
    public string ShortCut { get; private set; }
    public string FileName { get; set; }
    public string RealFileName { get; private set; }
    public string FancyName { get; private set; }
    public string Parameters { get; private set; }

    public AutoRunStartUpResult()
    {
      FileName = null;
      ResultType = ScanResultType.AutoRun;
    }

    public AutoRunStartUpResult(string fancyName, string path, string shortCut, string fileName,
      bool validShortCut = false)
      : this()
    {
      FancyName = fancyName;
      Path = path;
      ShortCut = shortCut;
      FileName = fileName;
      _validShortCut = validShortCut;
    }

    private IDetectionItemContextMenu[] _menuItems;

    public ICollection<IDetectionItemContextMenu> MenuItems
    {
      get
      {
        if (_menuItems != null && _menuItems.Any())
          return _menuItems;

        return _menuItems =
          new IDetectionItemContextMenu[]
          {
            new ScanItemContextMenu
              (
                "Locate...",
                null,
                new IDetectionItemContextMenu[]
                {
                  new ScanItemContextMenu("File",
                    (sender, args) =>
                      Process.Start(new ProcessStartInfo("explorer.exe", string.Format("/select, \"{0}\"", this.FileName))
                      {
                        Verb = "open"
                      })),
                  new ScanItemContextMenu("Shortcut",
                    (sender, args) =>
                      Process.Start(new ProcessStartInfo("explorer.exe", string.Format("/select, \"{0}\"", this.ShortCut))
                      {
                        Verb = "open"
                      })) {Enabled = _validShortCut},
                }
              ),
            new ScanItemContextMenu(true),
            new ScanItemContextMenu("&Properties", (sender, args) => Shell32.ShowFileProperties(this.FileName))

          };
      }
    }

    public string ToolTip
    {
      get
      {
        return
          string.Format("{0}\n\nFile : {1}", ResultType.FormatToString(true),
          FileName);
      }
    }

    public bool CanRepair
    {
      get { return false; }
    }

    public bool IsChecked
    {
      get { return false; }
    }

    public async Task<IDetectionRepairResult> Repair()
    {
      //await Task.FromResult(0);
      return null;
    }
  }
}