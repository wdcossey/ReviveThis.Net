using System;

namespace ReviveThis.Interfaces
{
  public interface IModule: IDisposable
  {
    //IModule Default { get; }
    void Execute(); 
  }
}