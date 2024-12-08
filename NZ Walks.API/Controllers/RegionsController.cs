using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZ_Walks.API.Data;
using NZ_Walks.API.Models.Domain;
using NZ_Walks.API.Models.DTO;
using NZ_Walks.API.Repositories;

namespace NZ_Walks.API.Controllers
{
    //https://localhost:7179/api/region
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {

        public RegionsController(NZWalksDbContext dbContext , IRegionRepository regionRepository) 
        {
            DbContext = dbContext;
            RegionRepository = regionRepository;
        }

        public NZWalksDbContext DbContext { get; }
        public IRegionRepository RegionRepository { get; }

        [HttpGet]
        public async Task<IActionResult> getAll() {
            //var regions = new List<Region> {
            //  new Region{
            //    Id= Guid.NewGuid(),
            //    Name="ackland region",
            //    Code="akl",
            //    RegionImageUrl="https://unsplash.com/photos/a-bunch-of-balloons-that-are-shaped-like-email-7NT4EDSI5Ok"
            //  },
            //     new Region{
            //    Id= Guid.NewGuid(),
            //    Name="finland region",
            //    Code="fn",
            //    RegionImageUrl="https://unsplash.com/photos/a-bunch-of-balloons-that-are-shaped-like-email-7NT4EDSI5Ok"
            //  }

            //};



            var regions = await RegionRepository.GetAllAsync();

            var DtoRegions=new List<RegionDto>();

            foreach (var region in regions) {

                DtoRegions.Add(new RegionDto
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl,
                });
            
            }

            return Ok(DtoRegions);

        }


        // get single region (get by id)
        // get: https://localhost:7179/api/regions/{id}
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> getRegionById([FromRoute]Guid id) {

            //var region = DbContext.Regions.Find(id);

            // Get data from database
            //var region = await DbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var region = await RegionRepository.GetSingleRegion(id);

            // map domain modals to dtos



            if (region == null) {
                return NotFound();
            }

            var regionDto = new RegionDto
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl,

            };
            return Ok(regionDto);
        }

        // create a new region
        // post: https://localhost:7179/api/regions
        [HttpPost]

        public async Task<IActionResult> create([FromBody] AddRegionRequestDto addRegionRequest) {

            var regionDomainModal = new Region
            {

                Name = addRegionRequest.Name,
                Code = addRegionRequest.Code,
                RegionImageUrl = addRegionRequest.RegionImageUrl,

            };

            //await DbContext.Regions.AddAsync(regionDomainModal);
            await RegionRepository.createNewRegion(regionDomainModal);
            //DbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = regionDomainModal.Id,
                Name = regionDomainModal.Name,
                Code = regionDomainModal.Code,
                RegionImageUrl = regionDomainModal.RegionImageUrl,

            };




           return CreatedAtAction(nameof(getRegionById), new { id = regionDto.Id }, regionDto);

           
          
        }


        [HttpPut]
        [Route("{id:guid}")]

        //update a region
        //put: https://localhost:7179/api/regions/{id}

        public async Task<IActionResult> Update([FromRoute] Guid id , [FromBody] UpdateRegionRequestDto updateRegionRequest) {

            //var regionDomainModal=await DbContext.Regions.FirstOrDefaultAsync(x => x.Id == id); 

            var domainModal = new Region
            {

                Name = updateRegionRequest.Name,
                Code = updateRegionRequest.Code,
                RegionImageUrl = updateRegionRequest.RegionImageUrl,


            };

            var regionDomainModal = await RegionRepository.updateRegion(id, domainModal);


            if (regionDomainModal == null) {
                NotFound();
            
            }
            //Map dto to domain modal
            regionDomainModal.Code = updateRegionRequest.Code;
            regionDomainModal.RegionImageUrl = updateRegionRequest.RegionImageUrl;
            regionDomainModal.Name = updateRegionRequest.Name;

            DbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = regionDomainModal.Id,
                Code = regionDomainModal.Code,
                RegionImageUrl = regionDomainModal.RegionImageUrl,
                Name = regionDomainModal.Name

            };


            return Ok(regionDto);



        }


        // delete a region

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> delete([FromRoute] Guid id) {

            //var regionTODelete =  await DbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            var regionTODelete = await RegionRepository.deleteRegion(id);


            if (regionTODelete == null)
            {
                return NotFound();
            }

            var domainModal = new RegionDto
            {
                Code = regionTODelete.Code,
                Name = regionTODelete.Name,
                RegionImageUrl = regionTODelete.RegionImageUrl
            };
            //DbContext.Regions.Remove(regionTODelete);

            //DbContext.SaveChangesAsync();


            return Ok(domainModal);
;









        }

















    }
}
