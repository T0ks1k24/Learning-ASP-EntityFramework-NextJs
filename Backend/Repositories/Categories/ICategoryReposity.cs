using Entity_Framework.Models.Dtos.Categories;
using Entity_Framework.Models.Entitys;

namespace Entity_Framework.Repositories.Categories
{
    public interface ICategoryReposity
    {
        Task<List<CategoryDto>> GetAll();
        Task<CategoryDto> GetById(Guid id);
        Task<CategoryDto> Create(AddUpdateCategoryDto categoryDto);
        Task<CategoryDto> Update(Guid id, AddUpdateCategoryDto categoryDto);
        Task<bool> Delete(Guid id);
    }
}
