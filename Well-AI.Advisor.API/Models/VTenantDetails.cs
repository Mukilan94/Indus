using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.API.Models
{
    
    public class VTenantDetails
    {   
       
        public string tenantid { get; set; }

       
        public string AccountType { get; set; }

      
        public bool isactive { get; set; }

        
        public string connectionstring { get; set; }

    }
}
