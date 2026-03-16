using GarageApp.API.DTOs.Cars;
using GarageApp.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarageApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<IActionResult> Create(CreateCarDto model)
        {
            try
            {
                var car = await _carService.CreateAsync(model);

                if (car == null)
                {
                    return BadRequest(new { message = "Customer not found." });
                }

                return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<IActionResult> GetAll()
        {
            var cars = await _carService.GetAllAsync();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var car = await _carService.GetByIdAsync(id);

            if (car == null)
            {
                return NotFound(new { message = "Car not found." });
            }

            return Ok(car);
        }

        [HttpGet("customer/{customerId}")]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<IActionResult> GetByCustomerId(Guid customerId)
        {
            var cars = await _carService.GetByCustomerIdAsync(customerId);
            return Ok(cars);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<IActionResult> Update(Guid id, UpdateCarDto model)
        {
            try
            {
                var car = await _carService.UpdateAsync(id, model);

                if (car == null)
                {
                    return NotFound(new { message = "Car not found." });
                }

                return Ok(car);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _carService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new { message = "Car not found." });
            }

            return Ok(new { message = "Car deleted successfully." });
        }
    }
}