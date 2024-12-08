using NZ_Walks.API.Models.Domain;

namespace NZ_Walks.API.Repositories
{
    public class InMemoryRepository : IRegionRepository
    {
        public Task<Region> createNewRegion(Region newRegion)
        {
            throw new NotImplementedException();
        }

        public Task<Region> deleteRegion(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Region>> GetAllAsync()
        {

            return new List<Region>(){

                new Region {
                    Id = Guid.NewGuid(),
                    Code="hello",
                    Name="sajjad",
                    RegionImageUrl="hhhhhhh"




                }


            };
        }

        public Task<Region> GetSingleRegion(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Region?> updateRegion(Guid id, Region region)
        {
            throw new NotImplementedException();
        }
    }
}
