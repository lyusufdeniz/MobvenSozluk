using Microsoft.AspNetCore.Identity;
using MobvenSozluk.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Domain.Concrete.Entities
{
    public class Role: IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
