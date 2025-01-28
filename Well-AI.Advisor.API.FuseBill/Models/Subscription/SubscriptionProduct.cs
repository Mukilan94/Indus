using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.FuseBill.Models.Subscription
{

    public class ChargeModel
    {
        public string chargeModelType { get; set; }
        public string chargeTimingType { get; set; }
        public string prorationGranularity { get; set; }
        public bool prorateOnPositiveQuantity { get; set; }
        public bool prorateOnNegativeQuantity { get; set; }
        public bool reverseChargeOnNegativeQuantity { get; set; }
        public int id { get; set; }
        public object uri { get; set; }
    }

    public class Price
    {
        public double amount { get; set; }
        public string currency { get; set; }
        public int id { get; set; }
        public object uri { get; set; }
    }

    public class QuantityRanx
    {
        public double min { get; set; }
        public object max { get; set; }
        public IList<Price> prices { get; set; }
        public int id { get; set; }
        public object uri { get; set; }
    }

    public class PricingModel
    {
        public string pricingModelType { get; set; }
        public IList<QuantityRanx> quantityRanges { get; set; }
        public int id { get; set; }
        public object uri { get; set; }
    }

    public class OrderToCashCycle
    {
        public int planFrequencyId { get; set; }
        public int planProductId { get; set; }
        public int numberOfIntervals { get; set; }
        public string interval { get; set; }
        public IList<ChargeModel> chargeModels { get; set; }
        public object remainingInterval { get; set; }
        public bool groupQuantityChangeCharges { get; set; }
        public object planProductPriceUplifts { get; set; }
        public string earningInterval { get; set; }
        public int earningNumberOfIntervals { get; set; }
        public string earningTimingInterval { get; set; }
        public string earningTimingType { get; set; }
        public PricingModel pricingModel { get; set; }
        public int id { get; set; }
        public object uri { get; set; }
    }

    public class PlanProduct
    {
        public string status { get; set; }
        public int productId { get; set; }
        public int planId { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public string productStatus { get; set; }
        public object productDescription { get; set; }
        public string productType { get; set; }
        public string productGLCode { get; set; }
        public double quantity { get; set; }
        public object maxQuantity { get; set; }
        public bool isRecurring { get; set; }
        public bool isFixed { get; set; }
        public bool isOptional { get; set; }
        public bool isIncludedByDefault { get; set; }
        public bool isTrackingItems { get; set; }
        public bool chargeAtSubscriptionActivation { get; set; }
        public IList<OrderToCashCycle> orderToCashCycles { get; set; }
        public string resetType { get; set; }
        public int planProductUniqueId { get; set; }
        public bool generateZeroDollarCharge { get; set; }
        public int id { get; set; }
        public string uri { get; set; }
    }

    public class EarningSettings
    {
        public string earningTimingInterval { get; set; }
        public string earningTimingType { get; set; }
    }
    public class SubscriptionProduct
    {
        public int subscriptionId { get; set; }
        public PlanProduct planProduct { get; set; }
        public double quantity { get; set; }
        public bool isIncluded { get; set; }
        public object startDate { get; set; }
        public object subscriptionProductOverride { get; set; }
        public object subscriptionProductPriceOverride { get; set; }
        public bool chargeAtSubscriptionActivation { get; set; }
        public bool isCharged { get; set; }
        public object subscriptionProductDiscount { get; set; }
        public IList<object> subscriptionProductDiscounts { get; set; }
        public object customFields { get; set; }
        public double monthlyRecurringRevenue { get; set; }
        public double netMonthlyRecurringRevenue { get; set; }
        public double amount { get; set; }
        public string status { get; set; }
        public DateTime lastPurchaseDate { get; set; }
        public EarningSettings earningSettings { get; set; }
        public object remainingInterval { get; set; }
        public bool groupQuantityChangeCharges { get; set; }
        public bool priceUpliftsEnabled { get; set; }
        public IList<object> priceUplifts { get; set; }
        public IList<object> historicalPriceUplifts { get; set; }
        public bool generateZeroDollarCharge { get; set; }
        public int id { get; set; }
        public string uri { get; set; }
    }

}
