using CoreGateway.DBMap.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;

namespace CoreGateway.DBMap
{
    public class CoreGatewayDbContext : IdentityDbContext<IdentityUser>
    {
        #region Models

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Transactions> Transactions { get; set; }

        #endregion Models

        #region config

        private static string connectionString;

        public CoreGatewayDbContext()
        {
        }

        public CoreGatewayDbContext(DbContextOptions<CoreGatewayDbContext> options)
        : base(options)
        {
            var extension = options.FindExtension<SqlServerOptionsExtension>();
            connectionString = extension.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        #endregion config
    }
}