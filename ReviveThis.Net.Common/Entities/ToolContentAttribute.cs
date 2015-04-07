using System;
using System.ComponentModel.Composition;
using FirstFloor.ModernUI.Windows;
using ReviveThis.Interfaces;

namespace ReviveThis.Entities
{
  [MetadataAttribute]
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public class ToolContentAttribute : ExportAttribute
  {
    public ToolContentAttribute(string contentUri)
      : base(typeof (IToolAddIn))
    {
      this.ContentUri = contentUri;
    }

    public string ContentUri { get; private set; }
  }
}