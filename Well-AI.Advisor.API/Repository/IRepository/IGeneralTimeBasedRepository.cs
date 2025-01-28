using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.API.Models;

namespace WellAI.Advisor.API.Repository.IRepository
{
   public interface IGeneralTimeBasedRepository
    {
        bool CreateGeneralTimeBased(GeneralTimeBased generalTimeBased);
        Guid GeneralTimeExists(string Uid);
        bool SaveWellDepthData(GeneralTimeBased generalTimeBasedObj);
    }
}
