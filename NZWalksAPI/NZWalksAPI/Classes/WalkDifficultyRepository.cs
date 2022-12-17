using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Classes
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;
        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext) 
        {
            _nZWalksDbContext= nZWalksDbContext;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id= Guid.NewGuid();
            await _nZWalksDbContext.WalkDifficulties.AddAsync(walkDifficulty);
            await _nZWalksDbContext.SaveChangesAsync();

            return walkDifficulty;

        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var existData = await _nZWalksDbContext.WalkDifficulties.FindAsync(id);

            if (existData != null)
            {
                _nZWalksDbContext.WalkDifficulties.Remove(existData);
                await _nZWalksDbContext.SaveChangesAsync();
                return existData;
            }

            return null;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await _nZWalksDbContext.WalkDifficulties.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid Id)
        {
            return await _nZWalksDbContext.WalkDifficulties.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existData = await _nZWalksDbContext.WalkDifficulties.FindAsync(id);

            if (existData != null)
            {
                existData.Code = walkDifficulty.Code;
                await _nZWalksDbContext.SaveChangesAsync();
                return existData;
            }

            return null;
        }
    }
}
