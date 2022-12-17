using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllAsync();
        Task<WalkDifficulty> GetAsync(Guid Id);
        Task<WalkDifficulty> AddAsync(WalkDifficulty walk);
        Task<WalkDifficulty> UpdateAsync(Guid Id, WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> DeleteAsync(Guid Id);
    }
}