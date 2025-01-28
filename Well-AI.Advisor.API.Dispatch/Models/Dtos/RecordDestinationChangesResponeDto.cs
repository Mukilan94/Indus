using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.Model.ServiceCompany.Models;
namespace WellAI.Advisor.API.Models.Dtos
{
    public class RecordDestinationChangesResponseDto
    {
      
           [Required]
            public string result { get; set; }

            public string message { get; set; }

        
    }
}
