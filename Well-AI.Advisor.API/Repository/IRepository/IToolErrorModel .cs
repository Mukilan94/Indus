using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
   public interface IToolErrorModelRepository
    {
        ICollection<ToolErrorModel> GetToolErrorModelDetails();
        ToolErrorModel GetToolErrorModelDetail(string uid);
        bool ToolErrorModelExists(string Uid);
        bool CreateToolErrorModel(ToolErrorModel toolErrorModel);
        bool UpdateToolErrorModel(ToolErrorModel toolErrorModel);
        bool DeleteToolErrorModel(ToolErrorModel toolErrorModel);
        bool Save();
    }
}
