using Microsoft.AspNetCore.Mvc;
using NZ_Walks.API.Models.Domain;

namespace NZ_Walks.API.Repositories
{
    public interface IRegionRepository
    {
          Task<List<Region>>  GetAllAsync();

        Task<Region?> GetSingleRegion(Guid id);

        Task<Region> createNewRegion(Region newRegion);

        Task<Region?> updateRegion(Guid id, Region region );

        Task<Region> deleteRegion(Guid id);

    }
}
