﻿using FluentValidation;
using NZWalksAPI.Models.DTO.Walks;

namespace NZWalksAPI.Validators
{
    public class AddWalkRequestValidator : AbstractValidator<AddWalkRequest>
    {
        public AddWalkRequestValidator() 
        { 
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}
