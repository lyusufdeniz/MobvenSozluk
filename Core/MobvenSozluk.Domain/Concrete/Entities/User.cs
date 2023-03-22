using Microsoft.AspNetCore.Identity;
using MobvenSozluk.Domain.Abstract;
using MobvenSozluk.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Domain.Concrete.Entities
{
    public class User : IdentityUser<int>, IHasActive, IHasCreatedDate, IHasDeletable
    {
        //ICreatable
        //Fluent Validation
        [Sort]
        [Search]
        public override string UserName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }

        /* Related Entities */
        public  ICollection<Title> Titles { get; set; }
        public  ICollection<Entry> Entries { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

        /* Refresh Token Part */
        public string? RefreshToken { get; set; } = string.Empty;
        public DateTime? RefreshTokenCreated { get; set; } 
        public DateTime? RefreshTokenExpires { get; set; }
    }
}
