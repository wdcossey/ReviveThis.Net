using ReviveThis.Interfaces;

namespace ReviveThis.AddIn.AutoRun.AutoRun.Interfaces
{
  public interface IAutoRunStartUpResult : IDetectionResultItem, IAutoRunResult
  {
    string FancyName { get; }
    string Parameters { get; }
    string Path { get; }
    string ShortCut { get; }
  }
}