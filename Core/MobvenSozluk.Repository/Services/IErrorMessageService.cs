using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Repository.Services
{
    public interface IErrorMessageService
    {
        string UserNotFound { get; }
        string UserNameOrPasswordWrong { get; }
        string InvalidCredentials { get; }
        string UserOrTokenNotFound { get; }
    }
}
