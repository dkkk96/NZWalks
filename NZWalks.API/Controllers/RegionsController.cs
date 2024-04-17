using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilter;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRegionRepository regionRepository;
        private readonly ILogger logger;

        public RegionsController(IMapper mapper,IRegionRepository regionRepository, ILogger<RegionsController> logger)
        {
           
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.logger = logger;
        }
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("GetAll IAction Method of Region Controlled was invoked");
            var regions = await regionRepository.GetRegionsAsync();

            var regionDto = mapper.Map<List<RegionDto>>(regions);

            logger.LogInformation($"Finished GetAll Region Request with data: {JsonSerializer.Serialize(regionDto)}");

            return Ok(regionDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetId([FromRoute]Guid id) 
        {
            var region = await regionRepository.GetRegionAsync(id);
            if (region == null)
            {
                return NotFound();
            }


            var regionDto = mapper.Map<RegionDto>(region);

            return Ok(regionDto);
        }


        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            
                var region = mapper.Map<Region>(addRegionRequestDto);

                region = await regionRepository.CreateRegionAsync(region);

                var regionDto = mapper.Map<RegionDto>(region);
                return CreatedAtAction(nameof(GetId), new { id = regionDto.Id }, regionDto);

            
            
            
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id , [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            
            
                var region = await regionRepository.GetRegionAsync(id);
                if (region == null)
                {
                    return NotFound();
                }

                mapper.Map(updateRegionRequestDto, region);

                var updatedRegion = await regionRepository.UpdateRegionAsync(id, region);

                var regionDto = mapper.Map<RegionDto>(updatedRegion);

                return Ok(regionDto);

            
            
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var region = await regionRepository.DeleteRegionAsync(id);
            if (region == null)
            {
                return NotFound(id);
            }
            return Ok(mapper.Map<RegionDto>(region));
        }
    }
}






/*
Attributes Used:
1. ApiController : Tell the app that this controller is for the api use.
 */