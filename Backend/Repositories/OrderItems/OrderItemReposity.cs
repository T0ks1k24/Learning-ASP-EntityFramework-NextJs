using Entity_Framework.Data;
using Entity_Framework.Models.Dtos.Customers;
using Entity_Framework.Models.Dtos.OrderItems;
using Entity_Framework.Models.Entitys;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework.Repositories.OrderItems
{
    public class OrderItemReposity : IOrderItemReposity
    {
        private readonly LearningDbContext _context;
        public OrderItemReposity(LearningDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderItemDto>> GetAll()
        {
            var orderItems = await _context.OrderItems
                .AsNoTracking()
                .ToListAsync();

            var orderItemDtos = orderItems.Select(orderItems => new OrderItemDto
            {
                Id = orderItems.Id,
                OrderId = orderItems.OrderId,
                ProductId = orderItems.ProductId,
                Quantity = orderItems.Quantity,
                Price = orderItems.Price,
            }).ToList();

            return orderItemDtos;
        }

        public async Task<OrderItemDto> GetById(Guid id)
        {
            var orderItem = await _context.OrderItems
                .AsNoTracking()
                .FirstOrDefaultAsync(oi => oi.Id == id);

            if(orderItem == null)
            {
                return null;
            }

            return new OrderItemDto
            {
                Id = orderItem.Id,
                OrderId = orderItem.OrderId,
                ProductId = orderItem.ProductId,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price,
            };
        }

        public async Task<OrderItemDto> Create(AddUpdateOrderItemDto orderItemDto)
        {
            var orderItemEntity = new OrderItemEntity
            {
                OrderId = orderItemDto.OrderId,
                ProductId = orderItemDto.ProductId,
                Quantity = orderItemDto.Quantity,
                Price = orderItemDto.Price
            };

            await _context.OrderItems.AddAsync(orderItemEntity);
            await _context.SaveChangesAsync();

            return new OrderItemDto
            {
                Id = orderItemEntity.Id,
                OrderId = orderItemDto.OrderId,
                ProductId = orderItemDto.ProductId,
                Quantity = orderItemDto.Quantity,
                Price = orderItemDto.Price,
            };
        }

        public async Task<OrderItemDto> Update(Guid id, AddUpdateOrderItemDto orderItemDto)
        {
            var existingOrderItem = await _context.OrderItems
                .AsNoTracking()
                .FirstOrDefaultAsync(oi => oi.Id == id);

            if (existingOrderItem == null) { return null; }

            existingOrderItem.OrderId = orderItemDto.OrderId;
            existingOrderItem.ProductId = orderItemDto.ProductId;
            existingOrderItem.Quantity = orderItemDto.Quantity;
            existingOrderItem.Price = orderItemDto.Price;

            await _context.SaveChangesAsync();

            return new OrderItemDto
            {
                Id = existingOrderItem.Id,
                OrderId = orderItemDto.OrderId,
                ProductId = orderItemDto.ProductId,
                Quantity = orderItemDto.Quantity,
                Price = orderItemDto.Price
            };
        }

        public async Task<bool> Delete(Guid id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);

            if (orderItem == null)
            {
                return false;
            }

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();

            return true;
        }


    }
}
