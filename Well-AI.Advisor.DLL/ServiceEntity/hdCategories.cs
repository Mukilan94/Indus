using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.DLL.ServiceEntity
{
    [Table("hdCategories")]
    public class hdCategories
    {
        [Key]
        [Required]
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public int SectionID { get; set; }
        public int InstanceID { get; set; }
        public Boolean ForTechsOnly { get; set; }
        public Boolean ForSpecificUsers { get; set; }
        public int OrderByNumber { get; set; }
        public string FromAddress { get; set; }
        public Boolean KBOnly { get; set; }
        public Boolean FromAddressInReplyTo { get; set; }
        public string Notes { get; set; }
        public string FromName { get; set; }    
        
    }
}
