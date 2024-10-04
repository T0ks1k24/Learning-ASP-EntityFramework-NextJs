using Entity_Framework.Data;
using Entity_Framework.Models.Dtos.Categories;
using Entity_Framework.Models.Entitys;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework.Repositories.Categories
{
    public class CategoryReposity : ICategoryReposity
    {
        private readonly LearningDbContext _context;

        public CategoryReposity(LearningDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>> GetAll()
        {
            var categories = await _context.Categories
                    .AsNoTracking()
                    .ToListAsync();

            var categoryDtos = categories.Select(category => new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            }).ToList();

            return categoryDtos;
        }

        public async Task<CategoryDto> GetById(Guid id)
        {
            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return null;
            }

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public async Task<CategoryDto> Create(AddUpdateCategoryDto categoryDto)
        {
            var categoryEntity = new CategoryEntity
            {
                Name = categoryDto.Name,
            };

            await _context.Categories.AddAsync(categoryEntity);
            await _context.SaveChangesAsync();

            return new CategoryDto
            {
                Id = categoryEntity.Id,
                Name = categoryEntity.Name,
            };
        }

        public async Task<CategoryDto> Update(Guid id, AddUpdateCategoryDto categoryDto)
        {
            var existingCategory = await _context.Categories
               .AsNoTracking()
               .FirstOrDefaultAsync(c => c.Id == id);

            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.Name = categoryDto.Name;

            await _context.SaveChangesAsync();

            return new CategoryDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name
            };
        }

        public async Task<bool> Delete(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
