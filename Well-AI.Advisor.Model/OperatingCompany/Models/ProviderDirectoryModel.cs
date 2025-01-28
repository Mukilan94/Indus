using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class ProviderDirectoryModel
    {
        public List<ProviderProfile> Providers;

        public ProviderProfile PreferredProvider;
        public ProviderProfile SecondaryProvider;
        public int Records { get; set; }
        public int Pending { get; set; }
        public int InsExpiring90days { get; set; }
        public int ComplienceAlert { get; set; }
    }

    public class ProviderProfile
    {
        private byte m_Preferred;
        [ScaffoldColumn(false)]
        public string ProviderId { get; set; }

        public string Name { get; set; }
        public string CompanyId { get; set; }
        public string Approval { get; set; }
        public string ApprovalId { get; set; }
        public string Status { get; set; }
        public string StatusId { get; set; }
        public string PecStatus { get; set; }
        public string PecStatusId { get; set; }

        public string InsuranceId { get; set; }
        public string InsuranceDocument { get; set; }
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
        public byte Preferred
        {
            get
            {
                return m_Preferred;
            }
            set
            {
                m_Preferred = value;
                if (Convert.ToByte(value) == 1)
                {
                    this.PreferredStatus = "pending Review";
                }
                else if (Convert.ToByte(value) == 2)
                {
                    this.PreferredStatus = "Authorized";
                }
                else if (Convert.ToByte(value) == 3)
                {
                    this.PreferredStatus = "Preferred";
                }
                else if (Convert.ToByte(value) == 4)
                {
                    this.PreferredStatus = "Welcome";
                }
            }
        }
        //Phase II Changes - 02/26/2021 - Add the Preferred Status 
        public string PreferredStatus { get; set; }

        public bool Secondary { get; set; }
        public UserViewModel User { get; set; }
        public List<MSA> Msa { get; set; }
        public List<Insurance> Insurance { get; set; }
        public List<CurrentActivity> CurrentActivity { get; set; }
        public List<UpcomingActivity> UpcomingActivity { get; set; }
        public List<ServiceOffering> ServiceOffering { get; set; }
        public List<ProjectAuctionModel> Proposals { get; set; }
        public List<RigsDepthPermission_Model> RigDepth { get; set; }
    }

    public class CurrentActivity
    {
        [ScaffoldColumn(false)]
        public string CurrentActivityId { get; set; }
        public string Title { get; set; }
    }

    public class UpcomingActivity
    {
        [ScaffoldColumn(false)]
        public string UpcomingActivityId
        {
            get;
            set;
        }
        public string Title { get; set; }
    }

    public class ServiceOffering
    {
        [ScaffoldColumn(false)]
        public string ServiceOfferId
        {
            get;
            set;
        }
        public string Title { get; set; }
    }

    public class ProviderCompany
    {
        public string CompanyId { get; set; }
        public string Name { get; set; }
    }

    public class MSA
    {
        [ScaffoldColumn(false)]
        public string MsaId { get; set; }

        public DateTime? Expiration { get; set; }
        public string Status { get; set; }
        public string Attachment { get; set; }
        public string Value { get; set; }
        public string CompanyId { get; set; }

        public bool? IsApproved { get; set; }
        //Phase II Changes - 02/08/2021
        public DateTime? FileUploadTime { get; set; }
        //Phase II Changes - 05/18/2021 - MSA Permission
        public bool IsPermitted { get; set; }

    }

    public class Insurance
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

        public string OprTenantId { get; set; }
        public string Directory { get; set; }
    }

    public class RigsDepthPermission_Model
    {
        public string ID { get; set; }
        public string RigId { get; set; }
        public string WellId { get; set; }
        public string OprTenantId { get; set; }
        public string SerTenantId { get; set; }
        public bool? DepthPermission { get; set; }
        public string RigName { get; set; }
        public string WellName { get; set; }
        public string PreferredStatus { get; set; }
        [Display(Name = "Prediction")]
        public bool WellPrediction { get; set; }
    }


}
