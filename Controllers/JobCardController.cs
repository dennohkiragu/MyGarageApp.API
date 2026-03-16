using System.Security.Claims;
using GarageApp.API.DTOs.JobCards;
using GarageApp.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarageApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobCardsController : ControllerBase
    {
        private readonly IJobCardService _jobCardService;

        public JobCardsController(IJobCardService jobCardService)
        {
            _jobCardService = jobCardService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<IActionResult> Create(CreateJobCardDto model)
        {
            try
            {
                var jobCard = await _jobCardService.CreateAsync(model);

                if (jobCard == null)
                {
                    return BadRequest(new { message = "Customer or car not found." });
                }

                return CreatedAtAction(nameof(GetById), new { id = jobCard.Id }, jobCard);
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
            var jobCards = await _jobCardService.GetAllAsync();
            return Ok(jobCards);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Receptionist,Mechanic")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var jobCard = await _jobCardService.GetByIdAsync(id);

            if (jobCard == null)
            {
                return NotFound(new { message = "Job card not found." });
            }

            return Ok(jobCard);
        }

        [HttpGet("customer/{customerId}")]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<IActionResult> GetByCustomerId(Guid customerId)
        {
            var jobCards = await _jobCardService.GetByCustomerIdAsync(customerId);
            return Ok(jobCards);
        }

        [HttpGet("car/{carId}")]
        [Authorize(Roles = "Admin,Receptionist,Mechanic")]
        public async Task<IActionResult> GetByCarId(Guid carId)
        {
            var jobCards = await _jobCardService.GetByCarIdAsync(carId);
            return Ok(jobCards);
        }

        [HttpGet("my-assigned")]
        [Authorize(Roles = "Mechanic")]
        public async Task<IActionResult> GetMyAssigned()
        {
            var mechanicId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(mechanicId))
            {
                return Unauthorized(new { message = "User ID not found in token." });
            }

            var jobCards = await _jobCardService.GetByAssignedMechanicIdAsync(mechanicId);
            return Ok(jobCards);
        }

        [HttpPatch("{id}/assign-mechanic")]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<IActionResult> AssignMechanic(Guid id, AssignMechanicDto model)
        {
            try
            {
                var jobCard = await _jobCardService.AssignMechanicAsync(id, model);

                if (jobCard == null)
                {
                    return NotFound(new { message = "Job card not found." });
                }

                return Ok(jobCard);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Receptionist,Mechanic")]
        public async Task<IActionResult> Update(Guid id, UpdateJobCardDto model)
        {
            var jobCard = await _jobCardService.UpdateAsync(id, model);

            if (jobCard == null)
            {
                return NotFound(new { message = "Job card not found." });
            }

            return Ok(jobCard);
        }

        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> UpdateStatus(Guid id, UpdateJobCardStatusDto model)
        {
            var jobCard = await _jobCardService.UpdateStatusAsync(id, model);

            if (jobCard == null)
            {
                return NotFound(new { message = "Job card not found." });
            }

            return Ok(jobCard);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _jobCardService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new { message = "Job card not found." });
            }

            return Ok(new { message = "Job card deleted successfully." });
        }
    }
}