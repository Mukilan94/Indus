using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.PEC.Models
{
   public class Authonticate
    {
        [JsonProperty("SamsaraApiKey")]
        public string SamsaraApiKey { get; set; }
        [JsonProperty("PecGrantyType")]
        public string PecGrantyType { get; set; }
        [JsonProperty("PecClientId")]
        public string PecClientId { get; set; }
        [JsonProperty("PecClientSecret")]
        public string PecClientSecret { get; set; }
             
    }

    public class UserDetail
    {
        [JsonProperty("UserName")]
        public string UserName { get; set; }
        [JsonProperty("PasswordHash")]
        public string PasswordHash { get; set; }
    }

    public class AuthonticateJsonModel
    {
        public Authonticate BodyParameter { get; set; }
        public UserDetail UserDetails { get; set; }
    }
    
    public class AuthonticationResponse
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }
        public string ErrorMessage { get; set; }
       

    }
}
