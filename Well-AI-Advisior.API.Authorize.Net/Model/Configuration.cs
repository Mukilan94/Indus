using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Well_AI_Advisior.API.Authorize.Net.Model
{
    public partial class ConfigurationModel
    {
        public int Index { get; set; }
        public string FriendlyName { get; set; }
        public string ConstantName { get; set; }
        public string Value { get; set; }
    }

    public class ApiHeaderParameter
    {
        [JsonPropertyName("login_id")]
        public string LoginId { get; set; }
        [JsonPropertyName("transaction_key")]
        public string TransactionKey { get; set; }
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}
