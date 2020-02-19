using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bagcash
{
    public class IdentityContext : IdentityDbContext
    {
        public IConfiguration Configuration { get; }

        public IdentityContext(IConfiguration configuration, DbContextOptions<IdentityContext> options) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("BagcashContext"));
        }
    }
}
