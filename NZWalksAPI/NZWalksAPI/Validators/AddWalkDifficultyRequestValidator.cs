﻿using FluentValidation;
using NZWalksAPI.Models.DTO.WalkDifficulties;

namespace NZWalksAPI.Validators
{
    public class AddWalkDifficultyRequestValidator : AbstractValidator<AddWalkDifficultyRequest>
    {
        public AddWalkDifficultyRequestValidator() 
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
