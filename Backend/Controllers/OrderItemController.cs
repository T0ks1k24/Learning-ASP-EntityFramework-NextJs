using Entity_Framework.Models.Dtos.Customers;
using Entity_Framework.Models.Dtos.OrderItems;
using Entity_Framework.Repositories.Customers;
using Entity_Framework.Repositories.OrderItems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Entity_Framework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemReposity _orderItemReposity;

        public OrderItemController(IOrderItemReposity orderItemReposity)
        {
            _orderItemReposity = orderItemReposity;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orderItems = await _orderItemReposity.GetAll();
            if (orderItems == null) return NotFound();
            return Ok(orderItems);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var orderItem = await _orderItemReposity.GetById(id);
            if (orderItem == null) return NotFound();
            return Ok(orderItem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddUpdateOrderItemDto orderItemDto)
        {
            var createOrderItem = await _orderItemReposity.Create(orderItemDto);
            if (createOrderItem == null) return NotFound();
            return Ok(createOrderItem);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AddUpdateOrderItemDto orderItemDto)
        {
            var updateOrderItem = await _orderItemReposity.Update(id, orderItemDto);
            if (updateOrderItem == null) return NotFound();
            return Ok(updateOrderItem);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteOrderItem = await _orderItemReposity.Delete(id);
            if (!deleteOrderItem) return NotFound();
            return Ok(deleteOrderItem);
        }
    }
}
