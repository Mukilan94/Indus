using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Common
{
    [Table("TenantConfiguration")]
    public class TenantConfiguration
    {
        [Key]
        public int Index { get; set; }
        [StringLength(256)]
        public string TenantId { get; set; }
        [StringLength(256)]
        public string ConstantName { get; set; }
        [StringLength(256)]
        public string Value { get; set; }
    }

    public class TenantConfigurationModel
    {
        public int Index { get; set; }
        public string TenantId { get; set; }
        public string SamsaraApiKey { get; set; }
        public string PecClientId { get; set; }
        public string PecGrantyType { get; set; }
        public string PecClientSecret { get; set; }
    }

    public class TenantConfigurationSerialized
    {
        public string SamsaraApiKey { get; set; }
        public string PecClientId { get; set; }
        public string PecGrantyType { get; set; }
        public string PecClientSecret { get; set; }
    }
}
