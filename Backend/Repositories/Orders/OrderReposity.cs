using Entity_Framework.Data;
using Entity_Framework.Models.Dtos.Customers;
using Entity_Framework.Models.Dtos.Orders;
using Entity_Framework.Models.Entitys;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework.Repositories.Orders
{
    public class OrderReposity : IOrderReposity
    {
        private readonly LearningDbContext _context;

        public OrderReposity(LearningDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDto>> GetAll()
        {
            var orders = await _context.Orders
                .AsNoTracking()
                .ToListAsync();

            var ordersDto = orders.Select(orders => new OrderDto
            {
                Id = orders.Id,
                OrderDate = orders.OrderDate,
                CustomerId = orders.CustomerId,
            }).ToList();

            return ordersDto;
        }

        public async Task<OrderDto> GetById(Guid id)
        {
            var order = await _context.Orders
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);

            if(order == null)
            {
                return null;
            }

            return new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                CustomerId = order.CustomerId,
            };
        }

        public async Task<OrderDto> Create(AddUpdateOrderDto orderDto)
        {
            var orderEntity = new OrderEntity
            {
                OrderDate = orderDto.OrderDate,
                CustomerId = orderDto.CustomerId
            };

            _context.Orders.Add(orderEntity);
            await _context.SaveChangesAsync();

            return new OrderDto
            {
                Id = orderEntity.Id,
                OrderDate = orderDto.OrderDate,
                CustomerId = orderDto.CustomerId
            };
        }

        public async Task<OrderDto> Update(Guid id, AddUpdateOrderDto orderDto)
        {
            var existingOrder = await _context.Orders
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);

            if (existingOrder == null)
            {
                return null;
            }

            existingOrder.OrderDate = orderDto.OrderDate;
            existingOrder.CustomerId = orderDto.CustomerId;

            _context.Orders.Update(existingOrder);
            await _context.SaveChangesAsync();

            return new OrderDto
            {
                Id = existingOrder.Id,
                OrderDate = existingOrder.OrderDate,
                CustomerId = existingOrder.CustomerId
            };
        }

        public async Task<bool> Delete(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return false;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
