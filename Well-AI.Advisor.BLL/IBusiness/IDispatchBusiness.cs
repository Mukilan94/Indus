using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.ServiceCompany.Models;



namespace WellAI.Advisor.BLL.IBusiness
{
    public interface IDispatchBusiness
    {
        public Task<UserRoutes> GetUserCurrentRoutes(string userId,string dispatchAPIUrl);
        public Task<bool> UpdateUserRoutes(string userId,UserRoutes routes);

        public Task<UserRoutes> GetUserRoutes(string dispatchAPIUrl);
        public List<DispatchRoutesViewModel> GetDispatchDetailsList(string userid);
    }
}
