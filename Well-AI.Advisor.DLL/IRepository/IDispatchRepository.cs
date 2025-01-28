using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.DLL.IRepository
{
    public interface IDispatchRepository
    {
        public Task<UserRoutes> GetUserCurrentRoutes(string userId, string dispatchApiUrl);
        public Task<bool> UpdateUserRoutes(string userId, UserRoutes routes);
        public Task<WellApiData> GetWellDetailsByApiNumberAsync(string text, string filterType);

        public Task<UserRoutes> GetUserRoutes(string dispatchApiUrl);
        public List<DispatchRoutesViewModel> GetDispatchDetailsList(string UserId);
        public List<DispatchRoutesViewModel> GetDispatchDetailsList_active(string UserId);

        public DispatchUserStatusCount GetDispatchStatuscount(string UserId);
    }
}
