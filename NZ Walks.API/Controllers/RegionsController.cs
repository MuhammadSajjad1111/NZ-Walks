using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZ_Walks.API.Data;
using NZ_Walks.API.Models.Domain;
using NZ_Walks.API.Models.DTO;

namespace NZ_Walks.API.Controllers
{
    //https://localhost:7179/api/region
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {

        public RegionsController(NZWalksDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public NZWalksDbContext DbContext { get; }

        [HttpGet]
        public IActionResult getAll() {
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



            var regions = DbContext.Regions.ToList();

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
        public IActionResult getRegionById([FromRoute]Guid id) {

            //var region = DbContext.Regions.Find(id);

            // Get data from database
            var region = DbContext.Regions.FirstOrDefault(x => x.Id == id);

            // map domain modals to dtos



            if (region == null) {
                NotFound();
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

        public IActionResult create([FromBody] AddRegionRequestDto addRegionRequest) {

            var regionDomainModal = new Region
            {

                Name = addRegionRequest.Name,
                Code = addRegionRequest.Code,
                RegionImageUrl = addRegionRequest.RegionImageUrl,

            };

            DbContext.Regions.Add(regionDomainModal);
            DbContext.SaveChanges();

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

        public IActionResult Update([FromRoute] Guid id , [FromBody] UpdateRegionRequestDto updateRegionRequest) {

         var regionDomainModal=DbContext.Regions.FirstOrDefault(x => x.Id == id);

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
















    }
}
