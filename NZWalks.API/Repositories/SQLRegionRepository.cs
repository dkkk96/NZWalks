using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Region> CreateRegionAsync(Region region)
        {
            await dbContext.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var region =await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(region == null)
            {
                return null;
            }
            dbContext.Regions.Remove(region);
            return region;
        }

        public async Task<Region?> GetRegionAsync(Guid id)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);
            if (region == null)
                return null;
            return region;
        }

        public async Task<List<Region>> GetRegionsAsync()
        {
            var regions = await dbContext.Regions.ToListAsync();
            return regions;
        }

        public async Task<Region?> UpdateRegionAsync(Guid id, Region region)
        {
            var regionOld = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(regionOld != null)
            {
                regionOld.Code = region.Code;
                regionOld.Name = region.Name;
                regionOld.RegionImageUrl = region.RegionImageUrl;

                await dbContext.SaveChangesAsync();
                return regionOld;
            }
            return null;



        }
    }
}
