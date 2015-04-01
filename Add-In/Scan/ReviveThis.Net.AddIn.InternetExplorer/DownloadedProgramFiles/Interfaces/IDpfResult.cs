using ReviveThis.AddIn.InternetExplorer.DownloadedProgramFiles.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.InternetExplorer.DownloadedProgramFiles.Interfaces
{
  public interface IDpfResult : IDetectionResultItem
  {
    DpfResultType DpfResultType { get; }

    RegistryInformation RegistryInformation { get; }

    string Clsid { get; }

    /// <summary>
    /// Title reported by CLSID
    /// </summary>
    string Title { get; }
    
    /// <summary>
    /// Path of the CodeBase (.cab).
    /// </summary>
    string CodeBase { get; }
    
    /// <summary>
    /// Verify if the CodeBase (.cab) file exists.
    /// </summary>
    bool CodeBaseExists { get; }

    /// <summary>
    /// Path to the .inf File
    /// </summary>
    string InfFile { get; }

    /// <summary>
    /// List of files reported
    /// </summary>
    string[] FileList { get; }
  }
}