using System;

namespace ReviveThis.Entities.Attributes
{
  [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
  public class ScanResultSectionAttribute: Attribute
  {
    private string _sectionId;
    public string SectionId
    {
      get
      {
        return string.IsNullOrEmpty(_sectionId) ? "??" : _sectionId;
      }
      internal set { _sectionId = value; }
    }

    private string _text;
    public string Text
    {
      get { return string.IsNullOrEmpty(_text) ? "(undefined)" : _text; }
      internal set { _text = value; }
    }

    public ScanResultSectionAttribute(string sectionId, string text = null)
    {
      SectionId = sectionId;
      Text = text;
    }
  }
}