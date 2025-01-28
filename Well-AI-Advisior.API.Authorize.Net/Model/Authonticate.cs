using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI_Advisior.API.Authorize.Net.Model
{

    public class Authonticate
    {
        [JsonProperty("login_id")]
        public string LoginId { get; set; }
        [JsonProperty("transaction_key")]
        public string TransactionKey { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }

    }

    public class AuthonticateJsonModel
    {
        public Authonticate BodyParameter { get; set; }


    }

    public class SubscriptionResponse
    {
        public string Message { get; set; }
        public string SubscriptionId { get; set; }

    }
}

