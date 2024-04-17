using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        public Task<List<Region>> GetRegionsAsync();
        public Task<Region> GetRegionAsync(Guid id);
        public Task<Region> CreateRegionAsync(Region region);
        public Task<Region?> UpdateRegionAsync(Guid id, Region region);
        public Task<Region?> DeleteRegionAsync(Guid id);

    }
}
