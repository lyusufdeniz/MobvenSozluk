
using MobvenSozluk.Repository.DTOs.EntityDTOs;

namespace MobvenSozluk.Repository.DTOs.CustomQueryDTOs
{
    public class UserByIdWithTitlesDto:UserDto
    {
        public List<TitleDto> Titles { get; set; }
    }
}
