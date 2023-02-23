using FluentValidation;
using MobvenSozluk.Repository.DTOs.EntityDTOs;

namespace MobvenSozluk.Infrastructure.Validations;

public class EntryDtoValidator : AbstractValidator<EntryDto>
{
    public EntryDtoValidator()
    {
        RuleFor(x => x.Body)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(8, 1000).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}");

        RuleFor(x => x.TitleId)
            .InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(x => x.UserId)
            .InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 0.");
        
    }
}