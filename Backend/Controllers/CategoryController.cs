using Entity_Framework.Data;
using Entity_Framework.Models.Dtos.Categories;
using Entity_Framework.Models.Entitys;
using Entity_Framework.Repositories.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryReposity _categoryReposity;

        public CategoryController(ICategoryReposity categoryReposity)
        {
            _categoryReposity = categoryReposity;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _categoryReposity.GetAll();
            if (orders == null) return NotFound();
            return Ok(orders);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _categoryReposity.GetById(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddUpdateCategoryDto category)
        {
            var createCategory = await _categoryReposity.Create(category);
            if (createCategory == null) return NotFound();
            return Ok(createCategory);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, AddUpdateCategoryDto category)
        {
            var updateCategory = await _categoryReposity.Update(id, category);
            if (updateCategory == null) return NotFound();
            return Ok(updateCategory);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteCategory = await _categoryReposity.Delete(id);
            if (!deleteCategory) return NotFound();
            return Ok(deleteCategory); 
        }
    }
}
