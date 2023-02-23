using FluentValidation;
using MobvenSozluk.Repository.DTOs.EntityDTOs;

namespace MobvenSozluk.Infrastructure.Validations
{
public class UserDtoValidator : AbstractValidator<UserDto>
{
    {

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(8, 20).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
            .Matches("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&.])[A-Za-z\\d@$!%*#?&.]{8,}$").WithMessage("{PropertyName} must include minimum eight characters, at least one letter, one number and one special character");

    }
}