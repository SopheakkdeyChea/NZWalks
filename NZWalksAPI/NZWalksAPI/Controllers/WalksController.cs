using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTO.Walks;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;
        public WalksController(IWalkRepository walkRepository, IMapper mapper) 
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllWalksAsync")]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            // Fecth data from database - domain walks
            var walks = await _walkRepository.GetAllAsync();

            // Convert domain walks to DTO
            var walksDTO = _mapper.Map<List<Walk>>(walks);
            
            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("GetWalkAsync/{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            // Get Data from database
            var walks = await _walkRepository.GetAsync(id);

            // convert Data to DTo
            var walksDTO = _mapper.Map<Walk>(walks);

            return Ok(walksDTO);
        }

        [HttpPost]
        [Route("AddWalkAsync")]
        public async Task<IActionResult> AddWalkAsync([FromBody] AddWalkRequest addWalkRequest)
        {
            // Request(DTO) to Domain Model
            var walk = new Models.Domain.Walk()
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId,
            };

            walk  = await _walkRepository.AddAsync(walk);

            var walkDTO = new Models.DTO.Walks.Walk
            {
                Id = walk.Id,
                Length = walk.Length,
                Name = walk.Name,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId,
            };

            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpPut]
        [Route("UpdateWalkAsync/{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, 
            [FromBody] UpdateWalkRequest updateWalkRequest)
        {
            // Convert DTO to Domain object
            var walk = new Models.Domain.Walk()
            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId,
            };

            // Pass details to repository - Get Domain object  in response (or null)
            walk = await _walkRepository.UpdateAsync(id, walk);

            // Handle Null (not found)
            if (walk == null)
            {
                return NotFound();
            }

            // Convert back Domain to DTO
            var walkDTO = new Models.DTO.Walks.Walk
            {
                Id = walk.Id,
                Length = walk.Length,
                Name = walk.Name,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId,
            };

            // Return respone
            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("DeleteWalkAsync/{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            var walk = await _walkRepository.DeleteAsync(id);

            if (walk == null)
            {
                return NotFound();
            }

            var walkDTO = _mapper.Map < Models.DTO.Walks.Walk>(walk);
     
            return Ok(walkDTO);
        }
    }
}
