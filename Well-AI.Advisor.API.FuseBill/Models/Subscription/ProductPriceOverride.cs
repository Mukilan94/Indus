using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.FuseBill.Models.Subscription
{
  

    public class PriceRanx
    {
        public int min { get; set; }
        public object max { get; set; }
        public int amount { get; set; }
    }

    public class ProductPriceOverride
    {
        public int Id { get; set; }
        public int chargeAmount { get; set; }
        public IList<PriceRanx> priceRanges { get; set; }
        public string pricingModelType { get; set; }
    }
}
