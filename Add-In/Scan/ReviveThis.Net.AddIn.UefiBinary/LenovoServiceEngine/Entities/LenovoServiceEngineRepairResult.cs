using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.AddIn.UefiBinary.LenovoServiceEngine.Entities
{
  public class LenovoServiceEngineRepairResult: IDetectionRepairResult
  {
    public RepairResultType Result { get; }

    public string Text { get; }

    public LenovoServiceEngineRepairResult(RepairResultType result, string text)
    {
      Result = result;
      Text = text;
    }
  }
}