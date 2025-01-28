using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Well_AI.Helpdesk.Models.ManageViewModels
{
    public class CorporateProfile
{
        [Key]
        [Column("TenantId")]
        public string customerId { get; set; }
        [Display(Name = "Customer")]
        [StringLength(100)]
        [Required]
        public string name { get; set; }
        [Display(Name = "Full Address")]
        [StringLength(100)]
        public string address1 { get; set; }
        public string address2 { get; set; }
        [Display(Name = "Phone")]
        [StringLength(20)]
        public string phone { get; set; }
        [Display(Name = "City")]
        [StringLength(100)]
        public string city { get; set; }
        [Display(Name = "State")]
        [StringLength(100)]
        public string state { get; set; }
        [Display(Name = "Zip")]
        [StringLength(100)]
        public string zip { get; set; }
        [Display(Name = "Country")]
        [StringLength(100)]
        public string country { get; set; }
        [Display(Name = "Website")]
        [StringLength(100)]
        public string website { get; set; }

        public string UserId { get; set; }
    }
}
