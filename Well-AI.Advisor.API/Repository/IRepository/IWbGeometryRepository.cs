using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
   public interface IWbGeometryRepository
    {
        ICollection<WbGeometry> GetWbGeometryDetails();
        WbGeometry GetWbGeometryDetail(string uid);
        bool WbGeometryExists(string Uid);
        bool CreateWbGeometry(WbGeometry wbGeometry);
        bool UpdateWbGeometry(WbGeometry wbGeometry);
        bool DeleteWbGeometry(WbGeometry wbGeometry);
        bool Save();
    }
}
