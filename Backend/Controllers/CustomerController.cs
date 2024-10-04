using Entity_Framework.Models.Dtos.Customers;
using Entity_Framework.Repositories.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Entity_Framework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerReposity _customerReposity;

        public CustomerController(ICustomerReposity customerReposity)
        {
            _customerReposity = customerReposity;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerReposity.GetAll();
            if(customers == null) return NotFound();
            return Ok(customers);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var customer = await _customerReposity.GetById(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddUpdateCustomerDto customerDto)
        {
            var createCustomer = await _customerReposity.Create(customerDto);
            if (createCustomer == null) return NotFound();
            return Ok(createCustomer);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AddUpdateCustomerDto customerDto)
        {
            var updateCustomer = await _customerReposity.Update(id, customerDto);
            if (updateCustomer == null) return NotFound();
            return Ok(updateCustomer);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteCustomer = await _customerReposity.Delete(id);
            if (!deleteCustomer) return NotFound();
            return Ok(deleteCustomer);
        }
    }

}
