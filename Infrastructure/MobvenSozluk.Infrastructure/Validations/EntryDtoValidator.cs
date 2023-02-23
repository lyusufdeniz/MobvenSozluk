using FluentValidation;
using MobvenSozluk.Repository.DTOs.EntityDTOs;

namespace MobvenSozluk.Infrastructure.Validations
{
    public class EntryDtoValidator:AbstractValidator<EntryDto>
    {
        public EntryDtoValidator()
        {
            RuleFor(x => x.Body).NotNull().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Body).MinimumLength(8).WithMessage("{PropertyName}  length can't be less than {MinLength} characters");
            RuleFor(x => x.Body).MaximumLength(250).WithMessage("{PropertyName} length can't be more than {MaxLength} characters");

            RuleFor(x => x.TitleId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} is required");
            RuleFor(x => x.UserId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} is required");
        }

    }
}
