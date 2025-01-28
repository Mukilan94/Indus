using System;
using System.Collections.Generic;
using System.Text;

namespace WellAI.Advisor.Model.Administration
{
    public class SubscriptionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ErrorMessage { get; set; }
    }
}
