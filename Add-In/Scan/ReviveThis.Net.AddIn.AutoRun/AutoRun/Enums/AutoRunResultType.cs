namespace ReviveThis.AddIn.AutoRun.AutoRun.Enums
{
  public enum AutoRunResultType
  {
    /// <summary>
    /// Unknown ???
    /// </summary>
    Unknown = -1,

    /// <summary>
    /// AutoRun value from the registry.
    /// </summary>
    Registry = 0,

    /// <summary>
    /// AutoRun value from "StartUp" directory.
    /// </summary>
    Folder = 1,
  }
}