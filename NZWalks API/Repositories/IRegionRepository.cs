using Microsoft.AspNetCore.Mvc;
using NZWalks_API.Models.Domain;
using NZWalks_API.Models.DTO;

namespace NZWalks_API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region?>> GetAllAsync(string? filterColumnName , string? filterValue ,string? sortByColumn , bool? IsAscending , int PageIndex , int PageSize);
        Task<Region?> GetRegionByIdAsync(Guid Id);
        Task<Region?> CreateRegionAsync(Region region);
        Task<Region?> UpdateRegionAsync(Guid Id , UpdateRegionDto region);
        Task<Region?> DeleteRegionAsync(Guid Id);
    }
}
