using AutoMapper;
using LoggerService.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Classes;
using NZWalksAPI.Models.DTO.Regions;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerRepository _logger;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper, 
            IWalkDifficultyRepository walkDifficultyRepository, ILoggerRepository loggerRepository)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
            _walkDifficultyRepository = walkDifficultyRepository;
            _logger = loggerRepository;
        }

        [HttpGet]
        [Route("GetAllRegionsAsync")]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await _regionRepository.GetAllAsync();
            #region option 1
            // (Option 1 without Automapper)
            // Return DTO regions
            //var regionsDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO =  new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name= region.Name,
            //        Area= region.Area,
            //        Lat= region.Lat,
            //        Long= region.Long,
            //        Population= region.Population,
            //    };
            //    regionsDTO.Add(regionDTO);
            //});
            #endregion
            // (Option 2 with Automapper)
            var regionsDTO = _mapper.Map<List<Region>>(regions);

            _logger.LogInfo("test");
            return Ok(regionsDTO); 
        }

        [HttpGet]
        [Route("GetRegionAsync/{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id) 
        {
            var regions = await _regionRepository.GetAsync(id);
            
            if (regions == null)
            {
                return NotFound();
            }

            var regionDTO = _mapper.Map<Region>(regions);
            return Ok(regionDTO);
        }

        [HttpPost]
        [Route("AddRegionAsync")]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest addRegionRequest)
        {
            // Option Validation 1 or Using Fluent Validator
            //var validate = new ValidationObjects(_regionRepository, _walkDifficultyRepository);
            //// Validate The Request
            //if (!validate.ValidateAddRegionAsync(addRegionRequest))
            //{
            //    return BadRequest(validate.ModelState);
            //}

            // Request(DTO) to Domain Model
            var region = new Models.Domain.Region()
            {
                Code= addRegionRequest.Code,
                Area= addRegionRequest.Area,
                Lat= addRegionRequest.Lat,
                Long= addRegionRequest.Long,
                Name= addRegionRequest.Name,
                Population= addRegionRequest.Population,
            };

            // Pass Detail to Repository
            region = await _regionRepository.AddAsync(region);

            // Covert back to DTO
            var regionDTO = new Models.DTO.Regions.Region()
            {
                Id=region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population,
            };

            return CreatedAtAction(nameof(GetRegionAsync), new {id = regionDTO.Id}, regionDTO);
        }

        [HttpDelete]
        [Route("DeleteRegionAsync/{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            // Get region from DB
            var region = await _regionRepository.DeleteAsync(id);

            //If null Notfound
            if (region == null)
            {
                return NotFound();
            }

            //Convert response back to DTTO
            var regionDTO = new Models.DTO.Regions.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population,
            };

            // Return Ok
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("UpdateRegionAsync/{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequest updateRegionRequest)
        {
            // Option Validation 1 or Using Fluent Validator
            //var validate = new ValidationObjects(_regionRepository, _walkDifficultyRepository);
            //// Validate The Request
            //if (!validate.ValidateUpdateRegionAsync(updateRegionRequest))
            //{
            //    return BadRequest(validate.ModelState);
            //}

            // Convert Model to Domain Model
            var region = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population,
            };

            // Update Region using repository
            await _regionRepository.UpdateAsync(id, region);

            // If null then NotFound
            if (region == null)
            {
                return NotFound();
            }

            // Convert Domain Back to DTO
            var regionDTO = new Models.DTO.Regions.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population,
            };

            // return Ok response
            return Ok(regionDTO);
        }
    }
}
