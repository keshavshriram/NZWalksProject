using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks_API.Models.Domain;
using NZWalks_API.Models.DTO;
using NZWalks_API.Repositories;

namespace NZWalks_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalksRepository _walksRepository;
        public WalksController( IMapper mapper , IWalksRepository walksRepository)
        { 
            _mapper = mapper;
            _walksRepository = walksRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWalk( [FromBody] WalkCreateDto createWalk)
        {
            if (ModelState.IsValid)
            {
                var walkDomain = _mapper.Map<Walk>(createWalk);
                var result = await _walksRepository.CreateWalksAsync(walkDomain);

                if (result == null)
                {
                    return NotFound();
                }

                var response = _mapper.Map<WalksDto>(result);

                return Ok(response);
            }
            else
            {
                return BadRequest(ModelState);
            }
            
            

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Walk> walksListDomain = await _walksRepository.GetAllAsync();
            List<WalksDto> walksListDto = _mapper.Map<List<WalksDto>>(walksListDomain);
            return Ok(walksListDto);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetWakById([FromRoute] Guid Id)
        {
            Walk? walkDomain = await _walksRepository.GetWalkByIdAsync(Id);
            if (walkDomain == null)
            {
                return NotFound();
            }

            WalksDto walkDto = _mapper.Map<WalksDto>(walkDomain);
            return Ok(walkDto);
        }

        [HttpPut]
        [Route("{Id}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid Id , [FromBody] UpdateWalkDto walk)
        {
            if (ModelState.IsValid)
            {
                Walk? walkDomain = await _walksRepository.UpdateWalkAsync(Id, walk);
                WalksDto? walkDto = _mapper.Map<WalksDto?>(walkDomain);
                return Ok(walkDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid Id )
        {
            Walk? walk = await _walksRepository.DeleteWalkAsync(Id);
            WalksDto? WalkDto = _mapper.Map<WalksDto?>(walk);

            return Ok(WalkDto);
        }


    }
}
