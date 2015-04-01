namespace ReviveThis.Structs
{
  public struct CustomAddInSection
  {
    public string Id { get; set; }

    public string Text { get; set; }

    public CustomAddInSection(string id, string text) 
      : this()
    {
      Id = id;
      Text = text;
    }
  }
}