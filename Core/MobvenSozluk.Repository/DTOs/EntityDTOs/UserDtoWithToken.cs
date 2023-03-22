using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MobvenSozluk.Repository.DTOs.EntityDTOs
{
    public class UserDtoWithToken
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string RefreshToken { get; set; } 
    }
}
