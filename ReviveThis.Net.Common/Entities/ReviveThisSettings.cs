using System;
using System.Collections.Generic;
using System.Linq;

namespace ReviveThis.Entities
{
  public class ReviveThisSettings
  {
    private static volatile ReviveThisSettings _instance;
    private static readonly object LockObj = new Object();

    public static ReviveThisSettings Default
    {
      get
      {
        if (_instance == null)
        {
          lock (LockObj)
          {
            if (_instance == null)
              _instance = new ReviveThisSettings();
          }
        }

        return _instance;
      }
    }

    private static IEnumerable<string> Arguments
    {
      get
      {
        return Environment.GetCommandLineArgs().Select(s => s.ToLower()).ToArray();
      }
    }

    public bool VerboseMode
    {
      get
      {
        return Arguments.FirstOrDefault(f => f.Equals("/verbose", StringComparison.InvariantCultureIgnoreCase)) != null;
      }
    }

    public bool Complete
    {
      get
      {
        return Arguments.FirstOrDefault(f => f.Equals("/complete", StringComparison.InvariantCultureIgnoreCase)) != null;
      }
    }

    public bool ForceAll
    {
      get
      {
        return Arguments.FirstOrDefault(f => f.Equals("/forceall", StringComparison.InvariantCultureIgnoreCase)) != null;
      }
    }

    //// ReSharper disable once InconsistentNaming
    //public bool ForceWin9x
    //{
    //  get
    //  {
    //    return !ForceAll && Arguments.FirstOrDefault(f => f.Equals("/force9x", StringComparison.InvariantCultureIgnoreCase)) != null;
    //  }
    //}

    //public bool ForceWinNt
    //{
    //  get
    //  {
    //    return !ForceAll && Arguments.FirstOrDefault(f => f.Equals("/forcent", StringComparison.InvariantCultureIgnoreCase)) != null;
    //  }
    //}

    public bool HtmlOutput
    {
      get
      {
        return Arguments.FirstOrDefault(f => f.Equals("/html", StringComparison.InvariantCultureIgnoreCase)) != null;
      }
    }

    public bool Full
    {
      get
      {
        return Arguments.FirstOrDefault(f => f.Equals("/full", StringComparison.InvariantCultureIgnoreCase)) != null;
      }
    }

    public bool IgnoreSafe
    {
      get
      {
        return true;//Arguments.FirstOrDefault(f => f.Equals("/ignoreSafe", StringComparison.InvariantCultureIgnoreCase)) != null;
      }
    }

  }
}