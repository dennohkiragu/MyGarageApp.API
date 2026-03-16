using GarageApp.API.DTOs.Invoices;
using GarageApp.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarageApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInvoiceDto model)
        {
            try
            {
                var invoice = await _invoiceService.CreateAsync(model);

                if (invoice == null)
                {
                    return BadRequest(new { message = "Job card not found." });
                }

                return CreatedAtAction(nameof(GetById), new { id = invoice.Id }, invoice);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var invoices = await _invoiceService.GetAllAsync();
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var invoice = await _invoiceService.GetByIdAsync(id);

            if (invoice == null)
            {
                return NotFound(new { message = "Invoice not found." });
            }

            return Ok(invoice);
        }

        [HttpGet("jobcard/{jobCardId}")]
        public async Task<IActionResult> GetByJobCardId(Guid jobCardId)
        {
            var invoice = await _invoiceService.GetByJobCardIdAsync(jobCardId);

            if (invoice == null)
            {
                return NotFound(new { message = "Invoice not found for this job card." });
            }

            return Ok(invoice);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateInvoiceDto model)
        {
            try
            {
                var invoice = await _invoiceService.UpdateAsync(id, model);

                if (invoice == null)
                {
                    return NotFound(new { message = "Invoice not found." });
                }

                return Ok(invoice);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}/payment-status")]
        public async Task<IActionResult> UpdatePaymentStatus(Guid id, UpdateInvoicePaymentDto model)
        {
            try
            {
                var invoice = await _invoiceService.UpdatePaymentStatusAsync(id, model);

                if (invoice == null)
                {
                    return NotFound(new { message = "Invoice not found." });
                }

                return Ok(invoice);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _invoiceService.DeleteAsync(id);

                if (!deleted)
                {
                    return NotFound(new { message = "Invoice not found." });
                }

                return Ok(new { message = "Invoice deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}