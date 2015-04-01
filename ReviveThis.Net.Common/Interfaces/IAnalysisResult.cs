using ReviveThis.Enums;

namespace ReviveThis.Interfaces
{
  public interface IAnalysisResult
  {
    AnalyseResultType Result { get; set; }
    string Text { get; set; }
    IDetectionResultItem Match { get; set; }
  }
}