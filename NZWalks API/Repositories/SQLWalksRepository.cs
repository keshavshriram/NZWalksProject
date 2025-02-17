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
            Walk? walkDomain = await _dbContext.Walks.Include("Dificulty").Include("Region").FirstOrDefaultAsync(walk => walk.Id == Id);
            return walkDomain;
        }

        public async Task<Walk?> UpdateWalkAsync(Guid Id , UpdateWalkDto walk)
        {
            Walk? existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(walk => walk.Id == Id);
            if (existingWalk != null)
            {
                existingWalk.Name = walk.Name;
                existingWalk.Description = walk.Description;
                existingWalk.LengthInKm = walk.LengthInKm;
                existingWalk.WalkImageUrl = walk.WalkImageUrl;
                existingWalk.DificultyId = walk.DificultyId;
                existingWalk.RegionId = walk.RegionId;

                await _dbContext.SaveChangesAsync();
            }
            return existingWalk;
        }

        public async Task<Walk?> DeleteWalkAsync(Guid Id)
        {
            Walk? existingRecord = await _dbContext.Walks.Include("Dificulty").Include("Region").FirstOrDefaultAsync(walk => walk.Id == Id);
            if (existingRecord != null)
            {
                _dbContext.Remove(existingRecord);
                await _dbContext.SaveChangesAsync();
            }

            return existingRecord;
        }
    }
}
