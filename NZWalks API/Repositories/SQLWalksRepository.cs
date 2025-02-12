using Microsoft.EntityFrameworkCore;
using NZWalks_API.Data;
using NZWalks_API.Models.Domain;
using NZWalks_API.Models.DTO;

namespace NZWalks_API.Repositories
{
    public class SQLWalksRepository : IWalksRepository
    {
        private readonly NZWalksDBContext _dbContext;
        public SQLWalksRepository(NZWalksDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Walk> CreateWalksAsync( Walk walk )
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();

            return new Walk();
        }
    }
}
