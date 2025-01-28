using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Data;
using WellAI.Advisor.API.Models;

namespace WellAI.Advisor.API.Repository.IRepository
{
  public interface ITenantRepository
    {
        DbContextOptions<WellAIAdvisiorContext> SetDbContext(string tenantId);
    }
}
