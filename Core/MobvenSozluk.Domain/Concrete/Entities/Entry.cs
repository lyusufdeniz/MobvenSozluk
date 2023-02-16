using MobvenSozluk.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Domain.Concrete.Entities
{
    public class Entry : BaseEntity<int>
    {
        public string Body { get; set; }
        public int UpVotes { get; set; }

        /* Related Entities */
        public int UserId { get; set; }
        public User User { get; set; }
        public int TitleId { get; set; }
        public Title Title { get; set; }

    }
}
