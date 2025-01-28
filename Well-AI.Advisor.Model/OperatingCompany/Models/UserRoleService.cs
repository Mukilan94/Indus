
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
//using WebAI.Advisor.Helper;

namespace WebAI.Advisor.Model.OperatingCompany.Models
{
    public class UserRoleService
    {
        //private ISession _session;

        //public ISession Session { get { return _session; } }

        //public UserRoleService()
        //{
        //    _session = WellAIAppContext.Current.Session;
        //}

        //public void Create(UserRole role)
        //{
        //    var roles = GetAll().ToList();
        //    var first = roles.OrderByDescending(e => e.Id).FirstOrDefault();
        //    var id = (first != null) ? first.Id : 0;

        //    role.Id = id + 1;

        //    roles.Add(role);

        //    Session.SetObjectAsJson("UserRoles", roles);
        //}

        //public List<UserRole> GetAll()
        //{
        //    var result = Session.GetObjectFromJson<List<UserRole>>("UserRoles");

        //    if (result == null)
        //    {
        //        result = new List<UserRole>() {
        //                new UserRole { Id=1,Title="AccountExecutiveRole"},
        //                new UserRole { Id=2, Title="ActivityRole"},
        //                new UserRole { Id=3,Title="ChannelRole"},
        //                new UserRole { Id=4, Title="DashboardRole"},
        //                new UserRole { Id=5,Title="LeadRole"},
        //                new UserRole { Id=6, Title="OpportunityRole"},
        //                new UserRole { Id=7,Title="RatingRole"},
        //                new UserRole { Id=8, Title="StageRole"},
        //                new UserRole { Id=9, Title="HomeRole"},
        //                new UserRole { Id=10, Title="BranchRole"},
        //                new UserRole { Id=11, Title="CustomerRole"},
        //                new UserRole { Id=12, Title="ProductRole"},
        //                new UserRole { Id=13, Title="PurchaseOrderRole"}
        //        };

        //        Session.SetObjectAsJson("UserRoles", result);

        //    }

        //    return result;
        //}

        //public IEnumerable<UserRole> Read()
        //{
        //    return GetAll();
        //}

        //public void Update(UserRole role)
        //{
        //    var roles = GetAll();
        //    var target = roles.FirstOrDefault(e => e.Id == role.Id);

        //    if (target != null)
        //    {
        //        target.Title = role.Title;
        //    }

        //    Session.SetObjectAsJson("UserRoles", roles);
        //}
    }
}
