using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    [Table("CorporateProfile")]
    public class CorporateProfile
    {
        [Key]
        [Required]
        [StringLength(254)]
        public string ID { get; set; }
        [StringLength(500)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Website { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        [StringLength(500)]
        public string Address1 { get; set; }
        [StringLength(500)]
        public string Address2 { get; set; }
        [StringLength(254)]
        public string City { get; set; }
        [StringLength(50)]
        public string Country { get; set; }
        [StringLength(50)]
        public string Zip { get; set; }
        [StringLength(50)]
        public string State { get; set; }
        public string LogoPath { get; set; }
        [StringLength(254)]
        public string UserId { get; set; }
        [StringLength(40)]
        public string TenantId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CServices { get; set; }

        [StringLength(500)]
        public string BillingAddress1 { get; set; }
        [StringLength(500)]
        public string BillingAddress2 { get; set; }
        [StringLength(500)]
        public string BillingCity { get; set; }
        [StringLength(50)]
        public string BillingZip { get; set; }
        [StringLength(500)]
        public string BillingState { get; set; }
        [StringLength(50)]
        public string BillingPhone { get; set; }
        [StringLength(500)]
        public string BillingEmail { get; set; }
    }

    public class CorporateProfileViewModel
    {
        [Key]
        [Required]
        [StringLength(254)]
        public string ID { get; set; }
        [StringLength(500)]
        [Required]
        public string Name { get; set; }
        [StringLength(500)]
        [Required]
        public string Website { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        [StringLength(500)]
        [Required]
        public string Address1 { get; set; }
        [StringLength(500)]
        public string Address2 { get; set; }
        [StringLength(254)]
        [Required]
        public string City { get; set; }
        [StringLength(50)]
        [Required]
        public string Country { get; set; }
        [StringLength(50)]
        public string Zip { get; set; }
        [StringLength(50)]
        [Required]
        public string State { get; set; }
        public string LogoPath { get; set; }
        [StringLength(254)]
        public string UserId { get; set; }
        [StringLength(40)]
        public string TenantId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CServices { get; set; }
        public RigViewModel RigList { get; set; }

}

public class AddCorporateProfile: CorporateProfileViewModel
    {
        public IFormFile logofiles { get; set; }
        public string logofilesNull { get; set; }
    }

    public class ProviderLocator
    {
        [ScaffoldColumn(false)]
        public int Id
        {
            get;
            set;
        }

        public string Vehicle { get; set; }
        public string Status { get; set; }
        public string Speed { get; set; }

        public DateTime? Reported
        {
            get;
            set;
        }
    }

    public class OpratoreVehicleViewModel
    {
        public string Id { get; set; }
        public string TenantId { get; set; }
        public string AuthorId { get; set; }
        public string Samaaraid { get; set; }
        public string VehicleName { get; set; }
        [System.ComponentModel.DataAnnotations.Display(Name = "Driver")]
        public string TechnicianName { get; set; }
        public string TechnicianId { get; set; }
        [System.ComponentModel.DataAnnotations.Display(Name = "Location")]
        public string ProjectName { get; set; }
        public string ProposalId { get; set; }
        public string Vin { get; set; }
        public string Serial { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string LicensePlate { get; set; }
        public string WellId { get; set; }
        public string ProjectId { get; set; }
        [System.ComponentModel.DataAnnotations.Display(Name = "Provider")]
        public string OperatorName { get; set; }
        public string RigName { get; set; }
    }

    public class VechicleLocation
    {
        public DateTime time { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double heading { get; set; }
        public double speed { get; set; }
        public ReverseGeo reverseGeo { get; set; }
    }
    public class ReverseGeo
    {
        public string formattedLocation { get; set; }
    }

}
