using System;
using System.Collections.Generic;
using System.Text;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    //Phase II Changes
    public class StagingTasksModel
    {
        public string Stage { get; set; }
        public string Task { get; set; }
        public DateTime? RunningDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Day { get; set; }
        public int? StageId { get; set; }
        public bool? isActive { get; set; }
        public string ServiceCategory { get; set; }
        public string WellId { get; set; }
    }
}
