using System;
using System.Globalization;
using System.Reflection;

namespace ReviveThis.Models
{
  public class AboutModel
  {
    #region Members
    Version _version;
    private string _copyrightOwner;
    private string _copyrightYear;
    private string _name;

    #endregion

    #region Properties

    public string Name
    {
      get
      {
        if (_name != null)
          return _name;

        return _name = "ReviveThis.Net";
      }
    }

    public string CopyrightOwner
    {
      get
      {
        if (_copyrightOwner != null)
          return _copyrightOwner;

        return _copyrightOwner = "William David Cossey";
      }
    }

    public string CopyrightYear
    {
      get
      {
        if (_copyrightYear != null)
          return _copyrightYear;

        return _copyrightYear = string.Format("2014-{0}", (DateTime.Now.Year < 2014) ? string.Empty : DateTime.Now.Year.ToString(CultureInfo.InvariantCulture));
      }
    }

    public Version Version
    {
      get
      {
        if (_version != null)
          return _version;

        return _version = Assembly.GetEntryAssembly().GetName().Version;
      }
    }
    #endregion
  }
}