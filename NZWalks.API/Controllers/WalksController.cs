using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilter;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper , IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn , [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber =1, [FromQuery] int pageSize = 1000 )
        {
            throw new Exception("Something went wrong");
            var walks = await walkRepository.GetWalksAsync(filterOn,filterQuery,sortBy,isAscending?? true,pageNumber,pageSize);
            return Ok(mapper.Map<List<WalkDto>>(walks));

            
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walk = await walkRepository.GetWalkAsync(id);
            if (walk == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walk));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            
                var walk = await walkRepository.UpdateWalkAsync(id, mapper.Map<Walk>(updateWalkRequestDto));
                if (walk == null)
                    return NotFound();
                return Ok(mapper.Map<WalkDto>(walk));

            
            
            
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            
                var walk = mapper.Map<Walk>(addWalkRequestDto);

                await walkRepository.CreateWalkAsync(walk);

                var walkDto = mapper.Map<WalkDto>(walk);
                return CreatedAtAction(nameof(GetById), new { id = walkDto.Id }, walkDto);

            
            
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walk = await walkRepository.DeleteWalkAsync(id);
            if (walk == null) return NotFound();

            return Ok(mapper.Map<WalkDto>(walk));
        }


    }
}
