using Core.Entities;
using Core.Interfaces;
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
        private readonly IProductRepository repo;

        public ProductsController(IProductRepository repo)
        {
            this.repo = repo;
        }





        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetPrducts()
        {
            return Ok(await repo.GetProductsAsync());
        }










        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetProductByIdAsync(id);

            if (product == null) return NotFound();


           return Ok(product);
        }














        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody]Product product)
        {
            repo.AddProduct(product);

            if (await repo.SaveChangesAsyn())
            {
                return CreatedAtAction("GetProduct", new {id = product.Id}, product);
            }
            
            return BadRequest("Problem with creating product");
        }












        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct([FromRoute]int id, [FromBody]Product product)
        {
            if (product.Id != id || !ProductExists(id)) return BadRequest("Can't update this product");

            repo.UpdateProduct(product);

            if (await repo.SaveChangesAsyn())
            {
                return NoContent();
            }


            return BadRequest("Problem Updating the product");
            
        }









        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] int id) 
        {
            var product = await repo.GetProductByIdAsync(id);
            if (product == null) return NotFound();

           repo.DeleteProduct(product);
            if (await repo.SaveChangesAsyn())
            {
                return NoContent();
            }


            return BadRequest("Problem deleting the product");
        }




        private bool ProductExists(int id)
        {
            return repo.ProductsExists(id);
        }
    }
}
