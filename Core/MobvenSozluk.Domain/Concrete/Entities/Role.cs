using MobvenSozluk.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Domain.Concrete.Entities
{
    public class Role: IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        /* Related Entities */
        public ICollection<User> Users { get; set; }

    }
}
