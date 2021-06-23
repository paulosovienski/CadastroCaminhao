using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Volvo.Models
{
    public class CaminhaoContext: DbContext
    {
        public CaminhaoContext()
        {

        }
        public CaminhaoContext(DbContextOptions<CaminhaoContext> options)
           : base(options)
        { }



        public DbSet<Caminhao> Caminhao { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("VolvoDatabase"));
            }
        }        
    }
}
