using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTO.Regions;
using NZWalksAPI.Models.DTO.WalkDifficulties;
using NZWalksAPI.Models.DTO.Walks;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Classes
{
    public class ValidationObjects: ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;
        public ValidationObjects(IRegionRepository regionRepository, IWalkDifficultyRepository walkDifficultyRepository)
        {
            _regionRepository = regionRepository;
            _walkDifficultyRepository = walkDifficultyRepository;
        }

        #region Region
        public bool ValidateAddRegionAsync(AddRegionRequest addRegionRequest)
        {
            if (addRegionRequest == null)
            {
                ModelState.AddModelError(nameof(addRegionRequest),
                    $"Add Region Data is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Code),
                    $"{nameof(addRegionRequest.Code)} cannot be null or empty or white space.");
            }

            if (string.IsNullOrWhiteSpace(addRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Name),
                    $"{nameof(addRegionRequest.Name)} cannot be null or empty or white space.");
            }

            if (addRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Area),
                    $"{nameof(addRegionRequest.Area)} cannot be less or equal or zero.");
            }

            if (addRegionRequest.Lat <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Lat),
                    $"{nameof(addRegionRequest.Lat)} cannot be less or equal or zero.");
            }

            if (addRegionRequest.Long <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Long),
                    $"{nameof(addRegionRequest.Long)} cannot be less or equal or zero.");
            }

            if (addRegionRequest.Population <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Population),
                    $"{nameof(addRegionRequest.Population)} cannot be less or equal or zero.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        public bool ValidateUpdateRegionAsync(UpdateRegionRequest updateRegionRequest)
        {
            if (updateRegionRequest == null)
            {
                ModelState.AddModelError(nameof(updateRegionRequest),
                    $"Add Region Data is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Code),
                    $"{nameof(updateRegionRequest.Code)} cannot be null or empty or white space.");
            }

            if (string.IsNullOrWhiteSpace(updateRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Name),
                    $"{nameof(updateRegionRequest.Name)} cannot be null or empty or white space.");
            }

            if (updateRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Area),
                    $"{nameof(updateRegionRequest.Area)} cannot be less or equal or zero.");
            }

            if (updateRegionRequest.Lat <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Lat),
                    $"{nameof(updateRegionRequest.Lat)} cannot be less or equal or zero.");
            }

            if (updateRegionRequest.Long <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Long),
                    $"{nameof(updateRegionRequest.Long)} cannot be less or equal or zero.");
            }

            if (updateRegionRequest.Population <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Population),
                    $"{nameof(updateRegionRequest.Population)} cannot be less or equal or zero.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Walk
        public async Task<bool> ValidateAddWalkAsync(AddWalkRequest addWalkRequest)
        {
            //if(addWalkRequest == null)
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest),
            //        $"Add Walk Data is required.");
            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(addWalkRequest.Name))
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest.Name), 
            //        $"{nameof(addWalkRequest.Name)} cannot be empty.");
            //}

            //if (addWalkRequest.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest.Length),
            //        $"{nameof(addWalkRequest.Length)} cannot be less or equal than zero.");
            //}

            var region = await _regionRepository.GetAsync(addWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.RegionId),
                    $"{nameof(addWalkRequest.RegionId)} is invalid.");
            }

            var walkDifficulty = await _walkDifficultyRepository.GetAsync(addWalkRequest.WalkDifficultyId);
            if(walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.WalkDifficultyId),
                    $"{nameof(addWalkRequest.WalkDifficultyId)} is invalid.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ValidateUpdateWalkAsync(UpdateWalkRequest updateWalkRequest)
        {
            //if (updateWalkRequest == null)
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest),
            //        $"Add Walk Data is required.");
            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(updateWalkRequest.Name))
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest.Name),
            //        $"{nameof(updateWalkRequest.Name)} cannot be empty.");
            //}

            //if (updateWalkRequest.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest.Length),
            //        $"{nameof(updateWalkRequest.Length)} cannot be less or equal than zero.");
            //}

            var region = await _regionRepository.GetAsync(updateWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.RegionId),
                    $"{nameof(updateWalkRequest.RegionId)} is invalid.");
            }

            var walkDifficulty = await _walkDifficultyRepository.GetAsync(updateWalkRequest.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.WalkDifficultyId),
                    $"{nameof(updateWalkRequest.WalkDifficultyId)} is invalid.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region WalkDifficulty
        public bool ValidateAddWalkDifficultyAsync(AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            if (addWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest),
                    $"{nameof(addWalkDifficultyRequest)} is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest.Code),
                    $"{nameof(addWalkDifficultyRequest.Code)} is required.");
            }

            if(ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        public bool ValidateUpdateWalkDifficultyAsync(UpdateWalkDifficultyRequest updateWalkDifficultyReuest)
        {
            if (updateWalkDifficultyReuest == null)
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyReuest),
                    $"{nameof(updateWalkDifficultyReuest)} is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateWalkDifficultyReuest.Code))
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyReuest.Code),
                    $"{nameof(updateWalkDifficultyReuest.Code)} is required.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
