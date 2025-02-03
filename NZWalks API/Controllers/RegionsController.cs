using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using NZWalks_API.Data;
using NZWalks_API.Models.Domain;
using NZWalks_API.Models.DTO;
using System.Dynamic;

namespace NZWalks_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase 
    {
        private readonly NZWalksDBContext _dbContext ; 
        public RegionsController(NZWalksDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            List<Region> regions = this._dbContext.Regions.ToList();

            List<RegionDto> RegionDtoList = new List<RegionDto>();

            foreach ( var region in regions )
            {
                RegionDtoList.Add(new RegionDto
                {
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }
            return Ok(RegionDtoList); 
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult getRegionById([FromRoute] Guid id)
        {
            //dynamic result = new ExpandoObject();
            //var item = _dbContext.Regions.Find(id);

            //Holding item in a domain model

            var item = _dbContext.Regions.FirstOrDefault(record => record.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
                RegionImageUrl = item.RegionImageUrl
            };

            //result.Result = item;
            //result.Success = true; 

            return Ok(regionDto); 
        }
        [HttpPost]
        public IActionResult CreateRegion([FromBody] RegionCreateDto region)
        { 
            // Converted Dto to Domain Model . 
            Region regionDomain = new Region {
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            // I Used Domain Model To Create Region Record . 
            _dbContext.Regions.Add(regionDomain);
            _dbContext.SaveChanges();

            var RegionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return CreatedAtAction(nameof(getRegionById) , new { id = regionDomain.Id} , RegionDto);
        }
    }
}
