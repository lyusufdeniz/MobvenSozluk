using FluentValidation;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Infrastructure.Validations;

public class RoleDtoValidator : AbstractValidator<RoleDto>
{
    public RoleDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(3, 15).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
            .Matches("^[A-Za-z]+$").WithMessage("{PropertyName} includes letters only.");
    }
    
}