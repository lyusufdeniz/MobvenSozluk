using FluentValidation;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Infrastructure.Validations
{
    public class LoginDtoValidator : AbstractValidator<RegisterDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .EmailAddress().WithMessage("Please enter a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Length(8, 20).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
                .Matches("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&.])[A-Za-z\\d@$!%*#?&.]{8,}$").WithMessage("{PropertyName} must include minimum eight characters, at least one letter, one number and one special character");
        }
    }
}
