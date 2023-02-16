using MobvenSozluk.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Domain.Concrete.Entities
{
    public class Title: BaseEntity<int>
    {
        public string Name { get; set; }
        public string Body { get; set; }
        public int UpVotes { get; set; }
        public int Views { get; set; }

        /* Related Entities */
        public int UserId { get; set; }
        public User User { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
