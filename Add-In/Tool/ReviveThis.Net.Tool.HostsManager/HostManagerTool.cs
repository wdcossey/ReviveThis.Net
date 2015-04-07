using System;
using System.ComponentModel.Composition;
using FirstFloor.ModernUI.Windows;
using ReviveThis.Entities;
using ReviveThis.Interfaces;

namespace ReviveThis.Net.Tool.HostsManager
{
  //[ToolContent("/Tools/HostsManager")]
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
      get { return "Hosts File Manager"; }
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

    #region

    public Guid Guid
    {
      get { return new Guid("{A9D23C9A-AAD2-4B3C-8DBA-96CA885ED129}"); }
    }
    #endregion

    #endregion

  }
}