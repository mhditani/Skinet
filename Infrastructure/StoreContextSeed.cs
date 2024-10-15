using Core.Entities;
using Infrastructure.Data;
using System.Text.Json;

namespace Infrastructure
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, string seedDataFilePath)
        {
            if (!context.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync(seedDataFilePath);
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products == null) return;

                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }
        }
    }
}