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

            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            List<Walk> walksDomain = await _dbContext.Walks.Include("Dificulty").Include("Region").ToListAsync();
            return walksDomain;

        }

        public async Task<Walk?> GetWalkByIdAsync(Guid Id)
        {
            Walk? walkDomain = await _dbContext.Walks.FirstOrDefaultAsync(walk => walk.Id == Id);
            return walkDomain;
        }
    }
}
