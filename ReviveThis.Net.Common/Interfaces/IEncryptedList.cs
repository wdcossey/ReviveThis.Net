using System.Collections.Generic;

namespace ReviveThis.Interfaces
{
  public interface IEncryptedData
  {
    IEncryptedData Default { get; }
    string[] Array { get; }
    List<string> List { get; }
  }
}