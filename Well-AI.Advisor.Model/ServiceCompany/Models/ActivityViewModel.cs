using Kendo.Mvc.UI;
using System;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class ActivityViewModel : ISchedulerEvent
    {
        public string ProjectId { get; set; }
        [System.ComponentModel.DisplayName("Project")]
        public string Title { get; set; }
        public string Description { get; set; }

        private DateTime start;

        public DateTime Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value.ToUniversalTime();
            }
        }

        public string StartTimezone { get; set; }

        private DateTime end;

        public DateTime End
        {
            get
            {
                return end;
            }
            set
            {
                end = value.ToUniversalTime();
            }
        }

        public string EndTimezone { get; set; }
        public string TenantId { get; set; }
        public string RecurrenceRule { get; set; }
        public int? RecurrenceID { get; set; }
        public string RecurrenceException { get; set; }
        public bool IsAllDay { get; set; }
        public int? ProjectStatus { get; set; }
        [System.ComponentModel.DataAnnotations.Display(Name ="Project Status")]
        public string ProjectStatusName { get; set; }
        public bool ActivityIsTask { get; set; }
    }
}
