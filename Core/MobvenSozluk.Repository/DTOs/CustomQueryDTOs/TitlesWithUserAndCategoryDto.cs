using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Repository.DTOs.CustomQueryDTOs
{
    public class TitlesWithUserAndCategoryDto : TitleDto
    {
        public UserDto User { get; set; }
        public CategoryDto Category { get; set; }

    }
}
