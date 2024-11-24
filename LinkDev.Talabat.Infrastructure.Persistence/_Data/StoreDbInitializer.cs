using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializer;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistence._Common;
using System.Text.Json;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
    internal sealed class StoreDbInitializer(StoreContext _dbContext) : DbInitializer(_dbContext) , IStoreDbIntializer
    {
        public override async Task SeedAsync()
        {
            if (!_dbContext.Brands.Any())
            {

                var brandsData = await File.ReadAllTextAsync($"../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);


                if (brands?.Count > 0)
                {
                    await _dbContext.Set<ProductBrand>().AddRangeAsync(brands);
                    await _dbContext.SaveChangesAsync();
                }


            }

            if (!_dbContext.Categories.Any())
            {

                var CategoriesData = await File.ReadAllTextAsync($"../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/categories.json");
                var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);


                if (Categories?.Count > 0)
                {
                    await _dbContext.Set<ProductCategory>().AddRangeAsync(Categories);
                    await _dbContext.SaveChangesAsync();
                }


            }

            if (!_dbContext.Products.Any())
            {

                var productsData = await File.ReadAllTextAsync($"../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);


                if (products?.Count > 0)
                {
                    await _dbContext.Set<Product>().AddRangeAsync(products);
                    await _dbContext.SaveChangesAsync();
                }


            }
        }
    }
}
