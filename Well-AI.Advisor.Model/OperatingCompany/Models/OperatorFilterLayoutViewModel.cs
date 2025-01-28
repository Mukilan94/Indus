using System.Collections.Generic;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class OperatorFilterLayoutViewModel
    {
        public string SelectedOperatorId { get; set; }
        public List<OperatingProviderProfile> Operators { get; set; }
    }
}