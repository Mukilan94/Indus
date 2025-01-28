using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
   public interface ITargetRepository
    {
        ICollection<Target> GetTargetDetails();
        Target GetTargetDetail(string uid);
        bool TargetExists(string Uid);
        bool CreateTarget(Target target);
        bool UpdateTarget(Target target);
        bool DeleteTarget(Target target);
        bool Save();
    }
}
