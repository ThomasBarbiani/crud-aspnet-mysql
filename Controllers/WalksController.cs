using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Services;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkService walkService;
        private readonly IMapper mapper;

        public WalksController(IWalkService walkService, IMapper mapper)
        {
            this.walkService = walkService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walksDomain = await walkService.GetAllAsync();

            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walksDomain);

            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalk")]
        public async Task<IActionResult> GetWalk(Guid id)
        {
            var walkDomain = await walkService.GetAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            var walksDTO = mapper.Map<Models.DTO.Walk>(walkDomain);

            return Ok(walksDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalk([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            var walkDomain = mapper.Map<Models.Domain.Walk>(addWalkRequest);

            walkDomain = await walkService.AddAsync(walkDomain);

            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);

            return CreatedAtAction(nameof(GetWalk), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalk
            ([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            var walkDomain = mapper.Map<Models.Domain.Walk>(updateWalkRequest);

            walkDomain = await walkService.UpdateAsync(id, walkDomain);

            if (walkDomain == null)
            {
                return NotFound();
            }

            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);

            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            var walkDomain = await walkService.DeleteAsync(id);

            if (walkDomain == null) 
            {
                return NotFound();
            }

            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);

            return Ok(walkDTO);
        }
    }
}
