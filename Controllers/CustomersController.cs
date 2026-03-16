using GarageApp.API.DTOs.Customers;
using GarageApp.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarageApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<IActionResult> Create(CreateCustomerDto model)
        {
            var customer = await _customerService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var customer = await _customerService.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound(new { message = "Customer not found." });
            }

            return Ok(customer);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<IActionResult> Update(Guid id, UpdateCustomerDto model)
        {
            var customer = await _customerService.UpdateAsync(id, model);

            if (customer == null)
            {
                return NotFound(new { message = "Customer not found." });
            }

            return Ok(customer);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _customerService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new { message = "Customer not found." });
            }

            return Ok(new { message = "Customer deleted successfully." });
        }
    }
}