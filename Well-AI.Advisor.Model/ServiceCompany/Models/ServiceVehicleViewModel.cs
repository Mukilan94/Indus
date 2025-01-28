using System;
using System.Collections.Generic;
using System.Text;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class ServiceVehicleViewModel
    {
        public string Id { get; set; }
        public string TenantId { get; set; }
        public string AuthorId { get; set; }
        public string Samaaraid { get; set; }
        public string VehicleName { get; set; }
        [System.ComponentModel.DataAnnotations.Display(Name = "Technician")]
        public string TechnicianName { get; set; }
        public string TechnicianId { get; set; }
        [System.ComponentModel.DataAnnotations.Display(Name = "Project")]
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
