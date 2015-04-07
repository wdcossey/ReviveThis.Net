using System;
using System.ComponentModel.Composition;
using FirstFloor.ModernUI.Windows;
using ReviveThis.Interfaces;

namespace ReviveThis.Net.Tool.UninstallManager
{
  //[ToolContent("/Tools/UninstallManager")]
  [Export(typeof(IToolAddIn))]
  public class HostManagerTool: IToolAddIn
  {

    private static volatile ToolContent _content;
    private static readonly object SyncRoot = new Object();

    #region Author
    public string Author
    {
      get { return "William David Cossey"; }
    }
    #endregion

    #region Version
    private Version _version;
    public Version Version
    {
      get
      {
        if (_version == null)
        {
          lock (SyncRoot)
          {
            if (_version == null)
              _version = new Version(1, 0, 0, 0);
          }
        }

        return _version;
      }
    }
    #endregion

    #region Name
    public string Name {
      get { return "Uninstall Manager"; }
    }
    #endregion

    #region Description

    public string[] Description
    {
      get
      {
        return new[]
        {
          string.Empty
        };
      }
    }
    #endregion

    public void Dispose()
    {
      if (_content != null)
      {
        _content.Dispose();
        _content = null;
      }
    }

    #region Content 
    public IContent Content
    {
      get
      {
        if (_content == null)
        {
          lock (SyncRoot)
          {
            if (_content == null)
              _content = new ToolContent();
          }
        }

        return _content;
      }

    }

    #region Guid

    public Guid Guid
    {
      get { return new Guid("{CD370745-8EC7-472F-960F-EBD779F20DE8}"); }
    }
    #endregion

    #endregion

  }
}