﻿using Kendo.Mvc.UI;
using System;

namespace WebAI.Advisor.Areas.OperatingCompany.Models
{
    public class ActivityViewlModel : ISchedulerEvent
    {
        public int MeetingID { get; set; }
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

        public string RecurrenceRule { get; set; }
        public int? RecurrenceID { get; set; }
        public string RecurrenceException { get; set; }
        public bool IsAllDay { get; set; }
        public int? OwnerID { get; set; }
    }
}
