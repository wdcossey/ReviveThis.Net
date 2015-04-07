using System;
using FirstFloor.ModernUI.Windows;

namespace ReviveThis.Interfaces
{
  public interface IToolAddIn : IAddInBase
  {
    IContent Content { get; }

    Guid Guid { get; }
  }
}