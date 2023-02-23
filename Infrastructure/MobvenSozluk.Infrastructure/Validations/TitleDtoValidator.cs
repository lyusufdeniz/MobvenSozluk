using FluentValidation;
using MobvenSozluk.Repository.DTOs.EntityDTOs;

namespace MobvenSozluk.Infrastructure.Validations;


public class TitleDtoValidator : AbstractValidator<TitleDto>
{
    public TitleDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(2, 30).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}");

        RuleFor(x => x.CategoryId)
            .InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(x => x.CategoryId)
            .InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 0.");
    }
}
