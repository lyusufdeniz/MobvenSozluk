using MobvenSozluk.Domain.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Repository.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
        Task SetRefreshToken(RefreshToken refreshToken, User user);
        RefreshToken CreateRefreshToken();
        string ValidateToken(string token);
    }
}
