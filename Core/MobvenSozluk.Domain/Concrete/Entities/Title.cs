using MobvenSozluk.Domain.Abstract;
using MobvenSozluk.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Domain.Concrete.Entities
{
    public class Title: IBaseEntity, IHasCreatedDate, IHasActive, IHasDeletable
    {
        [Sort]
        public int Id { get; set; }
        [Sort]  [Search]
        public string Name { get; set; }
        [Sort] [Filter] 
        public int UpVotes { get; set; }
        [Sort] [Filter]
        public int Views { get; set; }
        [Sort] [Filter]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }

        /* Related Entities */
        public ICollection<Entry> Entries { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
