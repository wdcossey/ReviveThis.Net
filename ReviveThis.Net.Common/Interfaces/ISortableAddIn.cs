using ReviveThis.Enums;

namespace ReviveThis.Interfaces
{
  public interface ISortableAddIn
  {
    /// <summary>
    /// <para>Specifies the sorting order of the Module.<br/>
    /// This will affect the order in which the modules are run.</para>
    /// </summary>
    ScanResultType ResultType { get; } 
  }
}