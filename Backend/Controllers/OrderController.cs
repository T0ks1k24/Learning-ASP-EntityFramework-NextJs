using Entity_Framework.Models.Dtos.Orders;
using Entity_Framework.Models.Dtos.Products;
using Entity_Framework.Repositories.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Entity_Framework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderReposity _orderReposity;
        public OrderController(IOrderReposity orderReposity)
        {
            _orderReposity = orderReposity;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderReposity.GetAll();
            if(orders == null) return NotFound();
            return Ok(orders);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _orderReposity.GetById(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddUpdateOrderDto orderDto)
        {
            var createOrder = await _orderReposity.Create(orderDto);
            if(createOrder == null) return NotFound();
            return CreatedAtAction(nameof(GetById), new { id = createOrder.Id }, createOrder);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AddUpdateOrderDto orderDto)
        {
            var updateOrder = await _orderReposity.Update(id, orderDto);
            if (updateOrder == null) return NotFound();
            return Ok(updateOrder);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteOrder = await _orderReposity.Delete(id);
            if (!deleteOrder) return NotFound();
            return Ok(deleteOrder);
        }
    }
}
