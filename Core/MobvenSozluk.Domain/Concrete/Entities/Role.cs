using Microsoft.AspNetCore.Identity;
using MobvenSozluk.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Domain.Concrete.Entities
{
    #region CODE EXPLANATION SECTION
    /*
      As you seen here user inherited from IdentityUser that means now in this project, we can manage roles as an identity roles.
     */
    #endregion
    public class Role: IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
