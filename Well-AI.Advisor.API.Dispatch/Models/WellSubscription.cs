using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Well_AI.Advisor.API.Dispatch.Models
{
    public class WellSubscription
    {
        [Key]
        [JsonIgnore]
        public System.Guid RegisterationId { get; set; }
        public string CustomerAccountIdentifier { get; set; }
        public string WorkstationIdentifier { get; set; }
        public string WorkstationToken { get; set; }
        [JsonIgnore]
        public bool IsActive { get; set; }

        [NotMapped]
        public string ApiAccessKey { get; set; }
        [NotMapped]
        public string AuthorizationToken { get; set; }
    }
}
