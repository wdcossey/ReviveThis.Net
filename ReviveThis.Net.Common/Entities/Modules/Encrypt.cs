namespace ReviveThis.Entities.Modules
{
  public static class Encrypt
  {

    public static string Decrypt(string sMsg, string sPhrase)
    {
      return Process(sMsg, sPhrase);
    }

    public static string Crypt(string sMsg, string sPhrase)
    {
      return Process(sMsg, sPhrase, true);
    }

    private static string Process(string sMsg, string sPhrase, bool encrypt = false)
    {
      //var sEncryptionPhrase = "FUCK YOU SPYWARENUKER AND BPS SPYWARE REMOVER!";

      var j = 0;
      var result = string.Empty;

      foreach (var @char in sMsg)
      {
        var sChar = @char;
        if (encrypt)
        {
          //encrypt
          sChar = (char) (sChar + sPhrase[j]);
          if (sChar > 126)
          {
            sChar = (char) (sChar - 94);
          }
        }
        else
        {
          //decrypt
          var iChar = (sChar - sPhrase[j]);
          if (iChar < 32)
          {
            //make sure decrypted char is within
            //normal range (space to ~)
            sChar = (char) (iChar + 94);
          }
          else
          {
            //old encrypter doesn't encrypt chars above 126 :(
            if (sChar < 192)
            {
              sChar = (char) (iChar);
            }
          }
        }

        result += sChar;
        j++;
        if (j > sPhrase.Length - 1)
          j = 0;
      }
      return result;
    }

    //Public Function Crypt$(sMsg$, sPhrase$, Optional bEnOrDec As Boolean = False)
    //    Dim sEncryptionPhrase$
    //    On Error Resume Next
    //    'if one error happens, don't screw up everything following

    //    'like, NOT!
    //    sEncryptionPhrase = "FUCK YOU SPYWARENUKER AND BPS SPYWARE REMOVER!"

    //    'bEnOrDec = false -> decrypt
    //    'bEnOrDec = true -> encrypt

    //    ''some checks to prevent double encrypting
    //    ''or double decrypting
    //    'If Left(sMsg, 1) = ">" Then
    //    '    'the stuff is encrypted
    //    '    If bEnOrDec = True Then
    //    '        Crypt = sMsg
    //    '        Exit Function
    //    '    End If
    //    'ElseIf Left(sMsg, 1) = "H" Then
    //    '    'the stuff is not encrypted
    //    '    If bEnOrDec = False Then
    //    '        Crypt = sMsg
    //    '        Exit Function
    //    '    End If
    //    'End If

    //    Dim i%, j%, sChar$, iChar%, sOut$
    //    j = 1
    //    For i = 1 To Len(sMsg)
    //        sChar = Mid(sMsg, i, 1)
    //        If bEnOrDec Then
    //            'encrypt
    //            sChar = Chr(Asc(sChar) + Asc(Mid(sPhrase, j, 1)))
    //            If Asc(sChar) > 126 Then
    //                'make sure encrypted char is within
    //                'normal range (space to ~)
    //                sChar = Chr(Asc(sChar) - 94)
    //            End If
    //        Else
    //            'decrypt
    //            iChar = Asc(sChar) - Asc(Mid(sPhrase, j, 1))
    //            If iChar < 32 Then
    //                'make sure decrypted char is within
    //                'normal range (space to ~)
    //                sChar = Chr(iChar + 94)
    //            Else
    //                'old encrypter doesn't encrypt chars above 126 :(
    //                If Asc(sChar) < 192 Then
    //                    sChar = Chr(iChar)
    //                End If
    //            End If
    //        End If
    //        sOut = sOut & sChar
    //        j = j + 1
    //        If j > Len(sPhrase) Then j = 1
    //    Next i
    //    Crypt = sOut
    //End Function

  }
}