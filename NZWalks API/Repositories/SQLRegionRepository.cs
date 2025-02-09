using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks_API.Data;
using NZWalks_API.Models.Domain;
using NZWalks_API.Models.DTO;

namespace NZWalks_API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext _dbContext;
        public SQLRegionRepository(NZWalksDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Region?>> GetAllAsync()
        {
            var result = await _dbContext.Regions.ToListAsync();
            return result;
        }

        public async Task<Region?> GetRegionByIdAsync(Guid Id)
        {
            var item = await _dbContext.Regions.FirstOrDefaultAsync(record => record.Id == Id);
            return item;
        }

        public async Task<Region?> CreateRegionAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            return region;
            
        }

        public async Task<Region?> UpdateRegionAsync(Guid Id , UpdateRegionDto region)
        {
            Region existingRegion =await _dbContext.Regions.FirstOrDefaultAsync(record => record.Id == Id);

            existingRegion.Name = region.Name;
            existingRegion.Code = region.Code;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await _dbContext.SaveChangesAsync();

            return existingRegion;
        }

        public async Task<Region?> DeleteRegionAsync(Guid Id)
        {
            Region existingRegion = await _dbContext.Regions.FirstOrDefaultAsync(region => region.Id == Id);
            if (existingRegion != null)
            { 
                 _dbContext.Regions.Remove(existingRegion);
                await _dbContext.SaveChangesAsync();
            }
            return existingRegion;


        }

    }
}
