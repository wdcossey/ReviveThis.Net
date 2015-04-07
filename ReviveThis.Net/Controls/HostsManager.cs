using System;
using System.Linq;
using System.Windows.Forms;
using ReviveThis.Entities;
using ReviveThis.Entities.Modules;

namespace ReviveThis.Controls
{
  public partial class HostsManager : UserControl
  {
    private HostsFileReader _hostsFile = null;

    public HostsManager()
    {
      InitializeComponent();

      this.Load += OnLoad;

      btnNotepad.Click += OpenNotepad;
      btnDelete.Click += DeleteLines;
      btnToggle.Click += ToggleLines;
      muiElevated.Click += ElevatedOnClick;

      lstHosts.KeyPress += HostsOnKeyPress;
      lstHosts.SelectedIndexChanged += HostsSelectedIndexChanged;

      if (!this.DesignMode)
        //Load the Hosts file.
        _hostsFile = Hosts.ListHostsFile().Result;

      if (_hostsFile != null && _hostsFile.LineCount > 0)
      {
        lstHosts.BeginUpdate();
        lstHosts.Items.Clear();
        lstHosts.Items.AddRange(_hostsFile.Lines.ToArray());
        lstHosts.EndUpdate();

        lblCaption.Text = string.Format("Hosts file is located at: {0} ({1} lines, {2})", _hostsFile.Location,
          _hostsFile.LineCount, _hostsFile.FormattedAttributes);
      }

      HostsSelectedIndexChanged(lstHosts, new EventArgs());
    }

    private void ElevatedOnClick(object sender, EventArgs eventArgs)
    {
      Hosts.OpenInNotepad(true);
    }

    private void HostsSelectedIndexChanged(object sender, EventArgs eventArgs)
    {
      var listBox = sender as ListBox;
      if (listBox == null)
      {
        return;
      }

      var count = listBox.SelectedItems.Count;

      btnDelete.Enabled = count > 0;
      btnDelete.Text = string.Format("Delete ({0}) line{1}", count, count != 1 ? "s" : string.Empty);

      btnToggle.Enabled = count > 0;
      btnToggle.Text = string.Format("Toggle ({0}) line{1}", count, count != 1 ? "s" : string.Empty);

    }

    private void HostsOnKeyPress(object sender, KeyPressEventArgs keyPressEventArgs)
    {
      if (keyPressEventArgs.KeyChar == (char)Keys.Escape)
      {
        lstHosts.ClearSelected();
      }
    }

    private void ToggleLines(object sender, EventArgs eventArgs)
    {
      try
      {
        var selectedIndices = lstHosts.SelectedIndices.Cast<int>().OrderBy(o => o).ToArray();

        Hosts.HostsToggleLine(ref _hostsFile, selectedIndices);
        lstHosts.BeginUpdate();
        lstHosts.Items.Clear();
        lstHosts.Items.AddRange(_hostsFile.Lines);

        foreach (var index in selectedIndices)
        {
          lstHosts.SetSelected(index, true);
        }
      }
      catch (UnauthorizedAccessException ex)
      {
        throw;
      }
      catch (Exception)
      {

        throw;
      }
      finally
      {
        lstHosts.EndUpdate();
      }
    }

    private void OnLoad(object sender, EventArgs eventArgs)
    {

    }

    private void DeleteLines(object sender, EventArgs eventArgs)
    {
      try
      {
        var selectedIndices = lstHosts.SelectedIndices.Cast<int>().OrderBy(o => o).ToArray();

        Hosts.HostsDeleteLine(ref _hostsFile, selectedIndices);

        lstHosts.BeginUpdate();
        lstHosts.Items.Clear();
        lstHosts.Items.AddRange(_hostsFile.Lines);
      }
      catch (UnauthorizedAccessException ex)
      {
        throw;
      }
      catch (Exception)
      {

        throw;
      }
      finally
      {
        lstHosts.EndUpdate();
      }
    }



    private void OpenNotepad(object sender, EventArgs eventArgs)
    {
      Hosts.OpenInNotepad();
    }

  }
}
