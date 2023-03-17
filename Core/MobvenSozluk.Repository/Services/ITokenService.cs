﻿using MobvenSozluk.Domain.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Repository.Services
{
    public interface ITokenService
    {
        #region CODE EXPLANATION SECTION
        /*
          Create Token task which is returning string because program needs tokens as a string 
         */
        #endregion
        Task<string> CreateToken(User user);
    }
}