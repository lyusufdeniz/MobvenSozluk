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

        public string NotFoundMessage<T>()
        {
            return $"{typeof(T).Name} not found";
        }
        public string UserNameOrPasswordWrong => "User name or password wrong";
        public string UserOrTokenNotFound => "User not found or token expired";
        public string BadRequestDescription => "An error occurred while requesting the API";
        public string UserAlreadyExist => "User already exist";
        public string RoleAlreadyExist => "Role already exist";
        public string RoleNotExist => "There is no such role name exist";
        public string AlreadyLogin => "You cannot log in cuz you are already logged in";
        public string AlreadyLogout => "You cannot log out cuzz you are already logged out";
    }
}
