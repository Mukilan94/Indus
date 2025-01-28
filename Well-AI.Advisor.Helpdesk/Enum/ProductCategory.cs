using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Enum
{
    public enum ProductCategory
    {
        Billng = 0,
        General = 1,
        [Display(Name = "Technical Support")]
        TechnicalSupport = 2,
        //Desktop = 3,
        //Laptop = 4,
        //Printer = 5,
        //[Display(Name = "Other Hardware")]
        //OtherHardware = 6,
        //Windows = 7,
        //Word = 8,
        //Excel = 9,
        //Powerpoint = 10,
        //[Display(Name = "Other Software")]
        //OtherSoftware = 11
    }
}
