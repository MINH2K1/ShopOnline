using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Data.Data_Context
{
    public class ShopOnline_Config_DBContext_Migration : IDesignTimeDbContextFactory<ShopOnline_Context>
    {
        public ShopOnline_Context CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("Settings.json")
                 .Build();

            var connectionString = configuration.GetConnectionString("ShopOnline");

            var optionsBuilder = new DbContextOptionsBuilder<ShopOnline_Context>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ShopOnline_Context(optionsBuilder.Options);
        }
    }
}
