using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks_API.Data;
using NZWalks_API.Models.Domain;

namespace NZWalks_API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext _dbContext;
        public SQLRegionRepository(NZWalksDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Region>> GetAllAsync()
        {
            var result = await _dbContext.Regions.ToListAsync();
            return result;
        }
    }
}
