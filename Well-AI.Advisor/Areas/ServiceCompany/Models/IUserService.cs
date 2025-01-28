
using System.Collections.Generic;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.Areas.ServiceCompany.Models
{
    public interface IUserService
    {
        IEnumerable<UserViewSRVModel> Read();
        List<UserViewSRVModel> GetAll();
        void Create(UserViewSRVModel user);
        void Update(UserViewSRVModel product);
    }
}
