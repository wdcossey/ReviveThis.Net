using System;
using System.Linq;
using System.Windows.Forms;
using ComponentOwl.BetterListView;
using ReviveThis.Entities;
using ReviveThis.Interfaces;

namespace ReviveThis.Forms
{
  public partial class FrmAddInManager : Form
  {
    public FrmAddInManager()
    {
      InitializeComponent();

      try
      {
        betterListView1.BeginUpdate();
        betterListView1.Items.Clear();

        var list = ReviveThisApplication.Default.AddIns.Items;
        if (!list.Any())
          return;

        var groupNames =
          list.Select(s => s.GetType().Assembly.ManifestModule.ScopeName).Distinct().OrderBy(o => o).ToArray();

        var groups = groupNames.Select(group => new BetterListViewGroup(@group)).ToArray();
        betterListView1.Groups.AddRange(groups);

        betterListView1.Items.AddRange(ReviveThisApplication.Default.AddIns.Items.OrderBy(o => o.Name).Select(
          item =>
            new BetterListViewItem(new[]
            {
              new BetterListViewSubItem(item.Name ?? "(Unknown)") ,
              new BetterListViewSubItem(string.Format("{0}", item.Version)) {AlignHorizontal = TextAlignmentHorizontal.Center},
              new BetterListViewSubItem(item.Author ?? "(Unknown)"),
            },
              -1,
              betterListView1.Groups
                .FirstOrDefault(f => f.Header == item.GetType().Assembly.ManifestModule.ScopeName)
              )
            {
              Tag = item,
            })
          .ToArray());
      }
      finally
      {
        betterListView1.EndUpdate();
      }
    }

    private void betterListView1_SelectedIndexChanged(object sender, EventArgs e)
    {
      textBox1.Clear();

      var senderObj = sender as BetterListView;

      if (senderObj == null || senderObj.SelectedItems.Count <= 0)
      {
        return;
      }

      var item = senderObj.SelectedItems.FirstOrDefault() ?? senderObj.FocusedItem;

      if (item == null || item.Tag == null || !(item.Tag is IAddInBase))
      {
        return;
      }

      var description = ((IAddInBase)item.Tag).Description;

      textBox1.Lines = description != null && description.Any(w => !string.IsNullOrEmpty(w)) ? ((IAddInBase)item.Tag).Description : new[] { "(none)" };
    
    }
  }
}
