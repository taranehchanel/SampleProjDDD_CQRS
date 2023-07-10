using System.Linq;

namespace Persistence
{
    /// <summary>
	/// https://docs.microsoft.com/en-us/ef/core/dbcontext-configuration/
	/// </summary>
    public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
    {

        #region Constructor
        public DatabaseContext(Microsoft.EntityFrameworkCore
            .DbContextOptions<DatabaseContext> options) : base(options: options)
        {
            Database.EnsureCreated();
        }
        #endregion /Constructor

        #region Properties

        #region Domain.Aggregates

        public Microsoft.EntityFrameworkCore.DbSet<Domain.Aggregates.Products.Product> Products { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Domain.Aggregates.CategoryProducts.CategoryProduct> CategoryProducts { get; set; }

        #endregion /Domain.Aggregates

        #endregion /Properties

        #region Methods

        #region OnModelCreating()
        protected override void OnModelCreating
            (Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly
                (assembly: typeof(DatabaseContext).Assembly);

            //modelBuilder.Seed();
        }
        #endregion /OnModelCreating()

        #endregion /Methods


        


       
    }
}

