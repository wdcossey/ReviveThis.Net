namespace ReviveThis.Enums
{
  public enum RegistryScanMode: int
  {
    None = -1,
    // iMode = 0 -> check if value is infected
    ValueInfected = 0,
    // iMode = 1 -> check if value is present
    ValuePresent = 1,
    // iMode = 2 -> check if regkey is present
    KeyPresent = 2,
  }
}