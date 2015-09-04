using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReviveThis.Entities;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.Sample.CSharp.Classes
{
  public class SampleResult : IDetectionResultItem, IDetectionItemContextMenuCollection, ICustomAddInSection
  {
    private readonly ScanResultType _resultType;
    private readonly string _text;
    private const string SECTION_ID = "ZZ";

    public SampleResult(ScanResultType type, string text)
    {
      _resultType = type;
      _text = text;
    }

    public ScanResultType ResultType
    {
      get { return _resultType; }
    }

    public string LegacyString
    {
      get { return string.Format("{0} - {1}.", SECTION_ID, _text);  }
    }

    public ICollection<IDetectionItemContextMenu> MenuItems
    {
      get
      {
        var items = new Collection<IDetectionItemContextMenu>
        {
          new ScanItemContextMenu("Sample context menu", delegate
          {
            MessageBox.Show("Hello from your Add-On Sample context menu.", "Add-On Sample (C#)",
              MessageBoxButtons.OK, MessageBoxIcon.Information);
          })
        };

        return items;
      }
    }

    public CustomAddInSection CustomAddInSection
    {
      get { return new CustomAddInSection(SECTION_ID, "Wow I have a custom section..?"); }
    }

    public bool CanRepair
    {
      get { return true; }
    }

    public bool IsChecked
    {
      get { return true; }
    }

    public async Task<IDetectionRepairResult> Repair()
    {
      //await Task.FromResult(0);

      return new SampleRepairResult();

    }
  }
}