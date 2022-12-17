using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTO.WalkDifficulties;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkDifficultiesController : ControllerBase
    {
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;
        private readonly IMapper _mapper;

        public WalkDifficultiesController(IWalkDifficultyRepository walkDifficultyRepository, 
            IMapper mapper) 
        { 
            _walkDifficultyRepository = walkDifficultyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllWalkDifficultyAsync")]
        public async Task<IActionResult> GetAllWalkDifficultyAsync()
        {
            var walkDifficulty = await _walkDifficultyRepository.GetAllAsync();

            if (walkDifficulty == null)
            {
                return NotFound();
            }

            var walkDifficultyDTO = _mapper.Map<List<WalkDifficulty>>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        }

        [HttpGet]
        [Route("GetWalkDifficultyAsync/{id:guid}")]
        [ActionName("GetWalkDifficultyAsync")]
        public async Task<IActionResult> GetWalkDifficultyAsync(Guid id)
        {
            var walkDifficulty = await _walkDifficultyRepository.GetAsync(id);

            if (walkDifficulty == null)
            {
                return NotFound();
            }

            var walkDifficultyDTO = _mapper.Map<WalkDifficulty>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        }

        [HttpPost]
        [Route("AddWalkDifficultyAsync")]
        public async Task<IActionResult> AddWalkDifficultyAsync(AddWalkDifficultyReuest addWalkDifficultyReuest)
        {
            var walkDifficulty = new Models.Domain.WalkDifficulty()
            {
                Code = addWalkDifficultyReuest.Code
            };

            walkDifficulty = await _walkDifficultyRepository.AddAsync(walkDifficulty);

            var walkDifficultyDTO = _mapper.Map<WalkDifficulty>(walkDifficulty);

            return CreatedAtAction(nameof(GetWalkDifficultyAsync), new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);
        }

        [HttpPut]
        [Route("UpdateWalkDifficultyAsync/{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync(Guid id, UpdateWalkDifficultyReuest updateWalkDifficultyReuest)
        {
            var walkDifficulty = new Models.Domain.WalkDifficulty()
            {
                Code = updateWalkDifficultyReuest.Code,
            };

            walkDifficulty = await _walkDifficultyRepository.UpdateAsync(id, walkDifficulty);

            if (walkDifficulty == null) { return NotFound();}

            var walkDifficultyDTO = _mapper.Map<WalkDifficulty>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        }

        [HttpDelete]
        [Route("DeleteWalkDifficultyAsync/{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            var walkDifficulty = await _walkDifficultyRepository.DeleteAsync(id);

            if (walkDifficulty == null)
            {
                return NotFound();
            }

            var walkDifficultyDTO = _mapper.Map<WalkDifficulty>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        } 
    }
}
