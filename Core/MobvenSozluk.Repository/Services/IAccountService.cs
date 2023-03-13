using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Repository.Services
{
    public interface IAccountService
    {
        Task<CustomResponseDto<UserDtoWithToken>> Login(LoginDto loginDto);
        Task<CustomResponseDto<UserDtoWithToken>> Register(RegisterDto registerDto);
    }
}
