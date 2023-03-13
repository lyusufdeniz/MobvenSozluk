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
      As you seen here user inherited from IdentityUser that means now in this project, we can manage users as an identity user.
     */
    #endregion
    public class User : IdentityUser<int>, IHasActive, IHasCreatedDate, IHasDeletable
    {
        //ICreatable
        //Fluent Validation
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }

        /* Related Entities */
        public  ICollection<Title> Titles { get; set; }
        public  ICollection<Entry> Entries { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
