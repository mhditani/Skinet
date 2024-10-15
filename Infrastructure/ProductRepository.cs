using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext db;

        public ProductRepository(StoreContext db)
        {
            this.db = db;
        }

        public void AddProduct(Product product)
        {
            db.Products.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            db.Products.Remove(product);
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await db.Products.FindAsync(id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await db.Products.ToListAsync();
        }

        public bool ProductsExists(int id)
        {
            return db.Products.Any(p => p.Id == id);
        }

        public async Task<bool> SaveChangesAsyn()
        {
            return await db.SaveChangesAsync() > 0;
        }

        public void UpdateProduct(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
        }
    }
}
