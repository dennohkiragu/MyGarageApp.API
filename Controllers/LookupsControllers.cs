using GarageApp.API.DTOs.Lookups;
using GarageApp.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarageApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class LookupsController : ControllerBase
    {
        private readonly ILookupService _lookupService;

        public LookupsController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        [HttpPost("masters")]
        public async Task<IActionResult> CreateMaster(CreateLookupMasterDto model)
        {
            try
            {
                var master = await _lookupService.CreateMasterAsync(model);
                return CreatedAtAction(nameof(GetMasterById), new { id = master.Id }, master);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("masters")]
        public async Task<IActionResult> GetMasters()
        {
            var masters = await _lookupService.GetMastersAsync();
            return Ok(masters);
        }

        [HttpGet("masters/{id}")]
        public async Task<IActionResult> GetMasterById(Guid id)
        {
            var master = await _lookupService.GetMasterByIdAsync(id);

            if (master == null)
            {
                return NotFound(new { message = "Lookup master not found." });
            }

            return Ok(master);
        }

        [HttpPut("masters/{id}")]
        public async Task<IActionResult> UpdateMaster(Guid id, UpdateLookupMasterDto model)
        {
            try
            {
                var master = await _lookupService.UpdateMasterAsync(id, model);

                if (master == null)
                {
                    return NotFound(new { message = "Lookup master not found." });
                }

                return Ok(master);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("masters/{id}")]
        public async Task<IActionResult> DeleteMaster(Guid id)
        {
            var deleted = await _lookupService.DeleteMasterAsync(id);

            if (!deleted)
            {
                return NotFound(new { message = "Lookup master not found." });
            }

            return Ok(new { message = "Lookup master deleted successfully." });
        }

        [HttpPost("items")]
        public async Task<IActionResult> CreateItem(CreateLookupItemDto model)
        {
            try
            {
                var item = await _lookupService.CreateItemAsync(model);

                if (item == null)
                {
                    return BadRequest(new { message = "Lookup master not found." });
                }

                return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("items")]
        public async Task<IActionResult> GetItems()
        {
            var items = await _lookupService.GetItemsAsync();
            return Ok(items);
        }

        [HttpGet("items/{id}")]
        public async Task<IActionResult> GetItemById(Guid id)
        {
            var item = await _lookupService.GetItemByIdAsync(id);

            if (item == null)
            {
                return NotFound(new { message = "Lookup item not found." });
            }

            return Ok(item);
        }

        [HttpPut("items/{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, UpdateLookupItemDto model)
        {
            try
            {
                var item = await _lookupService.UpdateItemAsync(id, model);

                if (item == null)
                {
                    return NotFound(new { message = "Lookup item not found." });
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("items/{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var deleted = await _lookupService.DeleteItemAsync(id);

            if (!deleted)
            {
                return NotFound(new { message = "Lookup item not found." });
            }

            return Ok(new { message = "Lookup item deleted successfully." });
        }

        [HttpGet("{masterCode}/items")]
        public async Task<IActionResult> GetItemsByMasterCode(string masterCode, [FromQuery] string? search)
        {
            var items = await _lookupService.GetItemsByMasterCodeAsync(masterCode, search);
            return Ok(items);
        }
    }
}