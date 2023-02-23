using FluentValidation;
using MobvenSozluk.Repository.DTOs.EntityDTOs;

namespace MobvenSozluk.Infrastructure.Validations
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            //Name Validators
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("{PropertyName}  length can't be less than {MinLength} characters");
            RuleFor(x => x.Name).MaximumLength(15).WithMessage("{PropertyName} length can't be more than {MaxLength} characters");
            //Email Validators
            RuleFor(x => x.Email).NotNull().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("{PropertyName} is not valid e mail address");
            //Password Validators
            RuleFor(x => x.Password).NotNull().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("{PropertyName} length can't be less than {MinLength} characters");
            RuleFor(x => x.Password).MaximumLength(20).WithMessage("{PropertyName} length can't be more than {MaxLength} characters");
            //Role Validators
            RuleFor(x => x.RoleId).InclusiveBetween(1,int.MaxValue).WithMessage("{PropertyName} is required");


        }
    }
}
