using MobvenSozluk.Domain.Concrete.Entities;
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
        #region CODE EXPLANATION SECTION
        /*
          Create Login and Register methods to handle user access 
          Create RefreshToken method to handle tokens
         */
        #endregion
        Task<CustomResponseDto<UserDtoWithToken>> Login(LoginDto loginDto);
        Task<CustomResponseDto<UserDtoWithToken>> Register(RegisterDto registerDto);
        Task<CustomResponseDto<UserDtoWithToken>> RefreshToken(RefreshTokenDto token);
    }
}
