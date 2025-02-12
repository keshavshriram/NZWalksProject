using NZWalks_API.Models.Domain;
using NZWalks_API.Models.DTO;

namespace NZWalks_API.Repositories
{
    public interface IWalksRepository
    {
        Task<Walk> CreateWalksAsync(Walk walk);
    }
}
