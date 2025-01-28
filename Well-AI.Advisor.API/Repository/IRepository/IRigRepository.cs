using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
   public interface IRigRepository
    {
        ICollection<Rig> GetRigDetails();
        Rig GetRigDetail(string uid);
        bool RigExists(string Uid);
        bool CreateRig(Rig rig);
        bool UpdateRig(Rig rig);
        bool DeleteRig(Rig rig);
        bool Save();
    }
}
