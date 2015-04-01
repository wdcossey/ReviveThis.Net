using System;
using System.Reflection;

namespace ReviveThis.Interfaces
{
  public interface IModuleInformation
  {    
    /// <summary>
    /// Author of the Add-In
    /// </summary>
    string Author { get; }

    /// <summary>
    /// Version of the Add-In
    /// </summary>
    Version Version { get; }

    /// <summary>
    /// Name of the Add-In
    /// </summary>
    string Name { get; }

    /// <summary>
    /// <para>Description of the Add-In<br/>
    /// What does the Add-In do..?</para> 
    /// </summary>
    string[] Description { get; }
   
  }
}