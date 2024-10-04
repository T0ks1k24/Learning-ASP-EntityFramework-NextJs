using Entity_Framework.Data;
using Entity_Framework.Models.Dtos.Products;
using Entity_Framework.Models.Entitys;
using Entity_Framework.Repositories.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Entity_Framework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductReposity _productReposity;

        public ProductController(IProductReposity productReposity)
        {
            _productReposity = productReposity;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productReposity.GetAll();
            if(products == null) return NotFound();
            return Ok(products);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _productReposity.GetById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddUpdateProductDto product)
        {
            var createdProduct = await _productReposity.Create(product);
            if(createdProduct == null) return NotFound();
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AddUpdateProductDto productDto)
        {
            var updatedProduct = await _productReposity.Update(id, productDto);
            if (updatedProduct == null) return NotFound();
            return Ok(updatedProduct);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteProduct = await _productReposity.Delete(id);
            if (!deleteProduct) return NotFound();
            return Ok(deleteProduct);
        }
    }
}
