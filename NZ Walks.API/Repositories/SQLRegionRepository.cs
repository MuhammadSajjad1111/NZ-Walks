using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZ_Walks.API.Data;
using NZ_Walks.API.Models.Domain;
using System.Runtime.InteropServices;

namespace NZ_Walks.API.Repositories
{
    public class SQLRegionRepository: IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;


        public SQLRegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Region> createNewRegion(Region newRegion)
        {
           await nZWalksDbContext.Regions.AddAsync(newRegion);

            await nZWalksDbContext.SaveChangesAsync();

            return newRegion;

        }

        public async Task<Region> deleteRegion(Guid id)
        {

            var region = await nZWalksDbContext.Regions.FirstOrDefaultAsync(i => i.Id == id);

            if (region == null) {
                return null;
            }

            nZWalksDbContext.Regions.Remove(region);

            nZWalksDbContext.SaveChangesAsync();

            return region;


            
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetSingleRegion(Guid id)
        {
          return  await nZWalksDbContext.Regions.FirstOrDefaultAsync( x=> x.Id == id);
        }

        public async Task<Region?> updateRegion(Guid id, Region region)
        {
            var foundRegion= await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (foundRegion == null) {
                return null;
            }

            foundRegion.Code=region.Code;
            foundRegion.Name=region.Name;
            foundRegion.RegionImageUrl=region.RegionImageUrl;

            await nZWalksDbContext.SaveChangesAsync();

            return foundRegion;


        }
    }
}
