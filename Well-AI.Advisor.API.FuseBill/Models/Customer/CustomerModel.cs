using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.FuseBill.Models
{
    class CustomerModel
    {

        public class SalesTrackingCode
        {
            public String type { get; set; }
            public String code { get; set; }
            public String name { get; set; }
            public int id { get; set; }
            public String uri { get; set; }

        }
        public class CustomerReference
        {
            public String reference1 { get; set; }
            public string reference2 { get; set; }
            public String reference3 { get; set; }
            public SalesTrackingCode salesTrackingCodes { get; set; }
            public int id { get; set; }
            public string uri { get; set; }

        }
        public class CustomerAcquisition
        {
            public string adContent { get; set; }
            public string campaign { get; set; }
            public string keyword { get; set; }
            public string landingPage { get; set; }
            public string medium { get; set; }
            public string source { get; set; }
            public int id { get; set; }
            public string uri { get; set; }

        }
        public class Customer
        {
            public string firstName { get; set; }
            public string middleName { get; set; }
            public string lastName { get; set; }
            public string companyName { get; set; }
            public string suffix { get; set; }
            public string primaryEmail { get; set; }
            public int primaryPhone { get; set; }
            public string secondaryEmail { get; set; }
            public int secondaryPhone { get; set; }
            public string title { get; set; }
            public int reference { get; set; }
            public string status { get; set; }
            public string customerAccountStatus { get; set; }
            public string currency { get; set; }
            public CustomerReference customerReference { get; set; }
            public CustomerAcquisition customerAcquisition { get; set; }
            public int monthlyRecurringRevenue { get; set; }
            public int netMonthlyRecurringRevenue { get; set; }
            public int salesforceId { get; set; }
            public SalesforceAccountType? salesforceAccountType { get; set; }
            public string salesforceSynchStatus { get; set; }
            public int netsuiteId { get; set; }
            public string netsuiteSynchStatus { get; set; }
            public string netsuiteCustomerType { get; set; }
            public string portalUserName { get; set; }
            public int parentId { get; set; }
            public QuickBooksLatchType quickBooksLatchType { get; set; }
            public int quickBooksId { get; set; }
            public int quickBooksSyncToken { get; set; }
            public int  hubSpotId { get; set; }
            public int hubSpotCompanyId { get; set; }
            public DateTime modifiedTimestamp { get; set; }
            public int id { get; set; }
            public string uri { get; set; }

        }
    }

    

   public enum SalesforceAccountType { Company,Person}

   public enum QuickBooksLatchType{CreateNew, LatchExisting, DoNothing}

    public enum Title { Mr, Mrs, Ms, Miss, Dr }

    public enum Status{Draft, Active, Hold, Suspended, Cancelled}

    public enum CustomerAccountStatus { Good, Collection, PoorStanding}


    


}
