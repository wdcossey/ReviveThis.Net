namespace ReviveThis.Enums
{
  public enum IniScanMode
  {
    None = -1,

    /// <summary>
    /// 0 = check if value is infected
    /// </summary>
    ValueInfected = 0,

    /// <summary>
    /// 1 = check if value is present
    /// </summary>
    ValuePresent = 1,

    /// <summary>
    /// 2 = check if value is infected, in the Registry
    /// </summary>
    ValueInfectedRegistry = 2,

    /// <summary>
    /// 3 = check if value is present, in the Registry
    /// </summary>
    ValuePresentRegistry = 3,
  }
}