using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ReviveThis.Entities;
using ReviveThis.Entities.Modules;

namespace ReviveThis.Controls
{
  public partial class UninstallManager : UserControl
  {
    private List<UninstallItem> _installedList = null;

    public UninstallManager()
    {
      InitializeComponent();

      this.Load += OnLoad;


      btnRefresh.Click += RefreshOnClick;
      btnSave.Click += SaveOnClick;
      btnOpen.Click += OpenOnClick;
      btnRun.Click += RunOnClick;
      btnDelete.Click += DeleteOnClick;
      btnEdit.Click += EditOnClick;

      lstApplications.KeyPress += HostsOnKeyPress;
      lstApplications.SelectedIndexChanged += ApplicationsSelectedIndexChanged;

      RefreshOnClick(btnRefresh, new EventArgs());

      //HostsSelectedIndexChanged(lstApplications, new EventArgs());
    }

    private void EditOnClick(object sender, EventArgs eventArgs)
    {
      if (lstApplications == null || lstApplications.SelectedItem == null || !(lstApplications.SelectedItem is UninstallItem))
      {
        return;
      }

      var item = lstApplications.SelectedItem as UninstallItem;

      //Microsoft.VisualBasic.Interaction.InputBox("", "Title", item.UninstallString, -1, -1);

      //if (
      //  MessageBox.Show(
      //    "Deleting entries cannot be undone.\n\nAre you sure you want to delete the selected item?",
      //    string.Format("Delete - \"{0}\"", item.DisplayName), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
      //    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
      //{
      //  if (Uninstall.DeleteRegistryKey(item))
      //  {
      //    lstApplications.Items.Remove(lstApplications.SelectedItem);
      //  }
      //}
    }

    private void DeleteOnClick(object sender, EventArgs eventArgs)
    {
      if (lstApplications == null || lstApplications.SelectedItem == null || !(lstApplications.SelectedItem is UninstallItem))
      {
        return;
      }

      var item = lstApplications.SelectedItem as UninstallItem;
      if (
        MessageBox.Show(
          "Deleting entries cannot be undone.\n\nAre you sure you want to delete the selected item?",
          string.Format("Delete - \"{0}\"", item.DisplayName), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
          MessageBoxDefaultButton.Button2) == DialogResult.Yes)
      {
        if (Uninstall.DeleteRegistryKey(item))
        {
          lstApplications.Items.Remove(lstApplications.SelectedItem);
        }
      }
    }

    private void RunOnClick(object sender, EventArgs eventArgs)
    {
      if (lstApplications == null || lstApplications.SelectedItem == null || !(lstApplications.SelectedItem is UninstallItem))
      {
        return;
      }

      var item = lstApplications.SelectedItem as UninstallItem;
      if (MessageBox.Show("Please note that some uninstallers run automatically without any user intervention (once started they cannot be interrupted).\n\nAre you sure you want to run the file?", string.Format("Remove - \"{0}\"", item.DisplayName), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
        Uninstall.ExeccuteUninstall(item);
    }

    private void OpenOnClick(object sender, EventArgs eventArgs)
    {
      Uninstall.OpenControlPanel();
    }

    private void ApplicationsSelectedIndexChanged(object sender, EventArgs eventArgs)
    {
      var listBox = sender as ListBox;
      if (listBox == null || listBox.SelectedItem == null || !(listBox.SelectedItem is UninstallItem))
      {
        return;
      }

      var item = listBox.SelectedItem as UninstallItem;

      txtName.Text = item.DisplayName;
      txtCommand.Text = item.UninstallString;

    }

    private void HostsOnKeyPress(object sender, KeyPressEventArgs keyPressEventArgs)
    {
      //if (keyPressEventArgs.KeyChar == (char)Keys.Escape)
      //{
      //  lstApplications.ClearSelected();
      //}
    }

    private void SaveOnClick(object sender, EventArgs eventArgs)
    {
      Uninstall.SaveList(lstApplications.Items.Cast<UninstallItem>());
    }

    private void OnLoad(object sender, EventArgs eventArgs)
    {

    }

    private void RefreshOnClick(object sender, EventArgs eventArgs)
    {
      try
      {
        _installedList = Uninstall.ParseUninstallList().ToList();

        if (_installedList == null || !_installedList.Any()) 
          return;

        lstApplications.BeginUpdate();
        lstApplications.DataSource = null;
        lstApplications.DataSource = _installedList;
        lstApplications.DisplayMember = "FancyName";
        lstApplications.EndUpdate();
      }
      catch (Exception)
      {

        throw;
      }
      finally
      {

      }
    }

  }
}
