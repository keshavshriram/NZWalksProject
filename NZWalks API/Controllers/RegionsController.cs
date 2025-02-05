using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly NZWalksDBContext _dbContext;
        public RegionsController(NZWalksDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public async  Task<IActionResult> GetAll()
        {
            List<Region> regions = await this._dbContext.Regions.ToListAsync();

            List<RegionDto> RegionDtoList = new List<RegionDto>();

            foreach (var region in regions)
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
        public async Task<IActionResult> getRegionById([FromRoute] Guid id)
        {
            //dynamic result = new ExpandoObject();
            //var item = _dbContext.Regions.Find(id);

            //Holding item in a domain model

            var item = await _dbContext.Regions.FirstOrDefaultAsync(record => record.Id == id);
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
        public async Task<IActionResult> CreateRegion([FromBody] RegionCreateDto region)
        {
            // Converted Dto to Domain Model . 
            Region regionDomain = new Region {
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            // I Used Domain Model To Create Region Record . 
            await _dbContext.Regions.AddAsync(regionDomain);
            await _dbContext.SaveChangesAsync();

            var RegionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return CreatedAtAction(nameof(getRegionById), new { id = regionDomain.Id }, RegionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDto regionDto)
        {
            // Checking existence of record with this id 

            var existingRecord = await _dbContext.Regions.FirstOrDefaultAsync(region => region.Id == id);

            if (existingRecord == null)
            {
                return NotFound();
            }


            // Updating Region
            existingRecord.Code = regionDto.Code;
            existingRecord.Name = regionDto.Name;
            existingRecord.RegionImageUrl = regionDto.RegionImageUrl;

            await _dbContext.SaveChangesAsync();

            var RegionUpdatedDto = new RegionDto
            {
                Id = existingRecord.Id,
                Code = existingRecord.Code,
                Name = existingRecord.Name,
                RegionImageUrl = existingRecord.RegionImageUrl
            };

            return Ok(RegionUpdatedDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var region = await _dbContext.Regions.FirstOrDefaultAsync(region => region.Id == id );
            if (region == null)
            {
                return NotFound();
            }
            _dbContext.Regions.Remove(region);
            await _dbContext.SaveChangesAsync();

            var regionDto = new RegionDto {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDto);
        }


    }
}
