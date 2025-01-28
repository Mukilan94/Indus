using System;
using System.Collections.Generic;
using System.Text;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class UsersOptions
    {
        public bool? IsDispatchUser { get; set; }
        public string DispatchNotes { get; set; }
        public string Locations { get; set; }
        public string CurrentStatus { get; set; }
    }
}
