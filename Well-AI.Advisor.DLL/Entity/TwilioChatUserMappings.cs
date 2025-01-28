using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("twiliochatusermappings")]
    public class TwilioChatUserMappings
    {
        [Key]
        public string channelsid{ get; set; }

        public string sendername { get; set; }

        public string receivername { get; set; }

        public string channeluniquename { get; set; }
        public string useridentity { get; set; }

        public byte invitationstatus { get; set; }

    }
}
