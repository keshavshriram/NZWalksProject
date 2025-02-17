using NZWalks_API.Models.Domain;
using NZWalks_API.Models.DTO;

namespace NZWalks_API.Repositories
{
    public interface IWalksRepository
    {
        Task<Walk> CreateWalksAsync(Walk walk);
        Task<List<Walk>> GetAllAsync();
        Task<Walk?> GetWalkByIdAsync(Guid Id);
        Task<Walk?> UpdateWalkAsync(Guid Id , UpdateWalkDto walk);
        Task<Walk?> DeleteWalkAsync(Guid Id);
    }
}
