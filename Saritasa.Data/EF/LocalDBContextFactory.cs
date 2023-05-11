using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saritasa.Data.EF
{
    public class LocalDBContextFactory : IDesignTimeDbContextFactory<LocalDBContext>
    {
        public LocalDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("LocalDatabase");

            var optionBuilder = new DbContextOptionsBuilder<LocalDBContext>();
            optionBuilder.UseSqlServer(connectionString);
            optionBuilder.EnableSensitiveDataLogging();

            return new LocalDBContext(optionBuilder.Options);
        }
    }
}
