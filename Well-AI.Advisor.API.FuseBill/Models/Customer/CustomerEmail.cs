using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.FuseBill.Models.Customer
{
    public class CustomerEmail
    {
        public int customerId { get; set; }
        public List<Preferences> preferences { get; set; }
    }

    public class Preferences
    {
        public string enabled { get; set; }
        public Boolean accountDefault { get; set; }
        public EmailType emailType { get; set; }
        public string emailCategory { get; set; }

    }

    public enum EmailType { InvoicePost, PaymentReceived, InvoiceOverdue, PasswordReset, PaymentFailed, CustomerActivation, SubscriptionActivation, SubscriptionCancellation, CustomerCredentialCreate, CustomerCredentialPasswordReset, CustomerSuspend, PaymentMethodUpdate, CreditCardExpiry, StatementNotification, UpcomingBillingNotification, Refund, PendingExpiryRenewalNotice }
}
