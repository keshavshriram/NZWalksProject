using Microsoft.AspNetCore.Mvc;
using NZWalks_API.Models.Domain;

namespace NZWalks_API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
    }
}
