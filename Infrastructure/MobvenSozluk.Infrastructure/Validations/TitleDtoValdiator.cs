using FluentValidation;
using MobvenSozluk.Repository.DTOs.EntityDTOs;

namespace MobvenSozluk.Infrastructure.Validations
{
    public class TitleDtoValdiator:AbstractValidator<TitleDto>
    {
        public TitleDtoValdiator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Name).MinimumLength(8).WithMessage("{PropertyName}  length can't be less than {MinLength} characters");
            RuleFor(x => x.Name).MaximumLength(30).WithMessage("{PropertyName} length can't be more than {MaxLength} characters");

            RuleFor(x => x.CategoryId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} is required");
            RuleFor(x => x.UserId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} is required");
         
            
        }
    }
}
