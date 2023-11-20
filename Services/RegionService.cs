using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Services
{
    public class RegionService : IRegionService
    {
        private readonly NZWalksDbContext nzWalksDbContext;

        public RegionService(NZWalksDbContext nzWalksDbContext)
        {
            this.nzWalksDbContext = nzWalksDbContext;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nzWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetAsync(Guid id)
        {
            var region = await nzWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (region == null)
            {
                return null;
            }

            return region;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nzWalksDbContext.AddAsync(region);
            await nzWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await nzWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Latitude = region.Latitude;
            existingRegion.Longitude = region.Longitude;
            existingRegion.Population = region.Population;

            await nzWalksDbContext.SaveChangesAsync();

            return existingRegion;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var region = await nzWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (region == null)
            {
                return null;
            }

            nzWalksDbContext.Regions.Remove(region);
            await nzWalksDbContext.SaveChangesAsync();
            return region;
        }
    }
}
