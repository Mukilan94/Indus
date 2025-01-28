using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("ProjectTechnician")]
    public class ProjectTechnician
    {
        [Key]
        public string Id { get; set; }
        public string TechnitionId { get; set; }
        public string ServiceVehicleId { get; set; }
        public string ProjectId { get; set; }
        public int ProjectTechStatus { get; set; }
        public DateTime AssignDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }

    }
}
