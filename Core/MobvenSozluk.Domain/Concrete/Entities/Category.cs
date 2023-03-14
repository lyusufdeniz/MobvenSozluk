using MobvenSozluk.Domain.Abstract;
using MobvenSozluk.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Domain.Concrete.Entities
{
    public class Category: IBaseEntity, IHasDeletable
    {
        [SortAttribute]
        public int Id { get; set; }
        [SortAttribute]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Title> Titles { get; set; }
    }
}
