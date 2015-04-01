namespace ReviveThis.AddIn.ControlPanel.Enums
{
  public enum ControlIniResultType
  {
    /// <summary>
    /// Unknown ???
    /// </summary>
    Unknown = -1,

    /// <summary>
    /// Value from the registry.
    /// </summary>
    Registry = 0,

    /// <summary>
    /// Value from "Control.ini" file.
    /// </summary>
    File = 1,
  }
}