﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    //[Table("PaymentMethod")]
    //public class PaymentMethod
    //{
    //    [Key]
    //    [StringLength(50)]
    //    public string ID { get; set; }
    //    [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
    //    [StringLength(254)]
    //    //[Required(ErrorMessage = "Enter The Name")]
    //    public string Holder { get; set; }
    //    [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
    //    [StringLength(256)]
    //    //[Required(ErrorMessage = "Enter The Nick name")]
    //    public string Nickname { get; set; }
    //    [Required]
    //    [RegularExpression("^[0-9]*$", ErrorMessage = "Card Number must be numeric")]
    //    [StringLength(16, ErrorMessage = "Card Number Must Be have 16 Digits", MinimumLength = 16),MaxLength(200)] public string Number { get; set; }
    //    public string ExpireMonth { get; set; }
    //    public string ExpireYear { get; set; }
    //    [StringLength(256)]
    //    [Display(Name ="Method")]
    //    public string PayType { get; set; }
    //    [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
    //    [StringLength(256)]
    //    //[Required(ErrorMessage = "Enter The First Name")]
    //    public string FirstName { get; set; }
    //    [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
    //    [StringLength(256)]
    //    //[Required(ErrorMessage = "Enter The Middle Initial")]
    //    public string MiddleInitial { get; set; }
    //    [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
    //    [StringLength(256)]
    //    //[Required(ErrorMessage = "Enter The Last Name")]
    //    public string LastName { get; set; }
    //    [StringLength(500)]
    //    public string Address1 { get; set; }
    //    [StringLength(500)]
    //    public string Address2 { get; set; }
    //    [StringLength(256)]
    //    public string City { get; set; }
    //    [StringLength(256)]
    //    public string State { get; set; }
    //    [StringLength(256)]
    //    public string Country { get; set; }
    //    [StringLength(256)]
    //    public string Zip { get; set; }

    //    public bool Default { get; set; }
    //    public bool Agreement { get; set; }

    //    [StringLength(20)]
    //    public string CardType { get; set; }

    //    [StringLength(50)]
    //    public string AccountNumber { get; set; }
    //    [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
    //    [StringLength(50)]
    //    //[Required(ErrorMessage = "Enter The Account Name")]
    //    public string AccountName { get; set; }

    //    [StringLength(50)]
    //    public string RoutingNumber { get; set; }

    //    [StringLength(50)]
    //    public string CheckNumber { get; set; }
    //}

    //[Table("PaymentType")]
    //public class PaymentTypeModel
    //{
    //    [Key]
    //    [StringLength(50)]
    //    public string ID { get; set; }
    //    [StringLength(254)]
    //    public string Name { get; set; }
    //}

    //[Table("Country")]
    //public class Country
    //{
    //    [Key]
    //    [StringLength(50)]
    //    public string ID { get; set; }
    //    [StringLength(254)]
    //    public string Name { get; set; }
    //}

    //[Table("Creditcardtype")]
    //public class CreditCardModel
    //{
    //    [Key]

    //    public int ID { get; set; }
    //    [StringLength(20)]
    //    public string CardType { get; set; }
    //}


    public class PaymentMethod2
    {
        [Key]
        [StringLength(50)]
        public string ID { get; set; }
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [StringLength(254)]
        //[Required(ErrorMessage = "Enter The Name")]
        public string Holder { get; set; }
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [StringLength(256)]
        //[Required(ErrorMessage = "Enter The Nick name")]
        public string Nickname { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Card Number must be numeric")]
        [StringLength(16, ErrorMessage = "Card Number Must Be have 16 Digits", MinimumLength = 16), MaxLength(200)] public string Number { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        [StringLength(04)]
        public string CVV { get; set; }
        [StringLength(256)]
        [Display(Name = "Method")]
        public string PayType { get; set; }
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [StringLength(256)]
        //[Required(ErrorMessage = "Enter The First Name")]
        public string FirstName { get; set; }
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [StringLength(256)]
        //[Required(ErrorMessage = "Enter The Middle Initial")]
        public string MiddleInitial { get; set; }
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [StringLength(256)]
        //[Required(ErrorMessage = "Enter The Last Name")]
        public string LastName { get; set; }
        [StringLength(500)]
        public string Address1 { get; set; }
        [StringLength(500)]
        public string Address2 { get; set; }
        [StringLength(256)]
        public string City { get; set; }
        [StringLength(256)]
        public string State { get; set; }
        [StringLength(256)]
        public string Country { get; set; }
        [StringLength(256)]
        public string Zip { get; set; }

        public bool Default { get; set; }
        public bool Agreement { get; set; }

        [StringLength(20)]
        public string? CardType { get; set; }

        [StringLength(50)]
        public string AccountNumber { get; set; }
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [StringLength(50)]
        //[Required(ErrorMessage = "Enter The Account Name")]
        public string AccountName { get; set; }

        [StringLength(50)]
        public string RoutingNumber { get; set; }

        [StringLength(50)]
        public string CheckNumber { get; set; }

        [StringLength(40)]
        public string TenantId { get; set; }

        //ach payment
        [StringLength(50)]
        public string BankName { get; set; }

        [StringLength(50)]
        public string TypeofAccount { get; set; }

        [StringLength(50)]
        public string CustomerName { get; set; }

    }

}
