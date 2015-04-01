using System;
using System.Windows.Forms;
using ReviveThis.Forms;

namespace ReviveThis
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {


      //HiJackThis.Module.StartupList.Default.Execute();


      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new FrmMain());
    }
  }
}
