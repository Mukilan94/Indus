using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("crmSharedDocuments")]
    public class CrmSharedDocuments
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
    }
}
