using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Domain.Concrete.Entities
{
    #region CODE EXPLANATION SECTION
    /*
      Created Join table to manage roles and users easily
     */
    #endregion
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
