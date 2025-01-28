using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("PaymentMethodNew")]
    public class PaymentMethodNew
    {
        [Key]
        [StringLength(50)]
        public string ID { get; set; }
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [StringLength(254)]
        //[Required(ErrorMessage = "Enter The Name")]
        public string Holder { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Card Number must be numeric")]
        [StringLength(16, ErrorMessage = "Card Number Must Be have 16 Digits", MinimumLength = 16), MaxLength(200)] public string Number { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        public bool Default { get; set; }
        public bool Agreement { get; set; }
        [StringLength(40)]
        public string TenantId { get; set; }
    }
}
