using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using FirstFloor.ModernUI.Windows;
using ReviveThis.Interfaces;

namespace ReviveThis.Entities
{
  public class AddInBootStrapper
  {
    /// <summary>
    /// List of Detection Add-Ins
    /// </summary>
    [ImportMany(typeof(IDetectionAddIn))]
    public IEnumerable<IDetectionAddIn> Detection { get; set; }
    
    /// <summary>
    /// List of Analysis Add-Ins
    /// </summary>
    [ImportMany(typeof(IAnalysisAddIn))]
    public IEnumerable<IAnalysisAddIn> Analysis { get; set; }

    /// <summary>
    /// List of Tool Add-Ins
    /// </summary>
    [ImportMany(typeof(IToolAddIn))]
    public IEnumerable<IToolAddIn> Tools { get; set; }
    //public IEnumerable<Lazy<IToolAddIn, IAddInContentMetadata>> Tools { get; set; }
    ////public Lazy<IContent, IContentMetadata>[] Contents { get; set; }


    #region Items
    private IEnumerable<IAddInBase> _items;

    /// <summary>
    /// Returns all Add-Ins
    /// </summary>
    /// <returns></returns>
    //[ImportMany(typeof(IAddInBase))]
    public IEnumerable<IAddInBase> Items
    {
      get
      {
        if (_items != null && _items.Any())
          return _items;

        var items = new List<IAddInBase>();
        items.AddRange(Detection);
        items.AddRange(Analysis);
        items.AddRange(Tools/*.Select(s => s.Value)*/);

        return _items = items;
      }
    }
    #endregion

  }
}