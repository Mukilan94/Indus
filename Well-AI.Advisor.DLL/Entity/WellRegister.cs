using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WellAI.Advisor.Model.Tenant.Models;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("Well_Register")]

    public class WellRegister
    {
        public WellRegister()
        {
            CreatedDate = DateTime.Now.Date;
        }
        [Key]
        public string well_id { get; set; }
        public string wellname { get; set; }
        public string welltype_id { get; set; }
        public string Country { get; set; }
        public byte Midland { get; set; }
        public byte Delaware { get; set; }
        public byte Conplete_well_drill { get; set; }
        public byte Batch_drill_casing { get; set; }
        public byte Batch_drill_horizontal { get; set; }
        public byte Casing_string { get; set; }

        public string NumAPI { get; set; }
        public string NumAFE { get; set; }
        public string customer_id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string FieldName { get; set; }
        public byte? WellTypeId { get; set; }

        public string State { get; set; }

        public string County { get; set; }

        public string PadID { get; set; }

        public string RigID { get; set; }

        public string CasingString { get; set; }

        public byte BatchFlag { get; set; }

        public string Basin { get; set; }

        public string BatchDrillingTypeID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ChartColor { get; set; }
        public DateTime? StartDate { get; set; }
        public bool Prediction { get; set; }
        //DWOP     
        public string ChecklistTemplateId { get; set; }
        public string Router_WellId { get; set; }
    }
}

