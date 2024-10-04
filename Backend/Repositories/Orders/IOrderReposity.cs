using Entity_Framework.Models.Dtos.Orders;
using Entity_Framework.Models.Entitys;

namespace Entity_Framework.Repositories.Orders
{
    public interface IOrderReposity
    {
        Task<List<OrderDto>> GetAll();
        Task<OrderDto> GetById(Guid id);
        Task<OrderDto> Create(AddUpdateOrderDto orderDto);
        Task<OrderDto> Update(Guid id, AddUpdateOrderDto orderDto);
        Task<bool> Delete(Guid id);
    }
}
