using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI_Advisior.API.Authorize.Net.Model
{
    public class Message
    {
        public string code { get; set; }
        public string description { get; set; }
    }

    public class Transaction
    {
        public string responseCode { get; set; }
        public string authCode { get; set; }
        public string avsResultCode { get; set; }
        public string cvvResultCode { get; set; }
        public string cavvResultCode { get; set; }
        public string transId { get; set; }
        public string refTransID { get; set; }
        public string transHash { get; set; }
        public string testRequest { get; set; }
        public string accountNumber { get; set; }
        public string accountType { get; set; }
        public IList<Message> messages { get; set; }
        public string transHashSha2 { get; set; }
        public int SupplementalDataQualificationIndicator { get; set; }
    }

    public class ReponnseMessage
    {
        public string code { get; set; }
        public string text { get; set; }
    }

    public class Messages
    {
        public string resultCode { get; set; }
        public IList<ReponnseMessage> message { get; set; }
    }

    public class TransactionResponseModel
    {
        public Transaction transactionResponse { get; set; }
        public Messages messages { get; set; }
    }
}
