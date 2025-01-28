using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WellAI.Advisor.Model.OperatingCompany.Models;


namespace WellAI.Advisor.Model.Common
{
    public class Registration
    {
        
        public SubscriptionDetails SubscriptionDetails { get; set; }
        public CompanyDetails CompanyDetails { get; set; }
        public PaymentDetails PaymentDetails{ get; set; }
         
    }
    public class UpdateSubscriptionViewModel
    {
        public UpdateSubscriptionDetails updateSubscriptionDetails { get; set; }
        public UpdateCompanyDetails updateCompanyDetails { get; set; }
        public UpdatePaymentDetails updatePaymentDetails { get; set; }

    }
    public class UpdateSubscription1
    {
        public SubscriptionDetails SubscriptionDetails { get; set; }
        public CompanyDetails CompanyDetails { get; set; }
        public PaymentDetails PaymentDetails { get; set; }

    }
    public class UpdatePaymentDetails
    {
        public string? PaymentMethodId { get; set; }
        //public string PaymentType { get; set; }
        //public string PayType { get; set; }
        //public string CardName { get; set; }
        //public string CardNumber { get; set; }
        //public string Month { get; set; }
        //public string Year { get; set; }
        //public string CardVerificationNumber { get; set; }


        ////ach payment
        //public string BankName { get; set; }
        //public string TypeofAccount { get; set; }
        //public string RoutingNumber { get; set; }
        //public string CustomerName { get; set; }
        //public string AccountNumber { get; set; }

    }
    public class UpdateCompanyDetails
    {
       
        public string Name { get; set; }
        public string CName { get; set; }       
        public string Title { get; set; }      
        public string CompanyPhone { get; set; }       
        public string CompanyEmail { get; set; }    
        public string CompanyAddress1 { get; set; }
        public string CompanyAddress2 { get; set; }
       public string CompanyCity { get; set; }     
        public string CompanyState { get; set; }
        public string CompanyZip { get; set; }     
        public string BillingName { get; set; }
        public string BillingPhone { get; set; }
        public string BillingEmail { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
    }
    public class UpdateSubscriptionDetails
    {

        public double DispatchUnitPrice { get; set; }
        public string DispatchName { get; set; }
        public string DispatchType { get; set; }
        public string DispatchQuantity { get; set; }
        public string DispatchDescription { get; set; }
        public double DispatchTotal { get; set; }
        public double ProviderUnitPrice { get; set; }
        public string ProviderName { get; set; }
        public string ProviderType { get; set; }
        public string ProviderQuantity { get; set; }
        public string ProviderDescription { get; set; }
        public double ProviderTotal { get; set; }
        public double OperatorUnitPrice { get; set; }
        public string OperatorName { get; set; }
        public string OperatorType { get; set; }
        public string OperatorQuantity { get; set; }
        public string OperatorDescription { get; set; }
        public double OperatorTotal { get; set; }
        public double TotalValue { get; set; }
    }
    public class SubscriptionAndPaymentDetails
    {

        public SubscriptionViewModel subscriptionViewModel { get; set; }
        public SubscriptionDetails SubscriptionDetails { get; set; }
        public CompanyDetails CompanyDetails { get; set; }
        public PaymentDetails PaymentDetails { get; set; }

    }
    public class UpdateSubscriptionAndPaymentDetails
    {

        public SubscriptionViewModel subscriptionViewModel { get; set; }
        public UpdateSubscriptionDetails updateSubscriptionDetails { get; set; }
        public UpdateCompanyDetails updateCompanyDetails { get; set; }
        public UpdatePaymentDetails updatePaymentDetails { get; set; }

    }

    public class SubscriptionDetails
    {
        public double DispatchUnitPrice { get; set; }
        public string  DispatchName { get; set; }
        public string DispatchType { get; set; }
        public string DispatchQuantity { get; set; }
        public string DispatchDescription { get; set; }
        public double DispatchTotal { get; set; }
        public double ProviderUnitPrice { get; set; }
        public string ProviderName { get; set; }
        public string ProviderType { get; set; }
        public string ProviderQuantity { get; set; }
        public string ProviderDescription { get; set; }
        public double ProviderTotal { get; set; }
        public double OperatorUnitPrice { get; set; }
        public string OperatorName { get; set; }
        public string OperatorType { get; set; }
        public string OperatorQuantity { get; set; }
        public string OperatorDescription { get; set; }
        public double OperatorTotal { get; set; }
        public double TotalValue { get; set; }

    }
    
    public class CompanyDetails
    {
        [StringLength(500)]
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(500)]
        public string CName { get; set; }
        [Required]
        [StringLength(500)]

        public string Title { get; set; }
        [Required]
        [StringLength(50)]

        public string CompanyPhone { get; set; }
        [StringLength(500)]
        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string CompanyEmail { get; set; }
        [Required]
        [StringLength(500)]

        public string CompanyAddress1 { get; set; }
        [Required]
        [StringLength(500)]

        public string CompanyAddress2 { get; set; }
        [Required]
        [StringLength(254)]

        public string CompanyCity { get; set; }
        [Required]
        [StringLength(50)]

        public string CompanyState { get; set; }
        [Required]
        [StringLength(6)]

        public string CompanyZip { get; set; }

        [StringLength(500)]
        public string BillingName { get; set; }

        [StringLength(50)]

        public string BillingPhone { get; set; }

        [StringLength(500)]
        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string BillingEmail { get; set; }


        [StringLength(500)]

        public string BillingAddress1 { get; set; }

        [StringLength(500)]

        public string BillingAddress2 { get; set; }

        [StringLength(254)]

        public string BillingCity { get; set; }

        [StringLength(50)]

        public string BillingState { get; set; }

        [StringLength(6)]

        public string BillingZip { get; set; }

    }
    public class PaymentDetails
    {
        [StringLength(50)]
        public string PaymentType { get; set; }
       
        [StringLength(5)]
        public string PayType { get; set; }

        [StringLength(50)]
        public string CardName { get; set; }
        [MinLength(16)]
       
        [MaxLength(16)]
        public string CardNumber { get; set; }

        [MaxLength(02)]
        public string Month { get; set; }

        [StringLength(02)]
        public string Year { get; set; }

        [StringLength(03)]
        public string CardVerificationNumber { get; set; }


        //ach payment
        [StringLength(50)]
        public string BankName { get; set; }

        [StringLength(50)]
        public string TypeofAccount { get; set; }

        [MinLength(9)]
        [MaxLength(9)]
        public string RoutingNumber { get; set; }

        [StringLength(50)]
        public string CustomerName { get; set; }

        [MinLength(16)]
        [MaxLength(16)]
        public string AccountNumber { get; set; }

    }
}

