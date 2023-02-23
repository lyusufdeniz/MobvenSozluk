using FluentValidation;
using MobvenSozluk.Repository.DTOs.EntityDTOs;

namespace MobvenSozluk.Infrastructure.Validations;

public class CategoryDtoValidator : AbstractValidator<CategoryDto>
{
    public CategoryDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(3, 15).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
            .Matches("^[a-zA-Z]+$").WithMessage("{PropertyName} includes letters only.");
    }
}