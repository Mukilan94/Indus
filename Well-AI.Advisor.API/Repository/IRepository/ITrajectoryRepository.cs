using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
    public interface ITrajectoryRepository
    {
        ICollection<Trajectory> GetTrajectoryDetails();
        Trajectory GetTrajectoryDetail(string uid);
        bool TrajectoryExists(string Uid);
        bool CreateTrajectory(Trajectory trajectory);
        bool UpdateTrajectory(Trajectory trajectory);
        bool DeleteTrajectory(Trajectory trajectory);
        bool Save();
    }
}
