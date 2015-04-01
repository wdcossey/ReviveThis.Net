using System;
using ReviveThis.Interfaces;

namespace ReviveThis.Structs
{
  public struct UpdateScanContent
  {
    public Type Content { get; private set; }

    //public WeakAction Action;
    //public object Token;


    public UpdateScanContent(Type content)
      : this()
    {
      Content = content;
    }
  }
}