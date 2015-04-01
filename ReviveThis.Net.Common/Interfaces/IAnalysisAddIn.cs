using System.Collections.Generic;
using System.Threading.Tasks;
using ReviveThis.Structs;

namespace ReviveThis.Interfaces
{
  public interface IAnalysisAddIn : IAddInBase
  {
    //Task<ICollection<AnalysisResult>> Analyse(ICollection<IScanResultItem> items);
    Task<ICollection<IAnalysisResult>> Analyse(ICollection<IDetectionResultItem> items);
    //Task Analyse();
  }
}