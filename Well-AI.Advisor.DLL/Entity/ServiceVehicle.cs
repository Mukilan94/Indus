using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("ServiceVehicle")]
    public class ServiceVehicle
    {
        [Key]
        public string Id{ get; set; }
        public string TenantId{ get; set; }
        public string AuthorId{ get; set; }
        public string Samsaraid{ get; set; }
        public string VehicleName { get; set; }
        public string Vin{ get; set; }
        public string Serial{ get; set; }
        public string Make{ get; set; }
        public string Model{ get; set; }
        public string Year{ get; set; }
        public string LicensePlate { get; set; }
	}
}
