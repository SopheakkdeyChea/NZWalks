﻿using FluentValidation;
using NZWalksAPI.Models.DTO.WalkDifficulties;

namespace NZWalksAPI.Validators
{
    public class UpdateWalkDifficultyRequestValidator : AbstractValidator<UpdateWalkDifficultyRequest>
    {
        public UpdateWalkDifficultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
