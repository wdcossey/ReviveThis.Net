using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ReviveThis.Interfaces;

namespace ReviveThis.Entities
{
  public class AddInBootStrapper
  {
    /// <summary>
    /// Holds a list of all the Scan Add-Ins
    /// </summary>
    [ImportMany(typeof(IDetectionAddIn))]
    public IEnumerable<IDetectionAddIn> ScanAddIns { get; set; }

    /// <summary>
    /// Holds a list of all the Analysis Add-Ins
    /// </summary>
    [ImportMany(typeof(IAnalysisAddIn))]
    public IEnumerable<IAnalysisAddIn> AnalysisAddIns { get; set; }

    #region Items

    private IList<IAddInBase> _items;

    /// <summary>
    /// Returns all Add-Ins
    /// </summary>
    /// <returns></returns>
    public IList<IAddInBase> Items
    {
      get
      {
        if (_items != null && _items.Any())
          return _items;

        var items = new List<IAddInBase>();
        items.AddRange(ScanAddIns);
        items.AddRange(AnalysisAddIns);

        return _items = items;
      }
    }
    #endregion

  }
}