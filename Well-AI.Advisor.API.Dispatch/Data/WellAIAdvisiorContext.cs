using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dispatch.Models;

namespace Well_AI.Advisor.API.Dispatch.Data
{
    public class WellAIAdvisiorContext : DbContext
    {

        public WellAIAdvisiorContext(DbContextOptions<WellAIAdvisiorContext> options) : base(options)
        {

        }
                           
        public DbSet<WellSubscription> WorkstationRegister { get; set; }

        public DbSet<Configuration> Configuration { get; set; }




    }



}
