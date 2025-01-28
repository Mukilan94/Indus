
using System.Collections.Generic;

namespace WebAI.Advisor.Model.OperatingCompany.Models
{
    public interface IUserService
    {
        IEnumerable<UserViewModel> Read();
        List<UserViewModel> GetAll();
        void Create(UserViewModel user);
        void Update(UserViewModel product);
    }
}
