using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{ 
   
    public class DashboardSRVModel
    {
        public int Accounts { get; set; }
        public int OnlineContacts { get; set; }
        public string LastContact { get; set; }
        public int ScheduledAppointments { get; set; }
        public int UpcomingServices { get; set; }
        public int BidsWon { get; set; }
        public int OpenFieldTickets { get; set; }
        public List<Rig> DepthData;
        public List<Rig> TimeData;
        public List<CompanyDepthRigModel> CompanyDepthRigModel;
        public List<WellAIStatusViewModel> WellAIStatusViewModel;
        public List<WellAIRAWStatusViewModel> WellAIRAWStatusViewModel;
        public Map Map;
        public List<PredectiveSchedule> PredectiveSchedule;
    }
    public class CompanyDepthRigModel
    {
        public string RigID { get; set; }
        public List<Provider> ServiceProvider;
        public List<Details> ServiceDetails;
        public List<AvaliableRFP> AvaliableRFP;
        public List<SubmittedProposal> SubmittedProposal;
        public continfo continfo;
        public List<RigDepth> RigDepth;
        public string RigNo { get; set; }
        public UserViewSRVModel User { get; set; }

    }

    public class Provider
    {
        public int srvId { get; set; }
        public string Title { get; set; }
    }
    public class Rig
    {
        public double Value { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string WellName { get; set; }
        public List<CompanyDepthRigModel> CompanyDepthRigModel;

    }

    public class continfo
    {
        public string RigUser { get; set; }
        public int ServiceRigNo { get; set; }
        public bool ContactUserStatus { get; set; }
    }
    public class Details
    {
        [ScaffoldColumn(false)]
        public string UpcommingService { get; set; }
        public int Depth { get; set; }
        public int Day { get; set; }
        public string RFP { get; set; }
        public string Status { get; set; }
    }
   
    public class AvaliableRFP
    {
        public string RFP { get; set; }
    }

    public class SubmittedProposal
    {
        public string Proposals { get; set; }
    }

    public class RigDepth
    {
        public RigDepth(string day, int? depth, int? premodel)
        {
            Day = day;
            Depth = depth;
            Premodel = premodel;
        }
        public string Day { get; set; }
        public int? Depth { get; set; }
        public int? Premodel { get; set; }
    }


    public class WellAIStatusViewModel
    {
        public string WellId { get; set; }
        public string WellName { get; set; }
        public int? AssociatedTasksCount { get; set; }
        public int? PredictiveTasksCount { get; set; }
        public int? ExemptionTasksCount { get; set; }
    }

    public class WellAIRAWStatusViewModel
    {
        public string WellId { get; set; }
        public string WellName { get; set; }
        public int? UpcomingCount { get; set; }
        public int? OngoingCount { get; set; }
        public int? ClosedCount { get; set; }
    }


    public class TaskTable
    {
        [Key]
        public string Tasktable_id { get; set; }
        public string Welltask_id { get; set; }
    }

    public class TaskStatus
    {
        [Key]
        public string Taskstatus_id { get; set; }
        public string Tasktable_id { get; set; }
        public int? Taskstatus { get; set; }
        public string Well_ID { get; set; }
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
    public class GeoLocations
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string address { get; set; }
    }
    public class PopulationUSA
    {
        public PopulationUSA(string name, int value, List<PopulationUSA> items)
        {
            Name = name;
            Value = value;
            Items = items;
        }

        public string Name { get; set; }
        public int Value { get; set; }

        public List<PopulationUSA> Items { get; set; }
    }

    public class WellAIPredectiveSchedule
    {
        public int? PredictiveTasksCount { get; set; }
        public DateTime ActionDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
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

}

