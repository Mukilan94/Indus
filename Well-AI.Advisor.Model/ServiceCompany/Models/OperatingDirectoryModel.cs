using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class OperatingDirectoryModel
    {
        public List<OperatingProviderProfile> Providers;

        public OperatingProviderProfile PreferredProvider;
        public OperatingProviderProfile SecondaryProvider;
        public int Records { get; set; }
        public int Pending { get; set; }
        public int InsExpiring90days { get; set; }
    }

    public class OperatingProviderProfile
    {

        [ScaffoldColumn(false)]
        public string ProviderId { get; set; }
        public string Name { get; set; }
        public string CompanyId { get; set; }// Tenantid of Opertor Company
        public string Approval { get; set; }
        public string ApprovalId { get; set; }
        public string Status { get; set; }
        public string StatusId { get; set; }
        public string PecStatus { get; set; }
        public string PecStatusId { get; set; }
        public DateTime? InsuranceStart { get; set; }
        public DateTime? InsuranceExpire { get; set; }
        public string MSADocumentId { get; set; }
        public string MSADocument { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string State { get; set; }
        public int? Rating { get; set; }
        //Phase II Changes - 02/26/2021 - Change Preferred Status type to Byte (1-Welcome,2-Authorized,3-Preferred)
        public bool Preferred { get; set; }
        public bool Secondary { get; set; }

        //Phase II - 05/19/2021
        public string InsuranceId { get; set; }
        public string InsuranceDocument { get; set; }
        public string ServiceTenantId { get; set; }

        public UserViewSRVModel User { get; set; }
        public List<ServiceMSA> Msa { get; set; }
        public List<ServiceInsurance> Insurance { get; set; }
        public List<ServiceCurrentActivity> CurrentActivity { get; set; }
        public List<ServiceUpcomingActivity> UpcomingActivity { get; set; }
        public List<ServiceOfferingSRV> ServiceOffering { get; set; }
        public List<ProjectAuctionModel> Proposals { get; set; }
        public OperatingCompany.Models.CorporateProfile CorporateProfile { get; set; }
        public string RigName { get; set; }
        public string RigId { get; set; }

    }

    public class ServiceCurrentActivity
    {
        [ScaffoldColumn(false)]
        public string CurrentActivityId { get; set; }
        public string Title { get; set; }
    }

    public class ServiceUpcomingActivity
    {
        [ScaffoldColumn(false)]
        public string UpcomingActivityId
        {
            get;
            set;
        }
        public string Title { get; set; }
    }

    public class ServiceOfferingSRV
    {
        [ScaffoldColumn(false)]
        public string ServiceOfferId
        {
            get;
            set;
        }
        public string Title { get; set; }
    }

    public class ServiceMSA
    {
        [ScaffoldColumn(false)]
        public string MsaId { get; set; }

        public DateTime? Expiration { get; set; }
        public string Status { get; set; }
        public string Attachment { get; set; }
        public string Value { get; set; }
        //Phase II changes - 02/08/2021
        public DateTime? FileUploadTime { get; set; }
    }

    public class ServiceInsurance
    {
        [ScaffoldColumn(false)]
        public string InsId
        {
            get;
            set;
        }

        public DateTime? Start { get; set; }
        public DateTime? Expiration { get; set; }
        public string Status { get; set; }
        public string Attachment { get; set; }
        public string Value { get; set; }
    }

    public class SubscribeOperators
    {
        public string ProviderId { get; set; }
        public string Name { get; set; }
        public string CompanyId { get; set; }// Tenantid of Opertor Company
        public string Approval { get; set; }
        public string ApprovalId { get; set; }
        public string Status { get; set; }
        public string StatusId { get; set; }
        public string PecStatus { get; set; }
        public string PecStatusId { get; set; }
        public DateTime? InsuranceStart { get; set; }
        public DateTime? InsuranceExpire { get; set; }
        public string MSADocumentId { get; set; }
        public string MSADocument { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string State { get; set; }
        public int? Rating { get; set; }
        //Phase II Changes - 02/26/2021 - Change Preferred Status type to Byte (1-Welcome,2-Authorized,3-Preferred)
        public byte Preferred { get; set; }
        public bool Secondary { get; set; }
        public string RigName { get; set; }
        public List<string> RigId { get; set; }
    }

   
}
