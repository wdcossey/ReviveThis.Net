using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.Sample.CSharp.Classes
{
  public class SampleRepairResult: IDetectionRepairResult
  {
    public RepairResultType Result
    {
      get { return RepairResultType.Failed; }
    }

    public string Text
    {
      get { return @"This is only a sample"; }
    }


  }
}