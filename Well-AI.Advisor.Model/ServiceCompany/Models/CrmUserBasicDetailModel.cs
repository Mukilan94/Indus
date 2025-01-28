
using System;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class CrmUserBasicDetailModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public int AccountType { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
