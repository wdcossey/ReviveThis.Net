using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.Structs
{
  public class AnalysisResult : IAnalysisResult
  {
    public AnalyseResultType Result { get; set; }

    public string Text { get; set; }

    public IDetectionResultItem Match { get; set; }

    public AnalysisResult()
    {
      
    }
    public AnalysisResult(AnalyseResultType type, string text, IDetectionResultItem match)
      : this()
    {
      Result = type;
      Text = text;
      Match = match;
    }
  }
}