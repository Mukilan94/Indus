using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
   public interface IStimJobRepository
    {
        ICollection<StimJob> GetStimJobDetails();
        StimJob GetStimJobDetail(string uid);
        bool StimJobExists(string Uid);
        bool CreateStimJob(StimJob stimJob);
        bool UpdateStimJob(StimJob stimJob);
        bool DeleteStimJob(StimJob stimJob);
        bool Save();
    }
}
