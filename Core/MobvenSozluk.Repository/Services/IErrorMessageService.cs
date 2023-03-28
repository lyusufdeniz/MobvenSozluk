using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Repository.Services
{
    public interface IErrorMessageService
    {
        string NotFoundMessage<T>();
        string UserNameOrPasswordWrong { get; }
        string UserOrTokenNotFound { get; }
        string BadRequestDescription { get; }
        string UserAlreadyExist { get; }
        string RoleAlreadyExist { get; }
        string RoleNotExist { get; }
        string AlreadyLogin { get; }
        string AlreadyLogout { get; }
    }
}
