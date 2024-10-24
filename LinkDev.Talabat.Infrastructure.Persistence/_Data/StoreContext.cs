using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistence._Common;
using System.Reflection;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
    public class StoreContext : DbContext 
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
             
        }

        public DbSet<Product> Products { get; set; } 
        public DbSet<ProductBrand> Brands { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly , 
                type=>type.GetCustomAttribute<DbContextTypeAttribute>()?.DbContextType==typeof(StoreContext));
        }


    }
}
