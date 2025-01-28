using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
    public interface IFormationMarkerRepository
    {
        ICollection<FormationMarker> GetFormationMarkerDetails();
        FormationMarker GetFormationMarkerDetail(string uid);
        bool FormationMarkerExists(string Uid);
        bool CreateFormationMarker(FormationMarker formationMarker);
        bool UpdateFormationMarker(FormationMarker formationMarker);
        bool DeleteFormationMarker(FormationMarker formationMarker);
        bool Save();
    }
}
