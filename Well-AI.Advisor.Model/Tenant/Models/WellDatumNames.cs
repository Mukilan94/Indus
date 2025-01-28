using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WellDatumNames
    {
        public WellDatumNames()
        {
            WellDatums = new HashSet<WellDatums>();
        }

        [Key]
        public int DatumNameId { get; set; }
        public string Code { get; set; }
        public string NamingSystem { get; set; }
        public string Text { get; set; }

        [InverseProperty("DatumName")]
        public virtual ICollection<WellDatums> WellDatums { get; set; }
    }
}
