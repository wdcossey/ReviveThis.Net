using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ReviveThis.Entities;
using ReviveThis.Entities.Modules;

namespace ReviveThis.Controls
{
  public partial class ServicesManager : UserControl
  {
    private List<ServiceInformation> _servicesList = null;

    public ServicesManager()
    {
      InitializeComponent();

      this.Load += OnLoad;

      tsbRefresh.Click += (sender, args) => RefreshServices();

      muiElevated.Click += ElevatedOnClick;

      //lstHosts.SelectedIndexChanged += HostsSelectedIndexChanged;

      //Load the installed services.
      RefreshServices();

      //HostsSelectedIndexChanged(lstHosts, new EventArgs());
    }

    private void RefreshServices()
    {
      _servicesList = Services.ParseServiceList();

      if (_servicesList != null && _servicesList.Count > 0)
      {
        try
        {
          ltvServices.BeginUpdate();
          ltvServices.Items.Clear();
          ltvServices.Items.AddRange(_servicesList.Select(item => new ListViewItem(new[] { string.Empty, item.DisplayName, item.ImagePath, item.ServiceType.ToString() }) { Tag = item }).ToArray());
        }
        finally
        {
          ltvServices.EndUpdate();
        }

        //lblCaption.Text = string.Format("Hosts file is located at: {0} ({1} lines, {2})", _servicesList.Location,
        //  _servicesList.LineCount, _servicesList.FormattedAttributes);
      }
    }

    private void ElevatedOnClick(object sender, EventArgs eventArgs)
    {
      Hosts.OpenInNotepad(true);
    }

    //private void HostsSelectedIndexChanged(object sender, EventArgs eventArgs)
    //{
    //  var listBox = sender as ListBox;
    //  if (listBox == null)
    //  {
    //    return;
    //  }

    //  var count = listBox.SelectedItems.Count;

    //  btnDelete.Enabled = count > 0;
    //  btnDelete.Text = string.Format("Delete ({0}) line{1}", count, count != 1 ? "s" : string.Empty);

    //  btnToggle.Enabled = count > 0;
    //  btnToggle.Text = string.Format("Toggle ({0}) line{1}", count, count != 1 ? "s" : string.Empty);

    //}

    private void ToggleLines(object sender, EventArgs eventArgs)
    {
      //try
      //{
      //  var selectedIndices = lstHosts.SelectedIndices.Cast<int>().OrderBy(o => o).ToArray();

      //  Hosts.HostsToggleLine(ref _servicesList, selectedIndices);
      //  lstHosts.BeginUpdate();
      //  lstHosts.Items.Clear();
      //  lstHosts.Items.AddRange(_servicesList.Lines);

      //  foreach (var index in selectedIndices)
      //  {
      //    lstHosts.SetSelected(index, true);
      //  }
      //}
      //catch (UnauthorizedAccessException ex)
      //{
      //  throw;
      //}
      //catch (Exception)
      //{

      //  throw;
      //}
      //finally
      //{
      //  lstHosts.EndUpdate();
      //}
    }

    private void OnLoad(object sender, EventArgs eventArgs)
    {

    }

    private void DeleteLines(object sender, EventArgs eventArgs)
    {
      //try
      //{
      //  var selectedIndices = lstHosts.SelectedIndices.Cast<int>().OrderBy(o => o).ToArray();

      //  Hosts.HostsDeleteLine(ref _servicesList, selectedIndices);

      //  lstHosts.BeginUpdate();
      //  lstHosts.Items.Clear();
      //  lstHosts.Items.AddRange(_servicesList.Lines);
      //}
      //catch (UnauthorizedAccessException ex)
      //{
      //  throw;
      //}
      //catch (Exception)
      //{

      //  throw;
      //}
      //finally
      //{
      //  lstHosts.EndUpdate();
      //}
    }



    private void OpenNotepad(object sender, EventArgs eventArgs)
    {
      Hosts.OpenInNotepad();
    }

  }
}
