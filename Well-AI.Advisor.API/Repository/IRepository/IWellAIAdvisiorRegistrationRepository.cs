using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

namespace Well_AI.Advisor.API.Repository.IRepository
{
    interface IWellAIAdvisiorRegistrationRepository
    {
        bool IsUniqueUser(string username);
       
    }
}
