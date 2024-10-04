using Entity_Framework.Data;
using Entity_Framework.Models.Dtos.Categories;
using Entity_Framework.Models.Dtos.Products;
using Entity_Framework.Models.Entitys;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Entity_Framework.Repositories.Products
{
    public class ProductReposity : IProductReposity
    {
        private readonly LearningDbContext _context;

        public ProductReposity(LearningDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> GetAll()
        {
            return await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Select(p => new ProductDto {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    Category = new CategoryDto 
                    {
                        Id = p.Category.Id,
                        Name = p.Category.Name
                    }

                })
                .ToListAsync();
        }

        public async Task<ProductDto> GetById(Guid id)
        {
            var product = await _context.Products
                .AsNoTracking()
                .Include(p => p.Category) 
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return null;
            }

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Category = new CategoryDto
                {
                    Id = product.Category.Id,
                    Name = product.Category.Name
                }
            };
        }

        public async Task<ProductDto> Create(AddUpdateProductDto productDto)
        {
            var productEntity = new ProductEntity
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Stock = productDto.Stock,
                CategoryId = productDto.CategoryId
            };

            await _context.Products.AddAsync(productEntity);
            await _context.SaveChangesAsync();

            return new ProductDto
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Description = productEntity.Description,
                Price = productEntity.Price,
                Stock = productEntity.Stock
            };
        }

        public async Task<ProductDto> Update(Guid id, AddUpdateProductDto productDto)
        {
            var existingProduct = await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = productDto.Name;
            existingProduct.Description = productDto.Description;
            existingProduct.Price = productDto.Price;
            existingProduct.Stock = productDto.Stock;
            existingProduct.CategoryId = productDto.CategoryId;

            await _context.SaveChangesAsync();

            return new ProductDto
            {
                Id = existingProduct.Id,
                Name = existingProduct.Name,
                Description = existingProduct.Description,
                Price = existingProduct.Price,
                Stock = existingProduct.Stock,
                Category = new CategoryDto
                {
                    Id = existingProduct.Category.Id,
                    Name = existingProduct.Category.Name
                }
            };
        }

        public async Task<bool> Delete(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }


    }
}
