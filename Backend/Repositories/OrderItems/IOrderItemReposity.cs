using Entity_Framework.Models.Dtos.OrderItems;

namespace Entity_Framework.Repositories.OrderItems
{
    public interface IOrderItemReposity
    {
        Task<List<OrderItemDto>> GetAll();
        Task<OrderItemDto> GetById(Guid id);
        Task<OrderItemDto> Create(AddUpdateOrderItemDto orderItemDto);
        Task<OrderItemDto> Update(Guid id, AddUpdateOrderItemDto orderItemDto);
        Task<bool> Delete(Guid id);
    }
}
