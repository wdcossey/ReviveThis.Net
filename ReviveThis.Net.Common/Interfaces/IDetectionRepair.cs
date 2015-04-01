using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviveThis.Interfaces
{
  public interface IDetectionRepair
  {
    bool CanRepair { get; }
    Task<IDetectionRepairResult> Repair();
  }
}