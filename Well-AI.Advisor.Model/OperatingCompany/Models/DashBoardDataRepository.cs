using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public partial class DashBoardDataRepository
    {
        public static IList<Rig> Rigs()
        {
            return new Rig[] {
            };
        }
    }

    public class DashboardModel
    {
        public int Accounts { get; set; }
        public int OnlineContacts { get; set; }
        public string LastContact { get; set; }
        public int ScheduledAppointments { get; set; }
        public int UpcomingServices { get; set; }
        public int BidsWon { get; set; }
        public int OpenFieldTickets { get; set; }
        public Map Map;

        public List<Rig> DepthData;
        public List<Rig> TimeData;
    }
    public class WellListItem
    {
        public string WellId { get; set; }
        public string Title { get; set; }
    }
    public class Rig
    {
        public double Value { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
    }

    public class Map
    {
        public double CenterLatitude { get; set; }
        public double CenterLongitude { get; set; }

        public IEnumerable<Marker> Markers { get; set; }

    }

    public class Marker
    {
        public double[] latlng { get; set; }
        public string Name { get; set; }

    }
    public class OperatingWellAIStatusViewModel
    {
        public string OprWellId { get; set; }
        public string OPrWellName { get; set; }
        public int? OprAssociatedTasksCount { get; set; }
        public int? OprPredictiveTasksCount { get; set; }
        public int? OprExemptionTasksCount { get; set; }
    }

    public class OperatingWellAIRAWStatusViewModel
    {
        public string WellId { get; set; }
        public string WellName { get; set; }
        public int? UpcomingCount { get; set; }
        public int? OngoingCount { get; set; }
        public int? ClosedCount { get; set; }
    }

    public class PredectiveSchedule : ISchedulerEvent
    {
        public int TaskID { get; set; }
        public string Title { get; set; }
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

        public int? OwnerID { get; set; }
        public string Description { get; set; }
        public string StartTimezone { get; set; }
        public string EndTimezone { get; set; }
        public string RecurrenceRule { get; set; }
        public int? RecurrenceID { get; set; }
        public string RecurrenceException { get; set; }
        public bool IsAllDay { get; set; }

        public string ActionDate { get; set; }
        public int? PredictiveTasksCount { get; set; }

    }
    public class TaskViewModel : ISchedulerEvent
    {
        public string TaskID { get; set; }
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

    public class OperatingTreeMap
    {
        public OperatingTreeMap(string name, int value, List<OperatingTreeMap> items)
        {
            Name = name;
            Value = value;
            Items = items;
        }

        public string Name { get; set; }
        public int Value { get; set; }

        public List<OperatingTreeMap> Items { get; set; }
    }



}
