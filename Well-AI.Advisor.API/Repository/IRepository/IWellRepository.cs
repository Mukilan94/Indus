using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
   public interface IWellRepository
    {
        ICollection<Well> GetWellDetails();
        Well GetWellDetail(string uid);
        bool WellExists(string Uid);
        bool CreateWell(Well well);
        bool UpdateWell(Well well);
        bool DeleteWell(Well well);
        bool Save();
    }
}
