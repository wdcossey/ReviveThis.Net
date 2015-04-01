using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReviveThis.Forms
{
  public partial class FrmProgress : Form
  {
    private bool _canClose = false;

    public FrmProgress()
    {
      InitializeComponent();
    }

    protected override void OnShown(EventArgs e)
    {
      base.OnShown(e);
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      e.Cancel = !_canClose;
      base.OnClosing(e);
    }
    
    public void CloseMe()
    {
      _canClose = true;
      Close();
    }
  }
}
