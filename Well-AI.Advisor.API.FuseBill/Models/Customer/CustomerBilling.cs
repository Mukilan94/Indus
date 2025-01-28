using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.FuseBill.Models
{

    public class BillingPeriodConfiguration
    {
        public string type { get; set; }
        public string rule { get; set; }
        public string interval { get; set; }
        public object day { get; set; }
        public object month { get; set; }
    }

    public class CustomerBillingStatementSetting
    {
        public object option { get; set; }
        public object type { get; set; }
        public object interval { get; set; }
        public object day { get; set; }
        public object month { get; set; }
        public object trackedItemDisplay { get; set; }
    }
    public class CustomerBilling
    {
        public int invoiceDay { get; set; }
        public string term { get; set; }
        public string interval { get; set; }
        public object autoCollect { get; set; }
        public string rechargeType { get; set; }
        public object rechargeThresholdAmount { get; set; }
        public object rechargeTargetAmount { get; set; }
        public object statusOnThreshold { get; set; }
        public object autoPostDraftInvoice { get; set; }
        public bool hasPaymentMethod { get; set; }
        public object customerGracePeriod { get; set; }
        public object gracePeriodExtension { get; set; }
        public object standingPoNumber { get; set; }
        public IList<BillingPeriodConfiguration> billingPeriodConfigurations { get; set; }
        public int acquisitionCost { get; set; }
        public object showZeroDollarCharges { get; set; }
        public bool taxExempt { get; set; }
        public object useCustomerBillingAddress { get; set; }
        public object taxExemptCode { get; set; }
        public object avalaraUsageType { get; set; }
        public object vatIdentificationNumber { get; set; }
        public string customerServiceStartOption { get; set; }
        public object rollUpTaxes { get; set; }
        public object rollUpDiscounts { get; set; }
        public object trackedItemDisplay { get; set; }
        public CustomerBillingStatementSetting customerBillingStatementSetting { get; set; }
        public int defaultPaymentMethodId { get; set; }
        public int id { get; set; }
        public string uri { get; set; }
    }
}
