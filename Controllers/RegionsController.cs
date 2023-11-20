using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Services;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionService regionService;
        private readonly IMapper mapper;

        public RegionsController(IRegionService regionService, IMapper mapper)
        {
            this.regionService = regionService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await regionService.GetAllAsync();

            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegion")]
        public async Task<IActionResult> GetRegion(Guid id)
        {
            var region = await regionService.GetAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegion(Models.DTO.AddRegionRequest addRegionRequest)
        {
            var region = mapper.Map<Models.Domain.Region>(addRegionRequest);

            region = await regionService.AddAsync(region);

            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return CreatedAtAction(nameof(GetRegion), new { id = regionDTO.Id } , regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion
            ([FromRoute] Guid id, [FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            var region = mapper.Map<Models.Domain.Region>(updateRegionRequest);

            region = await regionService.UpdateAsync(id, region);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            var region = await regionService.DeleteAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);
        }
    }
}
