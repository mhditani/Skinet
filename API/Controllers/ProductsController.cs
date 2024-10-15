using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext db;

        public ProductsController(StoreContext db)
        {
            this.db = db;
        }





        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetPrducts()
        {
            return await db.Products.ToListAsync();
        }










        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await db.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null) return NotFound();


           return Ok(product);
        }














        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody]Product product)
        {
            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();
            return Ok(product);
        }












        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct([FromRoute]int id, [FromBody]Product product)
        {
            if (product.Id != id || !ProductExists(id)) return BadRequest("Can't update this product");

            db.Entry(product).State = EntityState.Modified;

            await db.SaveChangesAsync(); 

            return NoContent();
            
        }









        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] int id) 
        {
            var product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return NoContent();
        }




        private bool ProductExists(int id)
        {
            return db.Products.Any(x => x.Id == id);
        }
    }
}
