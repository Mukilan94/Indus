using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.API.Models;

namespace WellAI.Advisor.API.Repository.IRepository
{
   public interface IDrillingDepthBasedRepository
    {
        bool CreateDrillingDepthBased(DRILLINGDEPTHBASED drillingDepthBased);
        Guid DrillingDepthBasedExists(string WellId);
        bool SaveWellDepthData(DRILLINGDEPTHBASED drillingDepthBased);
        
    }
}
