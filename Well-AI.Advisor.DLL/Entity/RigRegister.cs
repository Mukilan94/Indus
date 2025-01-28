using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("Rig_Register")]
    public class RigRegister
    {
        [Key]
        public string Rig_Id { get; set; }
        public string Rig_Name { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string TenantId { get; set; }
    }

    [Table("Pad_Register")]
    public class PadRegister
    {
        public string Rig_Id { get; set; }
        public string Rig_Name { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string TenantId { get; set; }
    }
}
