using ReviveThis.AddIn.GroupPolicy.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.GroupPolicy.Interfaces
{
  public interface IGroupPolicyResult : IDetectionResultItem
  {

    RegistryInformation RegistryInformation { get; }

    GroupPolicyResultType GroupPolicyResultType { get; }
  }
}