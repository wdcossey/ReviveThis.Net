using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.GroupPolicy.Enums;
using ReviveThis.AddIn.GroupPolicy.Interfaces;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.GroupPolicy.Entities
{
  public class GroupPolicyResult : IGroupPolicyResult
  {

    #region
    private const string INVALID_REG = "Invalid registry found";
    private const string FILE_MISSING = @"(file missing)";
    #endregion

    public ScanResultType ResultType { get; private set; }

    private string _string;

    public string LegacyString
    {
      get
      {
        if (!string.IsNullOrEmpty(_string))
          return _string;

        //"O6 - HKLM\Software\Policies\Microsoft\Internet Explorer\Toolbars\Restrictions present"
        return _string = string.Format("{0}{1} - {2}, {3}={4}", ResultType.FormatToString(), RegistryInformation.FormatViewToString(), RegistryInformation.Path, RegistryInformation.Name, Convert.ToBoolean(RegistryInformation.Value));

      }
    }

    public RegistryInformation RegistryInformation { get; private set; }

    public GroupPolicyResultType GroupPolicyResultType { get; private set; }

    public GroupPolicyResult(GroupPolicyResultType resultType, RegistryInformation registryInformation)
    {
      ResultType = ScanResultType.GroupPolicySettings;
      RegistryInformation = registryInformation;
      GroupPolicyResultType = resultType;
    }

    public GroupPolicyResult(GroupPolicyResultType resultType, RegistryHive hive, RegistryView view, string registryPath, string valueName, object value)
      : this (resultType, new RegistryInformation(hive, view, registryPath, valueName, value))
    {

    }

    public bool CanRepair
    {
      get { return false; }
    }

    public bool IsChecked
    {
      get { return false; }
    }

    public async Task<IDetectionRepairResult> Repair()
    {
      //await Task.FromResult(0);
      return null;
    }
  }
}