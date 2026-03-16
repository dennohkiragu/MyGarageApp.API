using GarageApp.API.Data;
using GarageApp.API.DTOs.Lookups;
using GarageApp.API.Interfaces;
using GarageApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GarageApp.API.Services
{
    public class LookupService : ILookupService
    {
        private readonly ApplicationDbContext _context;

        public LookupService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LookupMasterDto> CreateMasterAsync(CreateLookupMasterDto model)
        {
            var codeExists = await _context.LookupMasters
                .AnyAsync(x => x.Code == model.Code);

            if (codeExists)
            {
                throw new Exception("A lookup master with this code already exists.");
            }

            var master = new LookupMaster
            {
                Name = model.Name,
                Code = model.Code,
                Description = model.Description
            };

            _context.LookupMasters.Add(master);
            await _context.SaveChangesAsync();

            return new LookupMasterDto
            {
                Id = master.Id,
                Name = master.Name,
                Code = master.Code,
                Description = master.Description
            };
        }

        public async Task<List<LookupMasterDto>> GetMastersAsync()
        {
            return await _context.LookupMasters
                .OrderBy(x => x.Name)
                .Select(x => new LookupMasterDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    Description = x.Description
                })
                .ToListAsync();
        }

        public async Task<LookupMasterDto?> GetMasterByIdAsync(Guid id)
        {
            var master = await _context.LookupMasters.FindAsync(id);

            if (master == null)
            {
                return null;
            }

            return new LookupMasterDto
            {
                Id = master.Id,
                Name = master.Name,
                Code = master.Code,
                Description = master.Description
            };
        }

        public async Task<LookupMasterDto?> UpdateMasterAsync(Guid id, UpdateLookupMasterDto model)
        {
            var master = await _context.LookupMasters.FindAsync(id);

            if (master == null)
            {
                return null;
            }

            var codeExists = await _context.LookupMasters
                .AnyAsync(x => x.Code == model.Code && x.Id != id);

            if (codeExists)
            {
                throw new Exception("Another lookup master with this code already exists.");
            }

            master.Name = model.Name;
            master.Code = model.Code;
            master.Description = model.Description;

            await _context.SaveChangesAsync();

            return new LookupMasterDto
            {
                Id = master.Id,
                Name = master.Name,
                Code = master.Code,
                Description = master.Description
            };
        }

        public async Task<bool> DeleteMasterAsync(Guid id)
        {
            var master = await _context.LookupMasters.FindAsync(id);

            if (master == null)
            {
                return false;
            }

            _context.LookupMasters.Remove(master);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<LookupItemDto?> CreateItemAsync(CreateLookupItemDto model)
        {
            var master = await _context.LookupMasters.FindAsync(model.LookupMasterId);

            if (master == null)
            {
                return null;
            }

            var codeExists = await _context.LookupItems
                .AnyAsync(x => x.LookupMasterId == model.LookupMasterId && x.Code == model.Code);

            if (codeExists)
            {
                throw new Exception("A lookup item with this code already exists under this master.");
            }

            var item = new LookupItem
            {
                LookupMasterId = model.LookupMasterId,
                Name = model.Name,
                Code = model.Code,
                Value = model.Value,
                IsActive = model.IsActive,
                SortOrder = model.SortOrder
            };

            _context.LookupItems.Add(item);
            await _context.SaveChangesAsync();

            return new LookupItemDto
            {
                Id = item.Id,
                LookupMasterId = item.LookupMasterId,
                LookupMasterCode = master.Code,
                Name = item.Name,
                Code = item.Code,
                Value = item.Value,
                IsActive = item.IsActive,
                SortOrder = item.SortOrder
            };
        }

        public async Task<List<LookupItemDto>> GetItemsAsync()
        {
            return await _context.LookupItems
                .Include(x => x.LookupMaster)
                .OrderBy(x => x.LookupMaster.Name)
                .ThenBy(x => x.SortOrder)
                .ThenBy(x => x.Name)
                .Select(x => new LookupItemDto
                {
                    Id = x.Id,
                    LookupMasterId = x.LookupMasterId,
                    LookupMasterCode = x.LookupMaster.Code,
                    Name = x.Name,
                    Code = x.Code,
                    Value = x.Value,
                    IsActive = x.IsActive,
                    SortOrder = x.SortOrder
                })
                .ToListAsync();
        }

        public async Task<LookupItemDto?> GetItemByIdAsync(Guid id)
        {
            var item = await _context.LookupItems
                .Include(x => x.LookupMaster)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
            {
                return null;
            }

            return new LookupItemDto
            {
                Id = item.Id,
                LookupMasterId = item.LookupMasterId,
                LookupMasterCode = item.LookupMaster.Code,
                Name = item.Name,
                Code = item.Code,
                Value = item.Value,
                IsActive = item.IsActive,
                SortOrder = item.SortOrder
            };
        }

        public async Task<LookupItemDto?> UpdateItemAsync(Guid id, UpdateLookupItemDto model)
        {
            var item = await _context.LookupItems
                .Include(x => x.LookupMaster)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
            {
                return null;
            }

            var codeExists = await _context.LookupItems
                .AnyAsync(x => x.LookupMasterId == item.LookupMasterId && x.Code == model.Code && x.Id != id);

            if (codeExists)
            {
                throw new Exception("Another lookup item with this code already exists under this master.");
            }

            item.Name = model.Name;
            item.Code = model.Code;
            item.Value = model.Value;
            item.IsActive = model.IsActive;
            item.SortOrder = model.SortOrder;

            await _context.SaveChangesAsync();

            return new LookupItemDto
            {
                Id = item.Id,
                LookupMasterId = item.LookupMasterId,
                LookupMasterCode = item.LookupMaster.Code,
                Name = item.Name,
                Code = item.Code,
                Value = item.Value,
                IsActive = item.IsActive,
                SortOrder = item.SortOrder
            };
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var item = await _context.LookupItems.FindAsync(id);

            if (item == null)
            {
                return false;
            }

            _context.LookupItems.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<LookupItemDto>> GetItemsByMasterCodeAsync(string masterCode, string? search)
        {
            var query = _context.LookupItems
                .Include(x => x.LookupMaster)
                .Where(x => x.LookupMaster.Code == masterCode && x.IsActive);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim().ToLower();

                query = query.Where(x =>
                    x.Name.ToLower().Contains(term) ||
                    x.Code.ToLower().Contains(term) ||
                    x.Value.ToLower().Contains(term));
            }

            return await query
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.Name)
                .Select(x => new LookupItemDto
                {
                    Id = x.Id,
                    LookupMasterId = x.LookupMasterId,
                    LookupMasterCode = x.LookupMaster.Code,
                    Name = x.Name,
                    Code = x.Code,
                    Value = x.Value,
                    IsActive = x.IsActive,
                    SortOrder = x.SortOrder
                })
                .ToListAsync();
        }
    }
}