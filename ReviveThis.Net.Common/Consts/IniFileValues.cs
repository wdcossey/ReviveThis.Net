using System;
using System.IO;

namespace ReviveThis.Consts
{

  /// <summary>
  /// LOAD FILEVALS
  /// </summary>
  public static class IniFileValues
  {
    public static string[] Array =
    {
      //=== LOAD FILEVALS ===
      //syntax:
      // inifile,section,value,resetdata,baddata
      // |       |       |     |         |
      // |       |       |     |         1) data that shouldn't be (never used)
      // |       |       |     2) data to reset to
      // |       |       |        (delete all if empty)
      // |       |       3) value to check
      // |       4) section to check
      // 5) file to check
    
      //string.Format(@"{0},boot,Shell,explorer.exe,",
      //  Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "system.ini")),
      string.Format(@"{0},boot,Shell,explorer.exe,",
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "system.ini")),
      //@"system.ini,boot,Shell,explorer.exe,",
      //@"icdk'bvL\_LR`e6!IOHZbtVo2aYUShNUi[L",

      //string.Format(@"{0},boot,Shell,explorer.exe,",
      //  Path.Combine(Environment.GetEnvironmentVariable("SysWOW64"), "system.ini")),
      //string.Format(@"{0},boot,Shell,explorer.exe,",
      //  Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "system.ini")),
      
    
      string.Format(@"{0},windows,load,,", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "win.ini")),
      //@"win.ini,windows,load,,",
      //@"mS_%+cSme_0T`m5!bVDR""t",
    
      string.Format(@"{0},windows,run,,", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "win.ini")),
      //@"win.ini,windows,run,,",
      //@"mS_%+cSme_0T`m5!h\Qx""",

      string.Format(@"REG:{0},boot,Shell,explorer.exe,", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "system.ini")),
      //@"REG:system.ini,boot,Shell,explorer.exe,",
      //@"H/815n]WScNY__LWeVWxIRVc.!O[^b1bVhNZnLm",
    
      string.Format(@"REG:{0},windows,load,,", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "win.ini")),
      //@"REG:win.ini,windows,load,,",
      //@"H/819^XoWd+zh_0Ye^VxbYR[L!",
    
      string.Format(@"REG:{0},windows,run,,", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "win.ini")),
      //@"REG:win.ini,windows,run,,",
      //@"H/819^XoWd+zh_0Ye^Vxh__#L",

      string.Format(@"REG:{0},boot,UserInit,{1},", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "system.ini"), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "UserInit.exe")),
      //@"REG:system.ini,boot,UserInit,$WINDIR\System32\UserInit.exe,",
      //@"H/815n]WScNY__LWeVWxK]ViicSWxxw9?:iGR:\ajO^*RQ?VShi^ZjNZnLm",

    };
  }
}