using System;
using System.Collections.Generic;
using System.Text;
using Well_AI_Advisior.API.Authorize.Net.Helpers;

namespace Well_AI_Advisior.API.Authorize.Net.Model
{
    public class BankAccountType
    {
        public int AccountType { get; set; }
       
        public bool AccountTypeSpecified { get; set; }
        public string RoutingNumber { get; set; }
        public string AccountNumber { get; set; }
        public string NameOnAccount { get; set; }
        public string EcheckType { get; set; }

        public bool EcheckTypeSpecified { get; set; }
        public string BankName { get; set; }
        public string CheckNumber { get; set; }
    }
}
