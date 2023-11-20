using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Services
{
    public interface IRegionService
    {
        Task<IEnumerable<Region>> GetAllAsync();
        Task<Region?> GetAsync(Guid id);
        Task<Region> AddAsync(Region region);
        Task<Region?> UpdateAsync(Guid id, Region region);
        Task<Region?> DeleteAsync(Guid id);
    }
}
