namespace ReviveThis.Enums
{
  public enum AnalyseResultType
  {
    /// <summary>
    /// The result is unknown.
    /// </summary>
    Unknown,

    /// <summary>
    /// The result appears to be safe.
    /// </summary>
    Safe,

    /// <summary>
    /// The result is flagged with a warning.
    /// </summary>
    Caution,

    /// <summary>
    /// The result is flagged as nasty (is harmful).
    /// </summary>
    Critical,
  }
}