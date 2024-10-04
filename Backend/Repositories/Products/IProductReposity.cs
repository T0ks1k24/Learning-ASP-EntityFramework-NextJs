using Entity_Framework.Models.Dtos.Products;
using Entity_Framework.Models.Entitys;

namespace Entity_Framework.Repositories.Products
{
    public interface IProductReposity
    {
        Task<List<ProductDto>> GetAll();
        Task<ProductDto> GetById(Guid id);
        Task<ProductDto> Create(AddUpdateProductDto productDto);
        Task<ProductDto> Update(Guid id, AddUpdateProductDto productDto);
        Task<bool> Delete(Guid id);
    }
}
