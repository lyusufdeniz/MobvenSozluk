using MobvenSozluk.Repository.DTOs.EntityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Repository.DTOs.CustomQueryDTOs
{
    public class EntriesWithUserAndTitleDto : EntryDto
    {
        public UserDto User { get; set; }
        public TitleDto Title { get; set; }

    }
}
