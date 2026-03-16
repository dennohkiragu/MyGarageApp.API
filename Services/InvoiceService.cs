using GarageApp.API.Data;
using GarageApp.API.DTOs.Invoices;
using GarageApp.API.Interfaces;
using GarageApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GarageApp.API.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ApplicationDbContext _context;

        public InvoiceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<InvoiceDto?> CreateAsync(CreateInvoiceDto model)
        {
            var jobCard = await _context.JobCards
                .Include(j => j.Customer)
                .Include(j => j.Car)
                .Include(j => j.Invoice)
                .FirstOrDefaultAsync(j => j.Id == model.JobCardId);

            if (jobCard == null)
            {
                return null;
            }

            if (jobCard.Invoice != null)
            {
                throw new Exception("An invoice for this job card already exists.");
            }

            if (model.Items == null || !model.Items.Any())
            {
                throw new Exception("Invoice must have at least one item.");
            }

            var invoice = new Invoice
            {
                JobCardId = model.JobCardId,
                IsPaid = false,
                DateCreated = DateTime.UtcNow
            };

            invoice.Items = model.Items.Select(x => new InvoiceItem
            {
                ItemName = x.ItemName,
                Amount = x.Amount
            }).ToList();

            invoice.TotalAmount = invoice.Items.Sum(x => x.Amount);

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(invoice.Id);
        }

        public async Task<List<InvoiceDto>> GetAllAsync()
        {
            return await _context.Invoices
                .Include(i => i.JobCard)
                    .ThenInclude(j => j.Customer)
                .Include(i => i.JobCard)
                    .ThenInclude(j => j.Car)
                .Include(i => i.Items)
                .Select(invoice => new InvoiceDto
                {
                    Id = invoice.Id,
                    JobCardId = invoice.JobCardId,
                    CustomerName = invoice.JobCard.Customer.FullName,
                    CarDisplayName = $"{invoice.JobCard.Car.Make} {invoice.JobCard.Car.Model} - {invoice.JobCard.Car.RegistrationNumber}",
                    TotalAmount = invoice.TotalAmount,
                    IsPaid = invoice.IsPaid,
                    DateCreated = invoice.DateCreated,
                    Items = invoice.Items.Select(x => new InvoiceItemDto
                    {
                        Id = x.Id,
                        ItemName = x.ItemName,
                        Amount = x.Amount
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<InvoiceDto?> GetByIdAsync(Guid id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.JobCard)
                    .ThenInclude(j => j.Customer)
                .Include(i => i.JobCard)
                    .ThenInclude(j => j.Car)
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
            {
                return null;
            }

            return new InvoiceDto
            {
                Id = invoice.Id,
                JobCardId = invoice.JobCardId,
                CustomerName = invoice.JobCard.Customer.FullName,
                CarDisplayName = $"{invoice.JobCard.Car.Make} {invoice.JobCard.Car.Model} - {invoice.JobCard.Car.RegistrationNumber}",
                TotalAmount = invoice.TotalAmount,
                IsPaid = invoice.IsPaid,
                DateCreated = invoice.DateCreated,
                Items = invoice.Items.Select(x => new InvoiceItemDto
                {
                    Id = x.Id,
                    ItemName = x.ItemName,
                    Amount = x.Amount
                }).ToList()
            };
        }

        public async Task<InvoiceDto?> GetByJobCardIdAsync(Guid jobCardId)
        {
            var invoice = await _context.Invoices
                .Include(i => i.JobCard)
                    .ThenInclude(j => j.Customer)
                .Include(i => i.JobCard)
                    .ThenInclude(j => j.Car)
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.JobCardId == jobCardId);

            if (invoice == null)
            {
                return null;
            }

            return new InvoiceDto
            {
                Id = invoice.Id,
                JobCardId = invoice.JobCardId,
                CustomerName = invoice.JobCard.Customer.FullName,
                CarDisplayName = $"{invoice.JobCard.Car.Make} {invoice.JobCard.Car.Model} - {invoice.JobCard.Car.RegistrationNumber}",
                TotalAmount = invoice.TotalAmount,
                IsPaid = invoice.IsPaid,
                DateCreated = invoice.DateCreated,
                Items = invoice.Items.Select(x => new InvoiceItemDto
                {
                    Id = x.Id,
                    ItemName = x.ItemName,
                    Amount = x.Amount
                }).ToList()
            };
        }

        public async Task<InvoiceDto?> UpdateAsync(Guid id, UpdateInvoiceDto model)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Items)
                .Include(i => i.JobCard)
                    .ThenInclude(j => j.Customer)
                .Include(i => i.JobCard)
                    .ThenInclude(j => j.Car)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
            {
                return null;
            }

            if (invoice.IsPaid)
            {
                throw new Exception("Paid invoices cannot be edited.");
            }

            if (model.Items == null || !model.Items.Any())
            {
                throw new Exception("Invoice must have at least one item.");
            }

            _context.InvoiceItems.RemoveRange(invoice.Items);

            invoice.Items = model.Items.Select(x => new InvoiceItem
            {
                InvoiceId = invoice.Id,
                ItemName = x.ItemName,
                Amount = x.Amount
            }).ToList();

            invoice.TotalAmount = invoice.Items.Sum(x => x.Amount);

            await _context.SaveChangesAsync();

            return await GetByIdAsync(invoice.Id);
        }

        public async Task<InvoiceDto?> UpdatePaymentStatusAsync(Guid id, UpdateInvoicePaymentDto model)
        {
            var invoice = await _context.Invoices
                .Include(i => i.JobCard)
                    .ThenInclude(j => j.Customer)
                .Include(i => i.JobCard)
                    .ThenInclude(j => j.Car)
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
            {
                return null;
            }

            if (invoice.IsPaid)
            {
                throw new Exception("Paid invoices cannot have their payment status changed.");
            }

            invoice.IsPaid = model.IsPaid;
            await _context.SaveChangesAsync();

            return await GetByIdAsync(invoice.Id);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return false;
            }

            if (invoice.IsPaid)
            {
                throw new Exception("Paid invoices cannot be deleted.");
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}