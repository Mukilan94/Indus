using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI_Advisior.API.Authorize.Net.Model
{
   public class CreditCardType
    {
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CardCode { get; set; }
        public string Cryptogram { get; set; }
        public bool IsPaymentToken { get; set; }
        public bool IsPaymentTokenSpecified { get; set; }
        public string TokenRequestorEci { get; set; }
        public string TokenRequestorId { get; set; }
        public string TokenRequestorName { get; set; }
    }
}
