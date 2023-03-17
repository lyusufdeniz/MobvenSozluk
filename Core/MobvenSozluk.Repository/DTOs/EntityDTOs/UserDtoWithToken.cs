using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Repository.DTOs.EntityDTOs
{
    public class UserDtoWithToken
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string? RefreshToken { get; set; } 
    }
}
