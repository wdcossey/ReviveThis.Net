namespace ReviveThis.Enums
{
  public enum RepairResultType
  {
    /// <summary>
    /// The result is unknown.
    /// </summary>
    Unknown,

    /// <summary>
    /// The repair failed.
    /// </summary>
    Failed,
     
    /// <summary>
    /// The repair was partially successful.
    /// </summary>
    Partial,
     
    /// <summary>
    /// The repair completed successfully.
    /// </summary>
    Successful,
  }
}