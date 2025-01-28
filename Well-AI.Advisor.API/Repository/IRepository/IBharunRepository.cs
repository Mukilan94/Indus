using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
   public interface IBharunRepository
    {
        ICollection<Bharun> GetBharunDetails();
        Bharun GetBharunDetail(string Uid);
        bool BharunExists(string Uid);
        bool CreateBharun(Bharun bharun);
        bool UpdateBharun(Bharun bharun);
        bool DeleteBharun(Bharun bharun);
        bool Save();

    }
}
