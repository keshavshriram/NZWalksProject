using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NZWalks_API.Data;
using NZWalks_API.Models.Domain;
using NZWalks_API.Models.DTO;
using System.Linq;

namespace NZWalks_API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext _dbContext;
        public SQLRegionRepository(NZWalksDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Region?>> GetAllAsync(string? filterColumnName , string? filterValue , string? sortByColumn , bool? IsAscending , int PageIndex , int PageSize )  
        {
            var regions = _dbContext.Regions.AsQueryable();
            
                if ((filterColumnName != null && filterColumnName.Equals("name", StringComparison.OrdinalIgnoreCase)) || (sortByColumn != null && sortByColumn.Equals("name", StringComparison.OrdinalIgnoreCase )))
                {
                    if ( filterValue != null && filterColumnName != null )
                    {
                        regions = regions.Where(column => column.Name.Contains(filterValue));
                    }
                    
                    if ( IsAscending == true )
                    {
                        regions = regions.OrderBy(column => column.Name);
                    }
                    if (IsAscending == false)
                    {
                        regions = regions.OrderByDescending(column => column.Name);
                    }
                }

            regions = regions.Skip((PageIndex * PageSize)).Take(PageSize);

            
            return await regions.ToListAsync();
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
