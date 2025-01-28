using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
    public interface IConvCoreRepository
    {
        ICollection<ConvCore> GetConvCoreDetails();
        ConvCore GetConvCoreDetail(string uid);
        bool ConvCoreExists(string Uid);
        bool CreateConvCore(ConvCore convCore);
        bool UpdateConvCore(ConvCore convCore);
        bool DeleteConvCore(ConvCore convCore);
        bool Save();
    }
}
