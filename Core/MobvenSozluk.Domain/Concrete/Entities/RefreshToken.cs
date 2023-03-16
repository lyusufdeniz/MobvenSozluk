﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Domain.Concrete.Entities
{
    public class RefreshToken
    {
        #region CODE EXPLANATION SECTION 1
        /*
          The properties Token, Created, and Expires are defined.
          
         */
        #endregion
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; } 
        public DateTime Expires { get; set; }
    }
}
