using GarageApp.API.Data;
using GarageApp.API.DTOs.Customers;
using GarageApp.API.Interfaces;
using GarageApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GarageApp.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerDto> CreateAsync(CreateCustomerDto model)
        {
            var customer = new Customer
            {
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Address = model.Address
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return new CustomerDto
            {
                Id = customer.Id,
                FullName = customer.FullName,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                Address = customer.Address
            };
        }

        public async Task<List<CustomerDto>> GetAllAsync()
        {
            return await _context.Customers
                .Select(customer => new CustomerDto
                {
                    Id = customer.Id,
                    FullName = customer.FullName,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email,
                    Address = customer.Address
                })
                .ToListAsync();
        }

        public async Task<CustomerDto?> GetByIdAsync(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return null;
            }

            return new CustomerDto
            {
                Id = customer.Id,
                FullName = customer.FullName,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                Address = customer.Address
            };
        }

        public async Task<CustomerDto?> UpdateAsync(Guid id, UpdateCustomerDto model)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return null;
            }

            customer.FullName = model.FullName;
            customer.PhoneNumber = model.PhoneNumber;
            customer.Email = model.Email;
            customer.Address = model.Address;

            await _context.SaveChangesAsync();

            return new CustomerDto
            {
                Id = customer.Id,
                FullName = customer.FullName,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                Address = customer.Address
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}