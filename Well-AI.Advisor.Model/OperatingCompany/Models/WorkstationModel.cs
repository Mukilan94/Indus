using System;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{

    
    public class WorkstationModel
    {
       
        public System.Guid RegisterationId { get; set; }
       
        public string CustomerAccountIdentifier { get; set; }
       [Required]
        public string DeviceName { get; set; }
        [Required]
        public string WorkstationIdentifier { get; set; }
        public string WorkstationToken { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [Required]
        public string CustomerName { get; set; }
    }
}
