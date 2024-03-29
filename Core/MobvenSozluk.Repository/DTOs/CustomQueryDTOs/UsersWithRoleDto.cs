﻿using MobvenSozluk.Repository.DTOs.EntityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Repository.DTOs.CustomQueryDTOs
{
    public class UsersWithRoleDto 
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IList<RoleDto> Roles { get; set; }

    }
}
