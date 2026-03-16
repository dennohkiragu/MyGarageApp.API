using GarageApp.API.Data;
using GarageApp.API.DTOs.Cars;
using GarageApp.API.Interfaces;
using GarageApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GarageApp.API.Services
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _context;

        public CarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CarDto?> CreateAsync(CreateCarDto model)
        {
            var customer = await _context.Customers.FindAsync(model.CustomerId);
            if (customer == null)
            {
                return null;
            }

            var registrationExists = await _context.Cars
                .AnyAsync(c => c.RegistrationNumber == model.RegistrationNumber);

            if (registrationExists)
            {
                throw new Exception("A car with this registration number already exists.");
            }

            var car = new Car
            {
                CustomerId = model.CustomerId,
                Make = model.Make,
                Model = model.Model,
                Year = model.Year,
                RegistrationNumber = model.RegistrationNumber,
                Color = model.Color,
                Vin = model.Vin
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return new CarDto
            {
                Id = car.Id,
                CustomerId = car.CustomerId,
                CustomerName = customer.FullName,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                RegistrationNumber = car.RegistrationNumber,
                Color = car.Color,
                Vin = car.Vin
            };
        }

        public async Task<List<CarDto>> GetAllAsync()
        {
            return await _context.Cars
                .Include(c => c.Customer)
                .Select(car => new CarDto
                {
                    Id = car.Id,
                    CustomerId = car.CustomerId,
                    CustomerName = car.Customer.FullName,
                    Make = car.Make,
                    Model = car.Model,
                    Year = car.Year,
                    RegistrationNumber = car.RegistrationNumber,
                    Color = car.Color,
                    Vin = car.Vin
                })
                .ToListAsync();
        }

        public async Task<CarDto?> GetByIdAsync(Guid id)
        {
            var car = await _context.Cars
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                return null;
            }

            return new CarDto
            {
                Id = car.Id,
                CustomerId = car.CustomerId,
                CustomerName = car.Customer.FullName,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                RegistrationNumber = car.RegistrationNumber,
                Color = car.Color,
                Vin = car.Vin
            };
        }

        public async Task<List<CarDto>> GetByCustomerIdAsync(Guid customerId)
        {
            return await _context.Cars
                .Include(c => c.Customer)
                .Where(c => c.CustomerId == customerId)
                .Select(car => new CarDto
                {
                    Id = car.Id,
                    CustomerId = car.CustomerId,
                    CustomerName = car.Customer.FullName,
                    Make = car.Make,
                    Model = car.Model,
                    Year = car.Year,
                    RegistrationNumber = car.RegistrationNumber,
                    Color = car.Color,
                    Vin = car.Vin
                })
                .ToListAsync();
        }

        public async Task<CarDto?> UpdateAsync(Guid id, UpdateCarDto model)
        {
            var car = await _context.Cars
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                return null;
            }

            var registrationExists = await _context.Cars
                .AnyAsync(c => c.RegistrationNumber == model.RegistrationNumber && c.Id != id);

            if (registrationExists)
            {
                throw new Exception("Another car with this registration number already exists.");
            }

            car.Make = model.Make;
            car.Model = model.Model;
            car.Year = model.Year;
            car.RegistrationNumber = model.RegistrationNumber;
            car.Color = model.Color;
            car.Vin = model.Vin;

            await _context.SaveChangesAsync();

            return new CarDto
            {
                Id = car.Id,
                CustomerId = car.CustomerId,
                CustomerName = car.Customer.FullName,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                RegistrationNumber = car.RegistrationNumber,
                Color = car.Color,
                Vin = car.Vin
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return false;
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}