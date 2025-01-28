using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI_Advisior.API.Authorize.Net.Model
{

   

    public class CreditCard
    {
        public string cardNumber { get; set; }
        public string expirationDate { get; set; }
        public string cardCode { get; set; }
    }

    public class Payment
    {
        public CreditCard creditCard { get; set; }
    }

    public class LineItem
    {
        public string itemId {get ; set; }
      
        public bool alternateTaxRateSpecified {get ; set; }
        public decimal alternateTaxAmount {get ; set; }
      
        public bool alternateTaxAmountSpecified {get ; set; }
        public decimal totalAmount {get ; set; }
      
        public bool totalAmountSpecified {get ; set; }
        public string commodityCode {get ; set; }
        public string productCode {get ; set; }
        public decimal alternateTaxRate {get ; set; }
        public string productSKU {get ; set; }
      
        public bool discountRateSpecified {get ; set; }
        public decimal discountAmount {get ; set; }
      
        public bool discountAmountSpecified {get ; set; }
        public bool taxIncludedInTotal {get ; set; }
      
        public bool taxIncludedInTotalSpecified {get ; set; }
        public bool taxIsAfterDiscount {get ; set; }
      
        public bool taxIsAfterDiscountSpecified {get ; set; }
        public decimal discountRate {get ; set; }
        public string alternateTaxTypeApplied {get ; set; }
        public string alternateTaxType {get ; set; }
        public decimal taxRate {get ; set; }
        public decimal quantity {get ; set; }
        public decimal unitPrice {get ; set; }
        public bool taxable {get ; set; }
      
        public bool taxableSpecified {get ; set; }
        public string unitOfMeasure {get ; set; }
        public string typeOfSupply {get ; set; }
        public string alternateTaxId {get ; set; }
        public string description {get ; set; }
      
        public bool taxRateSpecified {get ; set; }
      
        public bool taxAmountSpecified {get ; set; }
        public decimal nationalTax {get ; set; }
      
        public bool nationalTaxSpecified {get ; set; }
        public decimal localTax {get ; set; }
      
        public bool localTaxSpecified {get ; set; }
        public decimal vatRate {get ; set; }
      
        public bool vatRateSpecified {get ; set; }
        public decimal taxAmount {get ; set; }
        public string name {get ; set; }
    }

    public class LineItems
    {
        public LineItem lineItem { get; set; }
    }

    public class Tax
    {
        public string amount { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class Duty
    {
        public string amount { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class Shipping
    {
        public string amount { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class Customer
    {
        public string id { get; set; }
    }

    public class CustomerAddressType
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
    }

    public class ShipTo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
    }

    public class Setting
    {
        public string settingName { get; set; }
        public string settingValue { get; set; }
    }

    public class TransactionSettings
    {
        public Setting setting { get; set; }
    }

    public class UserField
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class UserFields
    {
        public IList<UserField> userField { get; set; }
    }

    public class TransactionRequest
    {
        public string transactionType { get; set; }
        public decimal amount { get; set; }
        public Payment payment { get; set; }
        public LineItem[] lineItems { get; set; }
        public Tax tax { get; set; }
        public Duty duty { get; set; }
        public Shipping shipping { get; set; }
        public string poNumber { get; set; }
        public Customer customer { get; set; }
        public CustomerAddressType billTo { get; set; }
        public ShipTo shipTo { get; set; }
        public string customerIP { get; set; }
        public TransactionSettings transactionSettings { get; set; }
        public UserFields userFields { get; set; }
    }

    public class CreateTransactionRequest
    {
        public MerchantAuthentication merchantAuthentication { get; set; }
        public string refId { get; set; }
        public TransactionRequest transactionRequest { get; set; }
    }

    public class TransactionRequestModel
    {
        public CreateTransactionRequest createTransactionRequest { get; set; }
    }

}
