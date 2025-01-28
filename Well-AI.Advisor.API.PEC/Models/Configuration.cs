using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Well_AI.Advisor.API.PEC.Models
{
    [Table("TenantConfiguration")]
    public class TenantConfiguration
    {
        [Key]
        public string TenantId { get; set; }
        public string ConstantName { get; set; }
        public string value { get; set; }
  }

    [Table("AspNetUsers")]
    public class userDetailConfiguration
    {
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string TenantId { get; set; }
    }

    [Table("CrmCompanies")]
    public class DbCrmCompanies
    {
        [Key]
        public string userId { get; set; }
        public string TenantId { get; set; }
    }

    public class Parameter
    {
        [Key]
        public string TenantId { get; set; }
        public string ConstantName { get; set; }
        
        public string Loginparameter { get; set; }
        public string Bodyparameter { get; set; }
    }

}
