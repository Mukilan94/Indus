using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.API.Models;

namespace WellAI.Advisor.API.Repository.IRepository
{
   public interface IDrillingConnectionRepository
    {
        bool CreateDrillingConnection(DrillingConnection drillingConnection);
        bool DrillingConnectionExists(string WellId);
    }
}
