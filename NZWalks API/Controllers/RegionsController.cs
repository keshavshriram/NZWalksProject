using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            return Ok(regions); 
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

            // Changes 
        }

        // 
    }
}
