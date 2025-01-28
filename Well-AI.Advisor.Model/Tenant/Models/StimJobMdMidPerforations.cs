using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobMdMidPerforations
    {
        public StimJobMdMidPerforations()
        {
            StimJobPdatSessions = new HashSet<StimJobPdatSessions>();
        }

        [Key]
        public int MdMidPerforationId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdMidPerforation")]
        public virtual ICollection<StimJobPdatSessions> StimJobPdatSessions { get; set; }
    }
}
