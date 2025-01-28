using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
   public interface ICoordinateReferenceSystemRepository
    {
        ICollection<CoordinateReferenceSystem> GetCoordinateReferenceSystemDetails();
        CoordinateReferenceSystem GetCoordinateReferenceSystemDetail(string uid);
        bool CoordinateReferenceSystemExists(string Uid);
        bool CreateCoordinateReferenceSystem(CoordinateReferenceSystem coordinateReferenceSystem);
        bool UpdateCoordinateReferenceSystem(CoordinateReferenceSystem coordinateReferenceSystem);
        bool DeleteCoordinateReferenceSystem(CoordinateReferenceSystem coordinateReferenceSystem);
        bool Save();
    }
}
