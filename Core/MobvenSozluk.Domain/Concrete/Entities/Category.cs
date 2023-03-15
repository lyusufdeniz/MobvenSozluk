using MobvenSozluk.Domain.Abstract;
using MobvenSozluk.Domain.Attributes;

namespace MobvenSozluk.Domain.Concrete.Entities
{
    public class Category: IBaseEntity, IHasDeletable
    {
        [Sort]
        public int Id { get; set; }
        [Sort]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Title> Titles { get; set; }
    }
}
