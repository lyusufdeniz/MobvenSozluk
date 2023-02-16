using MobvenSozluk.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Domain.Concrete.Entities
{
    public class User: BaseEntity<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        /* Related Entities */
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
