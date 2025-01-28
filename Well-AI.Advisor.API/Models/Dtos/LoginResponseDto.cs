using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.API.Models.Dtos
{
    public class LoginResponseDto
    {
        [Required]
        public string result { get; set; }

        public string message { get; set; }
        public UserProfile userinfo { get;set; }
    }
}
