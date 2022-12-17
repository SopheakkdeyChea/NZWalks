using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Classes
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;
        public WalkRepository(NZWalksDbContext nZWalksDbContext) 
        { 
            _nZWalksDbContext= nZWalksDbContext;
        }
        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await _nZWalksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid Id)
        {
             return await _nZWalksDbContext.Walks
                            .Include(x => x.Region)
                            .Include(x => x.WalkDifficulty)
                            .FirstOrDefaultAsync(x => x.Id == Id);
        }
        
        public async Task<Walk> AddAsync(Walk walk)
        {
            // Assign new id
            walk.Id = Guid.NewGuid();
            await _nZWalksDbContext.Walks.AddAsync(walk);
            await _nZWalksDbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk> UpdateAsync(Guid Id, Walk walk)
        {
            var existData = await _nZWalksDbContext.Walks.FindAsync(Id);

            if (existData != null)
            {
                existData.Length = walk.Length;
                existData.Region = walk.Region;
                existData.WalkDifficulty = walk.WalkDifficulty;
                existData.Name= walk.Name;

                await _nZWalksDbContext.SaveChangesAsync();
                return existData;
            }

            return null;
        }

        public async Task<Walk> DeleteAsync(Guid Id)
        {
            var walk = await _nZWalksDbContext.Walks.FindAsync(Id);

            if (walk == null)
            {
                return null;
            }

            //Delete region
            _nZWalksDbContext.Walks.Remove(walk);
            await _nZWalksDbContext.SaveChangesAsync();
            return walk;
        }
    }
}
