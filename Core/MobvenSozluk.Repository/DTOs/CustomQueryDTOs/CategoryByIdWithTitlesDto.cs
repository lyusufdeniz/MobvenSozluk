using MobvenSozluk.Repository.DTOs.EntityDTOs;

namespace MobvenSozluk.Repository.DTOs.CustomQueryDTOs
{
    public class CategoryByIdWithTitlesDto : CategoryDto
    {
        public List<TitleDto> Titles { get; set; }
    }
}
