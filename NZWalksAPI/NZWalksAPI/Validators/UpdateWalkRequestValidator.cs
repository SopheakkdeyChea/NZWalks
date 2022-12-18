using FluentValidation;
using NZWalksAPI.Models.DTO.Walks;

namespace NZWalksAPI.Validators
{
    public class UpdateWalkRequestValidator : AbstractValidator<UpdateWalkRequest>
    {
        public UpdateWalkRequestValidator() 
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}
