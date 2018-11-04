using Microsoft.EntityFrameworkCore;
using STP.MS.WorkSample.Core.Entities;

namespace STP.MS.WorkSample.Data
{
    public class MsDbContext : DbContext
    {
        public MsDbContext(DbContextOptions<MsDbContext> options)
            : base(options)
        { }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
    }
}
