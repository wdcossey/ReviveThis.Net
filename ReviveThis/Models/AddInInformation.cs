using System;
using ReviveThis.Interfaces;

namespace ReviveThis.Models
{
  public class AddInInformation
  {

    public string Author
    {
      get { return Base.Author; }
    }

    public Version Version
    {
      get { return Base.Version; }
    }

    public string Name
    {
      get { return Base.Name; }
    }

    public string[] Description
    {
      get { return Base.Description; }
    }

    public string FileName
    {
      get { return Base.GetType().Assembly.ManifestModule.ScopeName; }
    }

    private IAddInBase Base { get; set; }

    public AddInInformation(IAddInBase @base)
    {
      Base = @base;
    }
  }
}