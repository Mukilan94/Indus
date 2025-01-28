using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.Areas.OperatingCompany.Models
{
    public class UserService : IUserService
    {
        private ISession _session;

        public ISession Session { get { return _session; } }

        public UserService()
        {
            _session = WellAIAppContext.Current.Session;
        }

        public void Create(UserViewModel user)
        {
            var users = GetAll().ToList();
            var first = users.OrderByDescending(e => e.UserID).FirstOrDefault();
            var id = (first != null) ? first.UserID : System.Guid.Empty.ToString();

            user.UserID = id + 1;

            users.Add(user);

            Session.SetObjectAsJson("Users", users);
        }

        public List<UserViewModel> GetAll()
        {
            var result = Session.GetObjectFromJson<List<UserViewModel>>("Users");

            if (result == null)
            {
               /* result = new List<UserViewModel>
                {
                    new UserViewModel{ UserID=1, LastName="Richards",FirstName = "Bob", Email = "bob@micrisift.com", City="Washington", PhoneNumber="+12147845678",
                                    Permissions = new List<UserRole>{   new UserRole{ Id = 1, Title="AccountExecutiveRole" },
                                                                        new UserRole{ Id = 2, Title="ActivityRole" },
                                                                        new UserRole{ Id = 3, Title="DashboardRole" },
                                                                        new UserRole{ Id = 4, Title="HomeRole" } } },
                    new UserViewModel{ UserID=2, LastName="Brown",FirstName = "Jack", Email = "jack@gmail.com", City="New York", PhoneNumber="+122473561",
                                    Permissions = new List<UserRole>{   new UserRole{ Id = 1, Title="AccountExecutiveRole" },
                                                                        new UserRole{ Id = 2, Title="ActivityRole" },
                                                                        new UserRole{ Id = 3, Title="DashboardRole" },
                                                                        new UserRole{ Id = 4, Title="HomeRole" } }},
                    new UserViewModel{ UserID=3, LastName="Johnson",FirstName = "John", Email = "john@amazon.com", City="Washington", PhoneNumber="+121478459876",
                                    Permissions = new List<UserRole>{   new UserRole{ Id = 1, Title="AccountExecutiveRole" },
                                                                        new UserRole{ Id = 2, Title="ActivityRole" },
                                                                        new UserRole{ Id = 3, Title="DashboardRole" },
                                                                        new UserRole{ Id = 4, Title="HomeRole" } }},
                    new UserViewModel{ UserID=4, LastName="Brown",FirstName = "Donald", Email = "donald@upwork.com", City="Washington", PhoneNumber="+12345678901",
                                    Permissions = new List<UserRole>{   new UserRole{ Id = 1, Title="AccountExecutiveRole" },
                                                                        new UserRole{ Id = 2, Title="ActivityRole" },
                                                                        new UserRole{ Id = 3, Title="DashboardRole" },
                                                                        new UserRole{ Id = 4, Title="HomeRole" } }},
                };*/

                Session.SetObjectAsJson("Users", result);

            }

            return result;
        }

        public IEnumerable<UserViewModel> GetAllWithFilter(string userpart)
        {
            var result = Session.GetObjectFromJson<IList<UserViewModel>>("Users");

            if (result == null)
            {
                /*result = new UserViewModel[]
                {
                    new UserViewModel{ UserID=1, LastName="Richards",FirstName = "Bob", Email = "bob@micrisift.com", City="Washington", PhoneNumber="+12147845678",
                                    Permissions = new List<UserRole>{   new UserRole{ Id = 1, Title="AccountExecutiveRole" },
                                                                        new UserRole{ Id = 2, Title="ActivityRole" },
                                                                        new UserRole{ Id = 3, Title="DashboardRole" },
                                                                        new UserRole{ Id = 4, Title="HomeRole" } } },
                    new UserViewModel{ UserID=2, LastName="Brown",FirstName = "Jack", Email = "jack@gmail.com", City="New York", PhoneNumber="+122473561",
                                    Permissions = new List<UserRole>{   new UserRole{ Id = 1, Title="AccountExecutiveRole" },
                                                                        new UserRole{ Id = 2, Title="ActivityRole" },
                                                                        new UserRole{ Id = 3, Title="DashboardRole" },
                                                                        new UserRole{ Id = 4, Title="HomeRole" } }},
                    new UserViewModel{ UserID=3, LastName="Johnson",FirstName = "John", Email = "john@amazon.com", City="Washington", PhoneNumber="+121478459876",
                                    Permissions = new List<UserRole>{   new UserRole{ Id = 1, Title="AccountExecutiveRole" },
                                                                        new UserRole{ Id = 2, Title="ActivityRole" },
                                                                        new UserRole{ Id = 3, Title="DashboardRole" },
                                                                        new UserRole{ Id = 4, Title="HomeRole" } }},
                    new UserViewModel{ UserID=4, LastName="Brown",FirstName = "Donald", Email = "donald@upwork.com", City="Washington", PhoneNumber="+12345678901",
                                    Permissions = new List<UserRole>{   new UserRole{ Id = 1, Title="AccountExecutiveRole" },
                                                                        new UserRole{ Id = 2, Title="ActivityRole" },
                                                                        new UserRole{ Id = 3, Title="DashboardRole" },
                                                                        new UserRole{ Id = 4, Title="HomeRole" } }},
                };*/

                Session.SetObjectAsJson("Users", result);

            }

            var filtered = result.Where(x => x.FirstName.IndexOf(userpart) >= 0 || x.LastName.IndexOf(userpart) >= 0 || x.MiddleName.IndexOf(userpart) >= 0);

            return filtered;
        }

        public IEnumerable<UserViewModel> GetFirstLimit(int limit)
        {
            var result = Session.GetObjectFromJson<IList<UserViewModel>>("Users");

            if (result == null)
            {
                /*result = new UserViewModel[]
                {
                    new UserViewModel{ UserID=1, LastName="Richards",FirstName = "Bob", Email = "bob@micrisift.com", City="Washington", PhoneNumber="+12147845678",
                                    Permissions = new List<UserRole>{   new UserRole{ Id = 1, Title="AccountExecutiveRole" },
                                                                        new UserRole{ Id = 1, Title="ActivityRole" },
                                                                        new UserRole{ Id = 1, Title="DashboardRole" },
                                                                        new UserRole{ Id = 1, Title="HomeRole" } } },
                    new UserViewModel{ UserID=2, LastName="Brown",FirstName = "Jack", Email = "jack@gmail.com", City="New York", PhoneNumber="+122473561",
                                    Permissions = new List<UserRole>{   new UserRole{ Id = 1, Title="AccountExecutiveRole" },
                                                                        new UserRole{ Id = 1, Title="ActivityRole" },
                                                                        new UserRole{ Id = 1, Title="DashboardRole" },
                                                                        new UserRole{ Id = 1, Title="HomeRole" } }},
                    new UserViewModel{ UserID=3, LastName="Johnson",FirstName = "John", Email = "john@amazon.com", City="Washington", PhoneNumber="+121478459876",
                                    Permissions = new List<UserRole>{   new UserRole{ Id = 1, Title="AccountExecutiveRole" },
                                                                        new UserRole{ Id = 1, Title="ActivityRole" },
                                                                        new UserRole{ Id = 1, Title="DashboardRole" },
                                                                        new UserRole{ Id = 1, Title="HomeRole" } }},
                    new UserViewModel{ UserID=4, LastName="Brown",FirstName = "Donald", Email = "donald@upwork.com", City="Washington", PhoneNumber="+12345678901",
                                    Permissions = new List<UserRole>{   new UserRole{ Id = 1, Title="AccountExecutiveRole" },
                                                                        new UserRole{ Id = 1, Title="ActivityRole" },
                                                                        new UserRole{ Id = 1, Title="DashboardRole" },
                                                                        new UserRole{ Id = 1, Title="HomeRole" } }},
                };*/

                Session.SetObjectAsJson("Users", result);

            }

            var filtered = result.SkipLast(result.Count - limit);

            return filtered;
        }

        public IEnumerable<UserViewModel> Read()
        {
            return GetAll();
        }

        public void Update(UserViewModel user)
        {
            var users = GetAll();
            var target = users.FirstOrDefault(e => e.UserID == user.UserID);

            if (target != null)
            {
                target.FirstName = user.FirstName;
                target.MiddleName = user.MiddleName;
                target.LastName = user.LastName;
                target.Email = user.Email;
                target.City = user.City;
                

                target.Address = user.Address;
                target.AdditionalNotes = user.AdditionalNotes;
                target.PhoneNumber = user.PhoneNumber;
                if (user.Permissions != null)
                {
                    target.Permissions.Clear();
                    target.Permissions.AddRange(user.Permissions);
                }
            }

            Session.SetObjectAsJson("Users", users);
        }
    }
}
