using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace STP.MS.WorkSample.Data
{
    public class DbContextFactory : IDesignTimeDbContextFactory<MsDbContext>
    {
        public MsDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), "STP.MS.WorkSample.Services"))
                .AddJsonFile("appsettings.json")
                .Build();

            var dbContextBuilder = new DbContextOptionsBuilder<MsDbContext>();

            var connectionString = configuration.GetConnectionString("DbContextConnection");

            dbContextBuilder.UseSqlServer(connectionString);

            return new MsDbContext(dbContextBuilder.Options);
        }
    }
}
