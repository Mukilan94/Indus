using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class ProviderDirectorySRVModel
    {
        public List<ProviderProfile> Providers;

        public ProviderProfile PreferredProvider;
        public ProviderProfile SecondaryProvider;
        public int Records { get; set; }
        public int Pending { get; set; }
        public int InsExpiring90days { get; set; }
    }

    public class ProviderProfile
    {
        [ScaffoldColumn(false)]
        public int ProviderId
        {
            get;
            set;
        }

        public string Name { get; set; }
        public string Approval { get; set; }
        public string Status { get; set; }
        public string PecStatus { get; set; }
        public string MSADocument { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public int Rating { get; set; }
        public UserViewSRVModel User { get; set; }
        public List<MSA> Msa { get; set; }
        public List<Insurance> Insurance { get; set; }
        public List<CurrentActivity> CurrentActivity { get; set; }
        public List<UpcomingActivity> UpcomingActivity { get; set; }
        public List<ServiceOffering> ServiceOffering { get; set; }
        public List<ProjectAuctionModel> Proposals { get; set; }
    }

    public class CurrentActivity
    {
        [ScaffoldColumn(false)]
        public int CurrentActivityId
        {
            get;
            set;
        }
        public string Title { get; set; }
    }

    public class UpcomingActivity
    {
        [ScaffoldColumn(false)]
        public int UpcomingActivityId
        {
            get;
            set;
        }
        public string Title { get; set; }
    }

    public class ServiceOffering
    {
        [ScaffoldColumn(false)]
        public int ServiceOfferId
        {
            get;
            set;
        }
        public string Title { get; set; }
    }

    public class MSA
    {
        [ScaffoldColumn(false)]
        public int MsaId
        {
            get;
            set;
        }

        public DateTime? Expiration { get; set; }
        public string Status { get; set; }
        public string Attachment { get; set; }
    }

    public class Insurance
    {
        [ScaffoldColumn(false)]
        public int InsId
        {
            get;
            set;
        }

        public DateTime? Expiration { get; set; }
        public string Status { get; set; }
        public string Attachment { get; set; }
    }
}
