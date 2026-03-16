using GarageApp.API.Data;
using GarageApp.API.DTOs.JobCards;
using GarageApp.API.Interfaces;
using GarageApp.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GarageApp.API.Services
{
    public class JobCardService : IJobCardService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public JobCardService(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<JobCardDto?> CreateAsync(CreateJobCardDto model)
        {
            var customer = await _context.Customers.FindAsync(model.CustomerId);
            if (customer == null)
            {
                return null;
            }

            var car = await _context.Cars.FindAsync(model.CarId);
            if (car == null)
            {
                return null;
            }

            if (car.CustomerId != model.CustomerId)
            {
                throw new Exception("The selected car does not belong to the selected customer.");
            }

            var jobCard = new JobCard
            {
                CustomerId = model.CustomerId,
                CarId = model.CarId,
                Complaint = model.Complaint,
                Diagnosis = string.Empty,
                WorkDone = string.Empty,
                Status = JobCardStatus.Pending,
                DateCreated = DateTime.UtcNow,
                AssignedMechanicId = null
            };

            _context.JobCards.Add(jobCard);
            await _context.SaveChangesAsync();

            return new JobCardDto
            {
                Id = jobCard.Id,
                CustomerId = jobCard.CustomerId,
                CarId = jobCard.CarId,
                AssignedMechanicId = jobCard.AssignedMechanicId,
                AssignedMechanicName = string.Empty,
                CustomerName = customer.FullName,
                CarDisplayName = $"{car.Make} {car.Model} - {car.RegistrationNumber}",
                Complaint = jobCard.Complaint,
                Diagnosis = jobCard.Diagnosis,
                WorkDone = jobCard.WorkDone,
                Status = jobCard.Status,
                DateCreated = jobCard.DateCreated
            };
        }

        public async Task<List<JobCardDto>> GetAllAsync()
        {
            var jobCards = await _context.JobCards
                .Include(j => j.Customer)
                .Include(j => j.Car)
                .ToListAsync();

            var result = new List<JobCardDto>();

            foreach (var jobCard in jobCards)
            {
                var mechanicName = string.Empty;

                if (!string.IsNullOrWhiteSpace(jobCard.AssignedMechanicId))
                {
                    var mechanic = await _userManager.FindByIdAsync(jobCard.AssignedMechanicId);
                    mechanicName = mechanic?.FullName ?? mechanic?.UserName ?? string.Empty;
                }

                result.Add(new JobCardDto
                {
                    Id = jobCard.Id,
                    CustomerId = jobCard.CustomerId,
                    CarId = jobCard.CarId,
                    AssignedMechanicId = jobCard.AssignedMechanicId,
                    AssignedMechanicName = mechanicName,
                    CustomerName = jobCard.Customer.FullName,
                    CarDisplayName = $"{jobCard.Car.Make} {jobCard.Car.Model} - {jobCard.Car.RegistrationNumber}",
                    Complaint = jobCard.Complaint,
                    Diagnosis = jobCard.Diagnosis,
                    WorkDone = jobCard.WorkDone,
                    Status = jobCard.Status,
                    DateCreated = jobCard.DateCreated
                });
            }

            return result;
        }

        public async Task<JobCardDto?> GetByIdAsync(Guid id)
        {
            var jobCard = await _context.JobCards
                .Include(j => j.Customer)
                .Include(j => j.Car)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (jobCard == null)
            {
                return null;
            }

            var mechanicName = string.Empty;

            if (!string.IsNullOrWhiteSpace(jobCard.AssignedMechanicId))
            {
                var mechanic = await _userManager.FindByIdAsync(jobCard.AssignedMechanicId);
                mechanicName = mechanic?.FullName ?? mechanic?.UserName ?? string.Empty;
            }

            return new JobCardDto
            {
                Id = jobCard.Id,
                CustomerId = jobCard.CustomerId,
                CarId = jobCard.CarId,
                AssignedMechanicId = jobCard.AssignedMechanicId,
                AssignedMechanicName = mechanicName,
                CustomerName = jobCard.Customer.FullName,
                CarDisplayName = $"{jobCard.Car.Make} {jobCard.Car.Model} - {jobCard.Car.RegistrationNumber}",
                Complaint = jobCard.Complaint,
                Diagnosis = jobCard.Diagnosis,
                WorkDone = jobCard.WorkDone,
                Status = jobCard.Status,
                DateCreated = jobCard.DateCreated
            };
        }

        public async Task<List<JobCardDto>> GetByCustomerIdAsync(Guid customerId)
        {
            var jobCards = await _context.JobCards
                .Include(j => j.Customer)
                .Include(j => j.Car)
                .Where(j => j.CustomerId == customerId)
                .ToListAsync();

            var result = new List<JobCardDto>();

            foreach (var jobCard in jobCards)
            {
                var mechanicName = string.Empty;

                if (!string.IsNullOrWhiteSpace(jobCard.AssignedMechanicId))
                {
                    var mechanic = await _userManager.FindByIdAsync(jobCard.AssignedMechanicId);
                    mechanicName = mechanic?.FullName ?? mechanic?.UserName ?? string.Empty;
                }

                result.Add(new JobCardDto
                {
                    Id = jobCard.Id,
                    CustomerId = jobCard.CustomerId,
                    CarId = jobCard.CarId,
                    AssignedMechanicId = jobCard.AssignedMechanicId,
                    AssignedMechanicName = mechanicName,
                    CustomerName = jobCard.Customer.FullName,
                    CarDisplayName = $"{jobCard.Car.Make} {jobCard.Car.Model} - {jobCard.Car.RegistrationNumber}",
                    Complaint = jobCard.Complaint,
                    Diagnosis = jobCard.Diagnosis,
                    WorkDone = jobCard.WorkDone,
                    Status = jobCard.Status,
                    DateCreated = jobCard.DateCreated
                });
            }

            return result;
        }

        public async Task<List<JobCardDto>> GetByCarIdAsync(Guid carId)
        {
            var jobCards = await _context.JobCards
                .Include(j => j.Customer)
                .Include(j => j.Car)
                .Where(j => j.CarId == carId)
                .ToListAsync();

            var result = new List<JobCardDto>();

            foreach (var jobCard in jobCards)
            {
                var mechanicName = string.Empty;

                if (!string.IsNullOrWhiteSpace(jobCard.AssignedMechanicId))
                {
                    var mechanic = await _userManager.FindByIdAsync(jobCard.AssignedMechanicId);
                    mechanicName = mechanic?.FullName ?? mechanic?.UserName ?? string.Empty;
                }

                result.Add(new JobCardDto
                {
                    Id = jobCard.Id,
                    CustomerId = jobCard.CustomerId,
                    CarId = jobCard.CarId,
                    AssignedMechanicId = jobCard.AssignedMechanicId,
                    AssignedMechanicName = mechanicName,
                    CustomerName = jobCard.Customer.FullName,
                    CarDisplayName = $"{jobCard.Car.Make} {jobCard.Car.Model} - {jobCard.Car.RegistrationNumber}",
                    Complaint = jobCard.Complaint,
                    Diagnosis = jobCard.Diagnosis,
                    WorkDone = jobCard.WorkDone,
                    Status = jobCard.Status,
                    DateCreated = jobCard.DateCreated
                });
            }

            return result;
        }

        public async Task<List<JobCardDto>> GetByAssignedMechanicIdAsync(string mechanicId)
        {
            var jobCards = await _context.JobCards
                .Include(j => j.Customer)
                .Include(j => j.Car)
                .Where(j => j.AssignedMechanicId == mechanicId)
                .ToListAsync();

            var mechanic = await _userManager.FindByIdAsync(mechanicId);
            var mechanicName = mechanic?.FullName ?? mechanic?.UserName ?? string.Empty;

            return jobCards.Select(jobCard => new JobCardDto
            {
                Id = jobCard.Id,
                CustomerId = jobCard.CustomerId,
                CarId = jobCard.CarId,
                AssignedMechanicId = jobCard.AssignedMechanicId,
                AssignedMechanicName = mechanicName,
                CustomerName = jobCard.Customer.FullName,
                CarDisplayName = $"{jobCard.Car.Make} {jobCard.Car.Model} - {jobCard.Car.RegistrationNumber}",
                Complaint = jobCard.Complaint,
                Diagnosis = jobCard.Diagnosis,
                WorkDone = jobCard.WorkDone,
                Status = jobCard.Status,
                DateCreated = jobCard.DateCreated
            }).ToList();
        }

        public async Task<JobCardDto?> AssignMechanicAsync(Guid id, AssignMechanicDto model)
        {
            var jobCard = await _context.JobCards
                .Include(j => j.Customer)
                .Include(j => j.Car)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (jobCard == null)
            {
                return null;
            }

            var mechanic = await _userManager.FindByIdAsync(model.MechanicId);
            if (mechanic == null)
            {
                throw new Exception("Mechanic not found.");
            }

            var roles = await _userManager.GetRolesAsync(mechanic);
            if (!roles.Contains("Mechanic"))
            {
                throw new Exception("Selected user is not a mechanic.");
            }

            jobCard.AssignedMechanicId = model.MechanicId;
            await _context.SaveChangesAsync();

            return new JobCardDto
            {
                Id = jobCard.Id,
                CustomerId = jobCard.CustomerId,
                CarId = jobCard.CarId,
                AssignedMechanicId = jobCard.AssignedMechanicId,
                AssignedMechanicName = mechanic.FullName ?? mechanic.UserName ?? string.Empty,
                CustomerName = jobCard.Customer.FullName,
                CarDisplayName = $"{jobCard.Car.Make} {jobCard.Car.Model} - {jobCard.Car.RegistrationNumber}",
                Complaint = jobCard.Complaint,
                Diagnosis = jobCard.Diagnosis,
                WorkDone = jobCard.WorkDone,
                Status = jobCard.Status,
                DateCreated = jobCard.DateCreated
            };
        }

        public async Task<JobCardDto?> UpdateAsync(Guid id, UpdateJobCardDto model)
        {
            var jobCard = await _context.JobCards
                .Include(j => j.Customer)
                .Include(j => j.Car)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (jobCard == null)
            {
                return null;
            }

            jobCard.Complaint = model.Complaint;
            jobCard.Diagnosis = model.Diagnosis;
            jobCard.WorkDone = model.WorkDone;

            await _context.SaveChangesAsync();

            var mechanicName = string.Empty;

            if (!string.IsNullOrWhiteSpace(jobCard.AssignedMechanicId))
            {
                var mechanic = await _userManager.FindByIdAsync(jobCard.AssignedMechanicId);
                mechanicName = mechanic?.FullName ?? mechanic?.UserName ?? string.Empty;
            }

            return new JobCardDto
            {
                Id = jobCard.Id,
                CustomerId = jobCard.CustomerId,
                CarId = jobCard.CarId,
                AssignedMechanicId = jobCard.AssignedMechanicId,
                AssignedMechanicName = mechanicName,
                CustomerName = jobCard.Customer.FullName,
                CarDisplayName = $"{jobCard.Car.Make} {jobCard.Car.Model} - {jobCard.Car.RegistrationNumber}",
                Complaint = jobCard.Complaint,
                Diagnosis = jobCard.Diagnosis,
                WorkDone = jobCard.WorkDone,
                Status = jobCard.Status,
                DateCreated = jobCard.DateCreated
            };
        }

        public async Task<JobCardDto?> UpdateStatusAsync(Guid id, UpdateJobCardStatusDto model)
        {
            var jobCard = await _context.JobCards
                .Include(j => j.Customer)
                .Include(j => j.Car)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (jobCard == null)
            {
                return null;
            }

            jobCard.Status = model.Status;
            await _context.SaveChangesAsync();

            var mechanicName = string.Empty;

            if (!string.IsNullOrWhiteSpace(jobCard.AssignedMechanicId))
            {
                var mechanic = await _userManager.FindByIdAsync(jobCard.AssignedMechanicId);
                mechanicName = mechanic?.FullName ?? mechanic?.UserName ?? string.Empty;
            }

            return new JobCardDto
            {
                Id = jobCard.Id,
                CustomerId = jobCard.CustomerId,
                CarId = jobCard.CarId,
                AssignedMechanicId = jobCard.AssignedMechanicId,
                AssignedMechanicName = mechanicName,
                CustomerName = jobCard.Customer.FullName,
                CarDisplayName = $"{jobCard.Car.Make} {jobCard.Car.Model} - {jobCard.Car.RegistrationNumber}",
                Complaint = jobCard.Complaint,
                Diagnosis = jobCard.Diagnosis,
                WorkDone = jobCard.WorkDone,
                Status = jobCard.Status,
                DateCreated = jobCard.DateCreated
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var jobCard = await _context.JobCards.FindAsync(id);

            if (jobCard == null)
            {
                return false;
            }

            _context.JobCards.Remove(jobCard);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}