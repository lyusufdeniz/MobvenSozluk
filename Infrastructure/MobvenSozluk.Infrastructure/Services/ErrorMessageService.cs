using MobvenSozluk.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Infrastructure.Services
{
    public class ErrorMessageService : IErrorMessageService
    {
        public string UserNotFound => "User not found";
        public string InvalidCredentials => "Invalid user name or password";
        public string UserNameOrPasswordWrong => "User name or password wrong";

        public string UserOrTokenNotFound => "User not found or token expired";
    }
}
