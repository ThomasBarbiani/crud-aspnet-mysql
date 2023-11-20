
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Services
{
    public interface IWalkService
    {
        Task<IEnumerable<Walk>> GetAllAsync();
        Task<Walk?> GetAsync(Guid id);
        Task<Walk> AddAsync(Walk walk);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteAsync(Guid id);

    }
}
