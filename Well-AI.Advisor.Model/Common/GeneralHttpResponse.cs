using System;
using System.Collections.Generic;
using System.Text;

namespace WellAI.Advisor.Model.Common
{
    public class GeneralHttpResponse
    {
       
        public int resultCode { get; set; }
        
        public string resultMessage { get; set; }
       
        public string messageContent { get; set; }
    }
}
