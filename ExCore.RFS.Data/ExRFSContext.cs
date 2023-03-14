using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ExCore.RFS.Data
{
    public partial class SystemContext : DbContext, IDataProtectionKeyContext
    {
        public SystemContext(DbContextOptions<SystemContext> options) : base(options)
        {

        }

        public virtual DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=QUYENBV\\QUYENBV;Initial Catalog=ExDatabaseef;Trusted_Connection=true;TrustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public class SystemContextFactory : IDesignTimeDbContextFactory<SystemContext>
        {
            public SystemContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<SystemContext>();
                optionsBuilder.UseSqlServer("Server=QUYENBV\\QUYENBV;Initial Catalog=ExDatabaseef;Trusted_Connection=true;TrustServerCertificate=true;");

                return new SystemContext(optionsBuilder.Options);
            }
        }
    }
}
