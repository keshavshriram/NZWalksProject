using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using NZWalks_API.Data;
using NZWalks_API.Models.Domain;
using NZWalks_API.Models.DTO;
using NZWalks_API.Repositories;
using System.Dynamic;

namespace NZWalks_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper mapper;
        public RegionsController(NZWalksDBContext dbContext , IRegionRepository regionRepository , IMapper mapper)
        {
            this._dbContext = dbContext;
            this._regionRepository = regionRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        [Authorize]
        public async  Task<IActionResult> GetAll([FromQuery] string? filterColumnName , [FromQuery] string? filterValue , [FromQuery] string? sortByColumn , [FromQuery] bool? IsAscending = null , [FromQuery] int PageIndex = 0 , [FromQuery] int PageSize = 10 )
        {
            //List<Region> regions = await this._dbContext.Regions.ToListAsync();

            List<Region>? regions = await _regionRepository.GetAllAsync(filterColumnName, filterValue , sortByColumn , IsAscending ,  PageIndex , PageSize );

            //List<RegionDto> RegionDtoList = new List<RegionDto>();

            //foreach (var region in regions)
            //{
            //    RegionDtoList.Add(new RegionDto
            //    {
            //        Code = region.Code,
            //        Name = region.Name,
            //        RegionImageUrl = region.RegionImageUrl
            //    });
            //}

            var RegionDtoList = mapper.Map<List<RegionDto>>(regions);
            
            return Ok(RegionDtoList);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> getRegionById([FromRoute] Guid Id)
        {
            //dynamic result = new ExpandoObject();
            //var item = _dbContext.Regions.Find(id);

            //Holding item in a domain model
            Region item = await _regionRepository.GetRegionByIdAsync(Id);

            var mappedItem = mapper.Map<RegionDto>(item);

            if (mappedItem == null)
            {
                return NotFound();
            }

            //var regionDto = new RegionDto
            //{
            //    Id = item.Id,
            //    Code = item.Code,
            //    Name = item.Name,
            //    RegionImageUrl = item.RegionImageUrl
            //};

            //result.Result = item;
            //result.Success = true; 

            return Ok(mappedItem);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] RegionCreateDto region)
        {
            // Converted Dto to Domain Model . 
            //Region regionDomain = new Region {
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl
            //};

            var regionDomain = mapper.Map<Region>(region);

            // I Used Domain Model To Create Region Record . 

            Region result = await _regionRepository.CreateRegionAsync(regionDomain);

            //var RegionDto = new RegionDto
            //{
            //    Id = result.Id,
            //    Code = result.Code,
            //    Name = result.Name,
            //    RegionImageUrl = result.RegionImageUrl
            //};

            var RegionDto = mapper.Map<RegionDto>(result);

            return CreatedAtAction(nameof(getRegionById), new { id = regionDomain.Id }, RegionDto);
        }

        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid Id, [FromBody] UpdateRegionDto regionDto)
        {
            // Checking existence of record with this id 

            var result = await _regionRepository.UpdateRegionAsync(Id ,regionDto );

            if (result == null)
            {
                return NotFound();
            }


            // Updating Region
            //existingRecord.Code = regionDto.Code;
            //existingRecord.Name = regionDto.Name;
            //existingRecord.RegionImageUrl = regionDto.RegionImageUrl;

            //await _dbContext.SaveChangesAsync();

            //var RegionUpdatedDto = new RegionDto
            //{
            //    Id = existingRecord.Id,
            //    Code = existingRecord.Code,
            //    Name = existingRecord.Name,
            //    RegionImageUrl = existingRecord.RegionImageUrl
            //};

            var RegionDto = mapper.Map<RegionDto>(result);

            return Ok(RegionDto);
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid Id)
        {
            //var region = await _dbContext.Regions.FirstOrDefaultAsync(region => region.Id == id );

            Region? region = await _regionRepository.DeleteRegionAsync(Id);

            if (region == null)
            {
                return NotFound();
            }

            //_dbContext.Regions.Remove(region);
            //await _dbContext.SaveChangesAsync();

            //var regionDto = new RegionDto {
            //    Id = region.Id,
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl
            //};

            var RegionDto = mapper.Map<RegionDto>(region);

            return Ok(RegionDto);
        }


    }
}
