using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class DispatchDashboardModel
    {
        public int UserId { get; set; }
        public string DriverStatus { get; set; }
        public string DriverName { get; set; }
        public string Customer { get; set; }
        public string DestinationWell { get; set; }
        public string DestinationRig { get; set; }
        public string ETA { get; set; }
        public string ScheduledArrival { get; set; }
        public string MultiDestination { get; set; }
        public string Notes { get; set; }

        //[Display(Name = "Proposal Id")]
        //public string ProposalId { get; set; }
        //[Display(Name = "Auction Status")]
        //public string AuctionBidStatusName { get; set; }
        //public int? AuctionBidStatus { get; set; }
        //public DateTime Created { get; set; }
        //[Display(Name = "Operator")]
        //public string TenantID { get; set; }

        //[Display(Name = "Well")]
        //public string WellName { get; set; }
        //[Display(Name = "Rig")]
        //public string RigName { get; set; }
        //[Display(Name = "Job")]
        //public string JobId { get; set; }
        //[Display(Name = "Start")]
        //public DateTime AuctionStart { get; set; }
        //[Display(Name = "End")]
        //public DateTime AuctionEnd { get; set; }
        //[Display(Name = "Title")]
        //public string Subject { get; set; }
        //[Display(Name = "Summary")]
        //public string Summary { get; set; }
        //[Display(Name = "Description")]
        //public string Body { get; set; }
        //public DateTime? ProjectStartDate { get; set; }
        //[Display(Name = "Project Start Time")]
        //public DateTime? ProjectStartTime { get; set; }
        //[Display(Name = "Duration")]
        //public double? ProjectDuration { get; set; }
        //public decimal BidAmount { get; set; }
        //public DateTime BidTime { get; set; }
        //public int? BidStatusId { get; set; }
        //[Display(Name = "Attachement")]
        //public string BidStatusName { get; set; }
        //public string AuthorId { get; set; }
        //public string BidID { get; set; }
        //public string BidAttachmentUrl { get; set; }
        //public int? Depth { get; set; }

        //[Display(Name = "Number")]
        //public string AuctionNumber { get; set; }

        //[MaxLength(300)]
        //public string BidSummary { get; set; }

        //public int Bids { get; set; }

        //public string JobName { get; set; }
        //[Display(Name = "Modified Date")]
        //[DataType(DataType.DateTime)]
        //public DateTime? ModifyDate { get; set; }
        //public string SRVTenantId { get; set; }

        //int UserId, string DriverStatus, string DriverName, string Customer, string DestinationWell, string DestinationRig, string ETA,
        //string ScheduledArrival, string MultiDestination, string Notes,

        //public DispatchDashboardModel(string ProposalId,string WellName)
        //    public DispatchDashboardModel(int UserId, string DriverStatus, string DriverName, string Customer, string DestinationWell, string DestinationRig, string ETA,
        //string ScheduledArrival, string MultiDestination, string Notes)
        //{
        //    this.UserId = UserId;
        //    this.DriverStatus = DriverStatus;
        //    this.DriverName = DriverName;
        //    this.Customer = Customer;
        //    this.DestinationWell = DestinationWell;
        //    this.DestinationRig = DestinationRig;
        //    this.ETA = ETA;
        //    this.ScheduledArrival = ScheduledArrival;
        //    this.MultiDestination = MultiDestination;
        //    this.Notes = Notes;
        //    //this.ProposalId = ProposalId;
        //    //this.WellName = WellName;
        //}
    }

}
