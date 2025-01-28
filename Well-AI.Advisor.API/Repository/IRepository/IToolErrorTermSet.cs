using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
  public  interface IToolErrorTermSetRepository
    {
        ICollection<ToolErrorTermSet> GetToolErrorTermSetDetails();
        ToolErrorTermSet GetToolErrorTermSetDetail(string uid);
        bool ToolErrorTermSetExists(string Uid);
        bool CreateToolErrorTermSet(ToolErrorTermSet toolErrorTermSet);
        bool UpdateToolErrorTermSet(ToolErrorTermSet toolErrorTermSet);
        bool DeleteToolErrorTermSet(ToolErrorTermSet toolErrorTermSet);
        bool Save();
    }
}
