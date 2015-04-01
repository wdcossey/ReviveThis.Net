
namespace ReviveThis.Consts
{
  /// <summary>
  /// LOAD REGVALS
  /// 
  /// syntax:
  ///   regkey,regvalue,resetdata,baddata
  ///   |      |        |          |
  ///   |      |        |          1) data that shouldn't be (never used)
  ///   |      |        2) data to reset to
  ///   |      3) value to check
  ///   4) regkey to check
  /// 
  /// when empty:
  /// 1) everything is considered bad (always used), change to resetdata
  /// 2) value being present is considered bad, delete value
  /// 3) key being present is considered bad, delete key (not used)
  /// 4) [invalid]
  /// 
  /// </summary>
  public static class RegistryValues
  {
    public static string[] Array =
    {
      //@"HKCU\Software\Microsoft\Internet Explorer,Default_Page_URL,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUSht5\(V_ObUpQX[!JH3mx",

      //@"HKCU\Software\Microsoft\Internet Explorer,Default_Search_URL,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUSht5\(V_ObUsURh%]U<5:""t",

      //@"HKCU\Software\Microsoft\Internet Explorer,SearchAssistant,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShtD\#gMK/i5Ydj#cjqm",
    
      //@"HKCU\Software\Microsoft\Internet Explorer,CustomizeSearch,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUSht4l5iYPWp'CVW4X^qm",

      //@"HKCU\Software\Microsoft\Internet Explorer,Search,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShtD\#gMKx""",
    
      //@"HKCU\Software\Microsoft\Internet Explorer,Search Bar,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShtD\#gMKl8#b{""",

      //@"HKCU\Software\Microsoft\Internet Explorer,Search Page,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShtD\#gMKlF#WV""L",
    
      //@"HKCU\Software\Microsoft\Internet Explorer,Start Page,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShtDk#g^a>W)U{""",
    
      //@"HKCU\Software\Microsoft\Internet Explorer,SearchURL,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShtD\#gMKCHlz{",
    
      //@"HKCU\Software\Microsoft\Internet Explorer,(Default),,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShtw;'[KXZjIz{",
    
      //@"HKCU\Software\Microsoft\Internet Explorer,www,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShthn9!t",
   
      //@"HKLM\Software\Microsoft\Internet Explorer,Default_Page_URL,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUSht5\(V_ObUpQX[!JH3mx",
    
      //@"HKLM\Software\Microsoft\Internet Explorer,Default_Search_URL,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUSht5\(V_ObUsURh%]U<5:""t",
    
      //@"HKLM\Software\Microsoft\Internet Explorer,SearchAssistant,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShtD\#gMK/i5Ydj#cjqm",
    
      //@"HKLM\Software\Microsoft\Internet Explorer,CustomizeSearch,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUSht4l5iYPWp'CVW4X^qm",
    
      //@"HKLM\Software\Microsoft\Internet Explorer,Search,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShtD\#gMKx""",
    
      //@"HKLM\Software\Microsoft\Internet Explorer,Search Bar,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShtD\#gMKl8#b{""",
    
      //@"HKLM\Software\Microsoft\Internet Explorer,Search Page,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShtD\#gMKlF#WV""L",
    
      //@"HKLM\Software\Microsoft\Internet Explorer,Start Page,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShtDk#g^a>W)U{""",
    
      //@"HKLM\Software\Microsoft\Internet Explorer,SearchURL,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShtD\#gMKCHlz{",
    
      //@"HKLM\Software\Microsoft\Internet Explorer,(Default),,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShtw;'[KXZjIz{",
    
      //@"HKLM\Software\Microsoft\Internet Explorer,www,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShthn9!t",

      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer,Default_Page_URL,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUShL4V\#jb[B>WQVVuG6mx",
    
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer,Default_Search_URL,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUShL4V\#jb[BA[KcZ*T?5:""L",

      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer,SearchAssistant,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUShLCVW4X^(Va_]eX0itm",
      
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer,CustomizeSearch,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUShL3fi6dcP]SIORi%]tm",
  
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer,Search,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUShLCVW4X^qm",
  
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer,Search Bar,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUShLCVW4X^e%Oht{",
  
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer,Search Page,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUShLCVW4X^e3O]O{#",
  
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer,Start Page,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUShLCeW4it7DU[t{",
  
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer,SearchURL,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUShLCVW4X^<5:""t",
  
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer,(Default),,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUShLv5[(VkSWu""t",
  
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer,www,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUShLghmL!",
    
      //@"HKCU\Software\Microsoft\Internet Explorer\Main,Default_Page_URL,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct'S\#e]j!EWNHMK<=#L",
  
      //@"HKCU\Software\Microsoft\Internet Explorer\Main,Default_Search_URL,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct'S\#e]j!H[HUQ^IFIl!t",
  
      //@"HKCU\Software\Microsoft\Internet Explorer\Main,SearchAssistant,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct6SW4SY75h_ZWOd^{#",
  
      //@"HKCU\Software\Microsoft\Internet Explorer\Main,CustomizeSearch,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct&ci6_^_<ZILD`YR{#",
  
      //@"HKCU\Software\Microsoft\Internet Explorer\Main,Search Bar,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct6SW4SYtbVhqm",
  
      //@"HKCU\Software\Microsoft\Internet Explorer\Main,Search Page,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct6SW4SYtpV]Lmx",
  
      //@"HKCU\Software\Microsoft\Internet Explorer\Main,Start Page,$DEFSTARTPAGE,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct6bW4doF#\[qe2;0DKaG>3/=ez",
  
      //@"HKCU\Software\Microsoft\Internet Explorer\Main,SearchURL,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct6SW4SYKrA""q",

      //@"HKLM\Software\Microsoft\Internet Explorer\Main,Default_Page_URL,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct'S\#e]j!EWNHMK<=#L",
  
      //@"HKLM\Software\Microsoft\Internet Explorer\Main,Default_Search_URL,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct'S\#e]j!H[HUQ^IFIl!t",
  
      //@"HKLM\Software\Microsoft\Internet Explorer\Main,SearchAssistant,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct6SW4SY75h_ZWOd^{#",
  
      //@"HKLM\Software\Microsoft\Internet Explorer\Main,CustomizeSearch,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct&ci6_^_<ZILD`YR{#",
  
      //@"HKLM\Software\Microsoft\Internet Explorer\Main,Search Bar,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct6SW4SYtbVhqm",
  
      //@"HKLM\Software\Microsoft\Internet Explorer\Main,Search Page,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct6SW4SYtpV]Lmx",
  
      //@"HKLM\Software\Microsoft\Internet Explorer\Main,Start Page,$DEFSTARTPAGE,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct6bW4doF#\[qe2;0DKaG>3/=ez",
  
      //@"HKLM\Software\Microsoft\Internet Explorer\Main,SearchURL,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct6SW4SYKrA""q",
    
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer\Main,Default_Page_URL,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUSh|=R_0!:LIOkVeVpVQHMKr<{""",
  
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer\Main,Default_Search_URL,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUSh|=R_0!:LIOkVeVsZKUQ^!ECBL!",
  
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer\Main,SearchAssistant,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUSh|=R_0!ILD`YR2j5^]WOd6z{",
  
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer\Main,CustomizeSearch,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUSh|=R_0!9\VbeWZq'HOD`Y*z{",
  
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer\Main,Search Bar,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUSh|=R_0!ILD`YRo9#gtm",
  
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer\Main,Search Page,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUSh|=R_0!ILD`YRoG#\Omx",
  
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer\Main,Start Page,$DEFSTARTPAGE,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUSh|=R_0!I[D`jhAX)Zte2;fCE7rIF(*3""",
  
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer\Main,SearchURL,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUSh|=R_0!ILD`YRFIl!t",

      //@"HKCU\Software\Microsoft\Internet Explorer\Search,Default_Search_URL,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShFD\#gMKx:'VRk.iU:HOhMYVuG6mx",
  
      //@"HKCU\Software\Microsoft\Internet Explorer\Search,SearchAssistant,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShFD\#gMKxI'QcY*6iZLajK_kL!",
  
      //@"HKCU\Software\Microsoft\Internet Explorer\Search,CustomizeSearch,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShFD\#gMKx97cee/^pL6SW\T_L!",
  
      //@"HKCU\Software\Microsoft\Internet Explorer\Search,(Default),,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShFD\#gMKx|dUWW7ajnmx",
  
      //@"HKCU\Software\Microsoft\Internet Explorer\Search,Default_Page_URL,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShFD\#gMKx:'VRk.iU7DU[IFIl!t",

      //@"HKLM\Software\Microsoft\Internet Explorer\Search,Default_Search_URL,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShFD\#gMKx:'VRk.iU:HOhMYVuG6mx",
  
      //@"HKLM\Software\Microsoft\Internet Explorer\Search,SearchAssistant,$DEFSEARCHASS,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShFD\#gMKxI'QcY*6iZLajK_kLw.(4Ie1C9h6I:m",
  
      //@"HKLM\Software\Microsoft\Internet Explorer\Search,CustomizeSearch,$DEFSEARCHCUST,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShFD\#gMKx97cee/^pL6SW\T_Lw.(4Ie1C9h8K:7x",
  
      //@"HKLM\Software\Microsoft\Internet Explorer\Search,(Default),,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShFD\#gMKx|dUWW7ajnmx",
  
      //@"HKLM\Software\Microsoft\Internet Explorer\Search,Default_Page_URL,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShFD\#gMKx:'VRk.iU7DU[IFIl!t",
    
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer\Search,Default_Search_URL,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUSh|CVW4X^q'S\Kfc6T=HOh%XPKrA""q",
  
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer\Search,SearchAssistant,$DEFSEARCHASS,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUSh|CVW4X^q6SW\T_ah]Laj#^e""D9;-637<4?aH=m",
  
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer\Search,CustomizeSearch,$DEFSEARCHCUST,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUSh|CVW4X^q&ci^`d+oO6SW4SY""D9;-637<4?cJ=7x",
  
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer\Search,(Default),,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUSh|CVW4X^qi2[PRl.iqmx",
  
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer\Search,Default_Page_URL,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUSh|CVW4X^q'S\Kfc6T:DU[!ECBL!",

      //@"HKCU\Software\Microsoft\Internet Explorer\SearchURL,(Default),,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShFD\#gMKCHlzw:'[W\Ob}t{",
  
      //@"HKCU\Software\Microsoft\Internet Explorer\SearchURL,SearchURL,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShFD\#gMKCHlzD[#gYO8@Bt{",

      //@"HKLM\Software\Microsoft\Internet Explorer\SearchURL,(Default),,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShFD\#gMKCHlzw:'[W\Ob}t{",
  
      //@"HKLM\Software\Microsoft\Internet Explorer\SearchURL,SearchURL,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShFD\#gMKCHlzD[#gYO8@Bt{",

      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer\SearchURL,(Default),,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUSh|CVW4X^<5:""p5\(V_Ob}Lz",
  
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer\SearchURL,SearchURL,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUSh|CVW4X^<5:""=VX4XR8@BLz",
    
      //@"HKCU\Software\Microsoft\Internet Explorer\Main,Startpagina,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct6bW4daW)^dHmx",

      //@"HKCU\Software\Microsoft\Internet Explorer\Main,First Home Page,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct)Wh5do>1b[e3O]O{#",

      //@"HKCU\Software\Microsoft\Internet Explorer\Main,Local Page,$WINSYSDIR\blank.htm,
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct/]Y#\oF#\[qeE?8DPs935JX.Q_aN]jTm",

      //@"HKCU\Software\Microsoft\Internet Explorer\Main,Start Page_bak,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct6bW4doF#\[FEOat{",

      //@"HKCU\Software\Microsoft\Internet Explorer\Main,HomeOldSP,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct+]c'?]ZsE""q",

      //@"HKCU\Software\Microsoft\Internet Explorer\Main,Use Custom Search URL,,",
      //@">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct8a[@3fi6dce6SW\T_ @J</x""",

      //@"HKCU\Software\Microsoft\Internet Explorer\Main,Úvodní stránka,,",
      //@">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ctÚde&^ít5iháQYWt{",

      //@"HKLM\Software\Microsoft\Internet Explorer\Main,Startpagina,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct6bW4daW)^dHmx",

      //@"HKLM\Software\Microsoft\Internet Explorer\Main,First Home Page,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct)Wh5do>1b[e3O]O{#",

      //@"HKLM\Software\Microsoft\Internet Explorer\Main,Local Page,$WINSYSDIR\blank.htm,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct/]Y#\oF#\[qeE?8DPs935JX.Q_aN]jTm",

      //@"HKLM\Software\Microsoft\Internet Explorer\Main,Start Page_bak,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct6bW4doF#\[FEOat{",

      //@"HKLM\Software\Microsoft\Internet Explorer\Main,HomeOldSP,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct+]c'?]ZsE""q",

      //@"HKLM\Software\Microsoft\Internet Explorer\Main,Úvodní stránka,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ctÚde&^ít5iháQYWt{",
    
      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer\Main,Startpagina,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUSh|=R_0!I[D`jZR^+cKmx",

      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer\Main,First Home Page,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUSh|=R_0!<PUajh9f/Zh3O]'z{",

      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer\Main,Local Page,$WINSYSDIR\blank.htm,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUSh|=R_0!BVFObhAX)ZteE?nCJId>HCEZWX\%*iWm",

      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer\Main,Start Page_bak,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUSh|=R_0!I[D`jhAX)ZIEOaLz",

      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer\Main,HomeOldSP,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUSh|=R_0!>VPSEVUJp!t",

      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer\Main,Úvodní stránka,,",
      //@">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUSh|=R_0!Ú]RRdíoj6gáQYWLz",
    
      //@"HKLM\Software\Microsoft\Internet Explorer\Main,YAHOOSubst,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct</>o?Dk$hjqm",

      //@"HKCU\Software\Microsoft\Internet Explorer\Main,YAHOOSubst,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct</>o?Dk$hjqm",

      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Explorer\Main,YAHOOSubst,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh6o2aYUSh|=R_0!O(+=E=fY5itm",
    
      //@"HKCU\Software\Microsoft\Internet Connection Wizard,ShellNext,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do91cdLFb_Y_uw^dD`ZLCY[.aDL[b""t",

      //@"HKLM\Software\Microsoft\Internet Connection Wizard,ShellNext,,",
      @">5=D|HYIbm#bVRm^YYRaePeSic^H`d'do91cdLFb_Y_uw^dD`ZLCY[.aDL[b""t",

      //@"HKUS\.DEFAULT\Software\Microsoft\Internet Connection Wizard,ShellNext,,",
      @">5FJ|#.(47u<ERsd\[ZOhOMD+X\Rae(dM?0i[YQSjh4f0cOFb_1^oM+oWYGxIRVc.CO[b""L",

      //@"HKCU\Software\Microsoft\Internet Explorer\Main,Window Title,,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShF>X+ct:Wd&_htt^jSHx""",

      //@"HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings,AutoConfigURL,,",
      @">54L|HYIbm#bVRm^YYRaePeSw^XG]m5L4k4g[UWD[\d`1cF,\j'b_[6sILWb_XXjL6_W]91^W_)JH3mx",

      //@"HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings,ProxyServer,,",
      @">54L|HYIbm#bVRm^YYRaePeSw^XG]m5L4k4g[UWD[\d`1cF,\j'b_[6sILWb_XXjLE\RfosUcl'g""q",

      //@"HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings,ProxyOverride,,",
      @">54L|HYIbm#bVRm^YYRaePeSw^XG]m5L4k4g[UWD[\d`1cF,\j'b_[6sILWb_XXjLE\RfoofVh4^ZLmx",

      //@"HKCU\Software\Microsoft\Internet Explorer\Toolbar,LinksFolderName,Links,",
      @">54L|HYIbm#bVRm^YYRaePeSic^H`d'do;:ebVUShFEf1aLD`""lY_a5;eSGSh8Rd'!6L\a5z"

    };
  }
}