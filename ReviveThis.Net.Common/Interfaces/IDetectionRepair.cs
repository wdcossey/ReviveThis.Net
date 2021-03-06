﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviveThis.Interfaces
{
  public interface IDetectionRepair
  {
    bool CanRepair { get; }

    bool IsChecked { get; }
    
    Task<IDetectionRepairResult> Repair();
  }
}