using FluentValidation;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Infrastructure.Validations
{
    public class TitleDtoValidator : AbstractValidator<TitleDto>
    {
        public TitleDtoValidator()
        {
           
        }
    }
}
