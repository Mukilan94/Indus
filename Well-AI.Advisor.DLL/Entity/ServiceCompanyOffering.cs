using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("ServiceCompanyOffering")]
    public class ServiceCompanyOffering
    {
        [Key]
        [StringLength(40)]
        public string Id {get;set;}

        [StringLength(40)]
        public string ServiceTenantId { get; set; }

        public string Offerings { get; set; }// json format list of ids from ServiceOffer table "{id1,id2,id3}"
    }
}
