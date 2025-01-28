using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
   public interface ITubularRepository
    {
        ICollection<Tubular> GetTubularDetails();
        Tubular GetTubularDetail(string uid);
        bool TubularExists(string Uid);
        bool CreateTubular(Tubular tubular);
        bool UpdateTubular(Tubular tubular);
        bool DeleteTubular(Tubular tubular);
        bool Save();
    }
}
