﻿using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Services
{
    public class WalkService : IWalkService
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkService(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await nZWalksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk?> GetAsync(Guid id)
        {
            return await nZWalksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await nZWalksDbContext.Walks.AddAsync(walk);
            await nZWalksDbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await nZWalksDbContext.Walks.FindAsync(id);

            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Length = walk.Length;
            existingWalk.Name = walk.Name;
            existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await nZWalksDbContext.SaveChangesAsync();

            return existingWalk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await nZWalksDbContext.Walks.FindAsync(id);

            if (existingWalk == null)
            {
                return null;
            }

            nZWalksDbContext.Walks.Remove(existingWalk);

            await nZWalksDbContext.SaveChangesAsync();

            return existingWalk;
        }
    }
}
