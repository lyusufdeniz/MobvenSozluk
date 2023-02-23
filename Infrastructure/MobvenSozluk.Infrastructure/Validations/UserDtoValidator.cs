using FluentValidation;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Infrastructure.Validations;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public  UserDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(3, 15).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
            .Matches("^[a-zA-Z0-9_]*$").WithMessage("{PropertyName} only includes alphanumeric characters.");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .EmailAddress().WithMessage("Please enter a valid email address.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(8, 20).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
            .Matches("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&.])[A-Za-z\\d@$!%*#?&.]{8,}$").WithMessage("{PropertyName} must include minimum eight characters, at least one letter, one number and one special character");

        RuleFor(x => x.RoleId)
            .InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 1.");
    }
}