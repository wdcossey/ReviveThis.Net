using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ReviveThis.AddIn.HijackThisDotDe.ExtensionMethods;
using ReviveThis.Enums;
using ReviveThis.Helpers;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.HijackThisDotDe
{
  [Export(typeof (IAnalysisAddIn))]
  public class HijackThisDotDe : IAnalysisAddIn
  {
    #region private

    #region consts

    //(?:(?:name\=\"anl\"\s+?id\=\"anl\").*?(?:class\=\"thead\".*?\>.*?\<\/tr\>)(?:.*?\<tr\s+?class\=\"alt(?:1|2)\"\>.*?\<td.*?\>(?<actions>.*?)<\/td\>.*?\<td.*?\>(?<entry>.*?)\<.*?<\/td\>.*?\<td.*?\>.*?title\=\"(?<kind>.*?)\".*?alt.*?<\/td\>.*?\<td.*?\>(?<assessment>.*?)<\/td\>.*?\<td.*?\>(?<information>.*?)<\/td\>.*?\<\/tr\>)+)
    private const string REG_EX_PATTERN =
      @"(?:(?:name\=\""anl\""\s+?id\=\""anl\"").*?(?:class\=\""thead\"".*?\>.*?\<\/tr\>)(?:.*?\<tr\s+?class\=\""alt(?:1|2)\""\>.*?\<td.*?\>(?<actions>.*?)<\/td\>.*?\<td.*?\>(?<entry>.*?)\<.*?<\/td\>.*?\<td.*?\>.*?title\=\""(?<kind>.*?)\"".*?alt.*?<\/td\>.*?\<td.*?\>(?<assessment>.*?)<\/td\>.*?\<td.*?\>(?<information>.*?)(?:\s*?|\<.*?)<\/td\>.*?\<\/tr\>)+)";

    #endregion

    #endregion

    public string Author
    {
      get { return @"William David Cossey"; }
    }

    private Version _version = null;

    public Version Version
    {
      get
      {
        if (_version != null)
          return _version;

        return _version = new Version(1, 0, 0, 0);
      }
    }

    public string Name
    {
      get { return @"HijackThis.de Security"; }
    }

    public string[] Description
    {
      get
      {
        return new[]
        {
          "Log file analysis for http://www.hijackthis.de/",
          string.Empty,
          "HijackThis.de Support Forum: http://www.hijackthis-forum.de/forum.php",
          string.Empty,
          "www.hijackthis.de is produced and supervised by: Mathias Mattner"
        };
      }
    }

    private void Null()
    {

    }

    public void Dispose()
    {
      //Nothing to dispose.
    }
    
    public async Task<ICollection<IAnalysisResult>> Analyse(ICollection<IDetectionResultItem> items)
    {
      var result = new List<IAnalysisResult>();

      try
      {

        //name="action"
        //auswerten

        //name="langselect"
        //english

        //name="logfile"
        //

        //name="debuging"
        //0 //default
        //1

        //name="communitycheck"
        //1 //default
        //0

        //name="Submit"
        //Analyze

        var data = string.Join("\r\n", items.Select(s => s.LegacyString.Trim()));

        var postParameters = new Dictionary<string, string>()
        {
          {"action", "auswerten"},
          {"langselect", "english"},
          {"logfile", data},
          {"debuging", "0"},
          {"communitycheck", "1"},
          {"Submit", "Analyze"},
        };

        var cachedItems =
          items.Select(
            f =>
              new
              {
                Match = f /*.LegacyString*/,
                Name = f.LegacyString.Trim()
                  .Split(220)
              }).Distinct().ToList();

        var responseMessage = await MultipartFormData.Post(
          "http://www.hijackthis.de/#anl",
          "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.117 Safari/537.36",
          postParameters);

        var match = Regex.Match(responseMessage, REG_EX_PATTERN,
          RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace |
          RegexOptions.CultureInvariant);

        if (match.Success)
        {
          var i = 0;
          foreach (var capture in match.Groups["entry"].Captures)
          {
            var name = WebUtility.HtmlDecode(((Capture) capture).Value.Trim());

            if (!result.Any(f => f.Match.LegacyString.Trim()
              .Split(220).Equals(name)))
            {
              foreach (var resultItem in cachedItems.Where(
                f =>
                  f.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                .Select(item => new AnalysisResult {Match = item.Match}))
              {
                switch (match.Groups["kind"].Captures[i].Value.ToLower())
                {
                  case "safe.":
                    resultItem.Result = AnalyseResultType.Safe;
                    break;
                  case "nasty":
                    resultItem.Result = AnalyseResultType.Critical;
                    break;
                  //case "":
                  //  resultItem.Type = AnalyseResultType.Caution;
                  //  break;
                  default: //unknown
                    resultItem.Result = AnalyseResultType.Unknown;
                    break;
                }

                resultItem.Text = match.Groups["information"].Captures[i].Value.Trim();

                result.Add(resultItem);
              }
            }
            i++;
          }
        }

      }
      catch (Exception ex)
      {
        //MessageBox.Show(ex.Message);
        throw;
      }

      return result;
    }
  }
}
