namespace ReviveThis.Interfaces
{
  public interface IIniResultItem : IDetectionResultItem
  {
    string FileName { get; }
    string SecionName { get; }
    string ValueName { get; }
    object Value { get; }
    object DefaultValue { get; }
  }
}