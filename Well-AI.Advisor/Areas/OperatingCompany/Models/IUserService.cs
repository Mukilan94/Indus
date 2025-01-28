
using System.Collections.Generic;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.Areas.OperatingCompany.Models
{
    public interface IUserService
    {
        IEnumerable<UserViewModel> Read();
        List<UserViewModel> GetAll();
        void Create(UserViewModel user);
        void Update(UserViewModel product);
    }
}
