using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Domain.Concrete.Entities
{
    public class RoleWithUsers : Role
    {
        public List<User> Users { get; set; }
    }
}
