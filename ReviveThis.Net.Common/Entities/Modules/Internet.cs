using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace ReviveThis.Entities.Modules
{
  public static class Internet
  {
    //public const string UPDATE_URL = @"http://sourceforge.net/projects/hjt/";
    public const string UPDATE_URL = @"https://bitbucket.org/wdcossey/revivethis.net/";

    #region CheckForUpdate

    public static void CheckForUpdate()
    {
      ////var version = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyVersionAttribute), true).Single() as AssemblyVersionAttribute;
      ////if (version == null)
      ////  return;

      //var fileVersionInfo = Process.GetCurrentProcess().MainModule.FileVersionInfo;
      //var sThisVersion = string.Format("{0}.{1}.{2}", fileVersionInfo.FileMajorPart, fileVersionInfo.FileMinorPart,
      //  fileVersionInfo.FileBuildPart);

      if (IsOnline)
      {
        Process.Start(new ProcessStartInfo(UPDATE_URL));
      }
      else
      {
        MessageBox.Show(@"No Internet Connection Available");
      }
    }

    /*
    Public Sub CheckForUpdate()
        Dim hInternet&, hFile&, sBuffer$, lBufferLen&
        Dim sVer$, sUpdate$, sZipFile$, sThisVersion$
        Dim sProxy$, sFilename$
        'On Error GoTo Error:
    
        sThisVersion = CStr(App.Major) & "." & CStr(App.Minor) & "." & CStr(App.Revision)
    
    
        Dim szUpdateUrl As String
        szUpdateUrl = "http://sourceforge.net/projects/hjt/"
        
        If True = IsOnline Then
            ShellExecute 0&, "open", szUpdateUrl, vbNullString, vbNullString, vbNormalFocus
        Else
            MsgBox "No Internet Connection Available"
        End If
    End Sub
    */
    #endregion

    #region SendData

    public static string SendData(string szUrl, string szData)
    {
      return null;
    }

    /*
    Public Sub SendData(szUrl As String, szData As String)
    On Error GoTo Error
    Dim szRequest As String
    Dim xmlhttp As Object
    Dim dataLen As Integer
    Set xmlhttp = CreateObject("MSXML2.ServerXMLHTTP")

    szRequest = "data=" & URLEncode(szData)

    dataLen = Len(szRequest)
    xmlhttp.Open "POST", szUrl, False
    xmlhttp.setRequestHeader "Content-Type", "application/x-www-form-urlencoded"
    'xmlhttp.setRequestHeader "User-Agent", "HJT.1.99.2" & "|" & sWinVersion & "|" & sMSIEVersion

    xmlhttp.send "" & szRequest
    'MsgBox szData

    szResponse = xmlhttp.responseText
    'MsgBox szResponse

    Set xmlhttp = Nothing

    Error:

    End Sub
    */
    #endregion

    #region GetUrl

    public static string GetUrl(string szUrl)
    {
      return null;
    }

    /*
    Function GetUrl(szUrl As String) As String
    On Error GoTo Error:
    Dim szRequest As String
    Dim xmlhttp As Object
    Dim dataLen As Integer
    Set xmlhttp = CreateObject("MSXML2.ServerXMLHTTP")

    dataLen = Len(szRequest)
    xmlhttp.Open "GET", szUrl, False
    xmlhttp.setRequestHeader "Content-Type", "application/x-www-form-urlencoded"
    'xmlhttp.setRequestHeader "User-Agent", "HJT.1.99.2" & "|" & sWinVersion & "|" & sMSIEVersion

    xmlhttp.send "" & szRequest
    'MsgBox szData

    GetUrl = xmlhttp.responseText
    'MsgBox szResponse

    Set xmlhttp = Nothing
    Exit Function

    Error:
    GetUrl = "HJT_NOT_SUPPORTED"
    End Function
    */
    #endregion

    #region ParseHTTPResponse
    //Public Sub ParseHTTPResponse(szResponse As String)

    //Dim curPos As Integer
    //Dim startIDPos, endIDPos, startDataPos, endDataPos As Integer
    //Dim szDataId, szData As String

    //curPos = 1
    //Do While curPos < Len(szResponse)
    //    startIDPos = InStr(curPos, szResponse, "#HJT_DATA:", vbTextCompare)
    
    //    If 1 > startIDPos Then Exit Sub
    
    //    startIDPos = startIDPos + 10
    
    //    endIDPos = InStr(curPos, szResponse, "=", vbTextCompare)
    
    //    If 1 > endIDPos Then Exit Sub
    
    //    endIDPos = endIDPos
    
    //    startDataPos = endIDPos + 1
    
    //    endDataPos = InStr(curPos, szResponse, "#END_HJT_DATA", vbTextCompare)
    
    //    If 1 > endIDPos Then Exit Sub
    
    //    endDataPos = endDataPos
    
    //    curPos = curPos + endDataPos + 14
    
    //    szDataId = Mid(szResponse, startIDPos, endIDPos - startIDPos)
    //    szData = Mid(szResponse, startDataPos, endDataPos - startDataPos)
    
    //    Select Case szDataId
    //    Case "REPORT_URL"
    //    ShellExecute 0&, "open", szData, vbNullString, vbNullString, vbNormalFocus
    //    Case "SUBMIT_URL"
    //    szSubmitUrl = szData
    //    End Select
    
    //Loop


    //End Sub
    #endregion

    #region URLEncode
    //Function URLEncode(ByVal Text As String) As String
    //    Dim i As Integer
    //    Dim acode As Integer
    //    Dim char As String
    
    //    URLEncode = Text
    
    //    For i = Len(URLEncode) To 1 Step -1
    //        acode = Asc(Mid$(URLEncode, i, 1))
    //        Select Case acode
    //            Case 48 To 57, 65 To 90, 97 To 122
    //                ' don't touch alphanumeric chars
    //            Case 32
    //                ' replace space with "+"
    //                Mid$(URLEncode, i, 1) = "+"
    //            Case Else
    //                ' replace punctuation chars with "%hex"
    //                URLEncode = Left$(URLEncode, i - 1) & "%" & Hex$(acode) & Mid$ _
    //                    (URLEncode, i + 1)
    //        End Select
    //    Next
    
    //End Function
    #endregion

    #region CheckForInternetConnection

    public static bool CheckForInternetConnection
    {
      get
      {
        try
        {
          using (var client = new WebClient())
          using (var stream = client.OpenRead("http://www.google.com"))
          {
            return true;
          }
        }
        catch
        {
          return false;
        }
      }
    }

    #endregion

    #region IsOnline

    public static bool IsOnline
    {
      get { return CheckForInternetConnection; }
    }

    /*
    //Public Function IsOnline() As Boolean

    //   IsOnline = InternetGetConnectedState(0&, 0&)

    //End Function
    */
    #endregion
  }
}