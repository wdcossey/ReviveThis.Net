using ReviveThis.Enums;

namespace ReviveThis.Interfaces
{
  public interface IDetectionRepairResult
  {
    RepairResultType Result { get; }
    string Text { get; }
  }
}