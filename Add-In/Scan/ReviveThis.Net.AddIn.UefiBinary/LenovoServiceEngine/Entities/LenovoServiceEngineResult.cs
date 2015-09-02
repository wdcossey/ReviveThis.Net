using System.Threading.Tasks;
using ReviveThis.AddIn.UefiBinary.LenovoServiceEngine.Enums;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.UefiBinary.LenovoServiceEngine.Entities
{
  public class LenovoServiceEngineResult : IDetectionResultItem, ICustomAddInSection, IDetectionItemToolTip
  {
    private readonly LenovoServiceEngineTypes _type;
    private readonly string _text;
    private string _fileOrDirectory;
    private const string SECTION_ID = "LSE";

    public LenovoServiceEngineResult(LenovoServiceEngineTypes type, string fileOrDirectory, string text)
    {
      _type = type;
      _fileOrDirectory = fileOrDirectory;
      _text = text;
    }

    public LenovoServiceEngineTypes Type
    {
      get { return _type; }
    }

    public string FileOrDirectory
    {
      get { return _fileOrDirectory; }
    }

    public ScanResultType ResultType
    {
      get { return ScanResultType.CustomAddIn; }
    }

    public string LegacyString
    {
      get { return string.Format("{0} - {1}.", SECTION_ID, _text); }
    }

    public CustomAddInSection CustomAddInSection
    {
      get { return new CustomAddInSection(SECTION_ID, "Lenovo Service Engine (LSE) BIOS"); }
    }

    public bool CanRepair
    {
      get { return false; }
    }

    public Task<IDetectionRepairResult> Repair()
    {
      return null;
    }

    public string ToolTip
    {
      get { return FileOrDirectory; }
    }
  }
}