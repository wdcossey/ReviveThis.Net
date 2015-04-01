using System;
using System.IO;
using System.Linq;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Enums;

namespace ReviveThis.Entities
{
  public class HostsFileReader
  {
    /// <summary>
    /// Lines read from the Hosts file.
    /// </summary>
    public string[] Lines { get; set; }

    /// <summary>
    /// Hosts file Attributes.
    /// </summary>
    public FileAttributes? Attributes { get; private set; }

    /// <summary>
    /// Hosts file Attributes (Formatted to a System.String).
    /// </summary>
    public string FormattedAttributes
    {
      get { return !Attributes.HasValue ? null : Attributes.Value.FormatToString(); }
    }

    /// <summary>
    /// Hosts file Location.
    /// </summary>
    public string Location { get; private set; }

    /// <summary>
    /// Hosts file type.
    /// </summary>
    public HostsFileType FileType { get; private set; }

    /// <summary>
    /// Number of lines in the Hosts file.
    /// </summary>
    public Int32 LineCount
    {
      get { return Lines == null ? 0 : Lines.Count(); }
    }

    public HostsFileReader(string[] lines, HostsFileType fileType, string location, FileAttributes? attributes)
    {
      Lines = lines;
      FileType = fileType;
      Location = location;
      Attributes = attributes;
    }

    public HostsFileReader(string[] lines, HostsFileType fileType)
      : this(lines, fileType, null, null)
    {

    }
  }
}