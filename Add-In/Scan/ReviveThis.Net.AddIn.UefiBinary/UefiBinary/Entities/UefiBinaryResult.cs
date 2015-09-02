using System.Threading.Tasks;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.UefiBinary.UefiBinary.Entities
{
  public class UefiBinaryResult : IDetectionResultItem, ICustomAddInSection
  {
    private readonly ScanResultType _resultType;
    private readonly string _text;
    private const string SECTION_ID = "WPBT";

    public UefiBinaryResult(ScanResultType type, string text)
    {
      _resultType = type;
      _text = text;
    }

    public ScanResultType ResultType
    {
      get { return _resultType; }
    }

    public string LegacyString
    {
      get { return string.Format("{0} - {1}.", SECTION_ID, _text);  }
    }

    public CustomAddInSection CustomAddInSection
    {
      get { return new CustomAddInSection(SECTION_ID, "Windows Platform Binary Table"); }
    }

    public bool CanRepair
    {
      get { return false; }
    }

    public Task<IDetectionRepairResult> Repair()
    {
      return null;
    }
  }
}