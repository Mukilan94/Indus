using System;
using System.Collections.Generic;
using System.Text;
using WellAI.Advisor.DLL.Entity;

namespace WellAI.Advisor.Function.DrillPlan
{
    public interface IErrorLogRepository
    {
        bool CreateErrorLog(ErrorLog errorLog);
    }
}
