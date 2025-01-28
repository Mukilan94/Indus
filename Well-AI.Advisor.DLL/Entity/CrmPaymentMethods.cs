using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("crmPaymentMethods")]
    public class CrmPaymentMethods
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CustomerName { get; set; }
        public string CreditCardNumber { get; set; }
        public string ValidUptoDate { get; set; }
    }
}
