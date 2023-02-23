using FluentValidation;
using MobvenSozluk.Repository.DTOs.EntityDTOs;

namespace MobvenSozluk.Infrastructure.Validations
{
    public class RoleDtoValidator:AbstractValidator<RoleDto>
    {
        public RoleDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("{PropertyName}  length can't be less than {MinLength} characters");
            RuleFor(x => x.Name).MaximumLength(10).WithMessage("{PropertyName} length can't be more than {MaxLength} characters");

      
        }
    }
}
