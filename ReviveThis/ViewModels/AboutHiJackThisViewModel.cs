using System;
using System.Reflection;
using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using ReviveThis.Models;

namespace ReviveThis.ViewModels
{
  public class AboutHiJackThisViewModel
  {
    #region Members
    string _author;
    Version _version;
    private string _copyrightOwner;
    private string _copyrightYear;
    #endregion

    #region Properties

    public string Name
    {
      get { return "Trend Micro HijackThis"; }
    }
    
    public string CopyrightOwner
    {
      get
      {
        if (_copyrightOwner != null)
          return _copyrightOwner;

        return _copyrightOwner = "Trend Micro";
      }
    }
    
    public string CopyrightYear
    {
      get
      {
        if (_copyrightYear != null)
          return _copyrightYear;

        return _copyrightYear = string.Empty;
      }
    }
    #endregion
  }
}