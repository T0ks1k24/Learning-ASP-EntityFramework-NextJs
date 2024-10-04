using Entity_Framework.Models.Dtos.Customers;

namespace Entity_Framework.Repositories.Customers
{
    public interface ICustomerReposity
    {
        Task<List<CustomerDto>> GetAll();
        Task<CustomerDto> GetById(Guid id);
        Task<CustomerDto> Create(AddUpdateCustomerDto customerDto);
        Task<CustomerDto> Update(Guid id, AddUpdateCustomerDto customerDto);
        Task<bool> Delete(Guid id);
    }
}
