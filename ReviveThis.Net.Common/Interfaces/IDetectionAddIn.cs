using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviveThis.Interfaces
{
  public interface IDetectionAddIn : IAddInBase, ISortableAddIn
  {
    //IModule Default { get; }
    Task<ICollection<IDetectionResultItem>> Scan();
    
  }
}