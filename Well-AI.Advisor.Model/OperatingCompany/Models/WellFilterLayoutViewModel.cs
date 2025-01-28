using System.Collections.Generic;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class WellFilterLayoutViewModel
    {
        public string SelectedWellId { get; set; }
        public List<WellViewModel> Wells { get; set; }
    }
}