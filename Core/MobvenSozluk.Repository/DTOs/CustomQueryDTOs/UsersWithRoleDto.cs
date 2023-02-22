using MobvenSozluk.Repository.DTOs.EntityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Repository.DTOs.CustomQueryDTOs
{
    public class UsersWithRoleDto : UserDto
    {
        public RoleDto Role { get; set; }
    }
}
