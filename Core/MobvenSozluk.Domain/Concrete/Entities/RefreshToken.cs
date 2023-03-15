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
          With the help of Encapsulation principle, The properties Token, Created, and Expires are defined within the class and are inaccessible from outside.
          This ensures that the values of these properties are set correctly and hides the internal logic of the class.
          This prevents external code from directly modifying or reading the property values while enabling direct access to these properties from within the class, 
               thereby enhancing the readability and maintainability of the code.
          
         */
        #endregion
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; } 
        public DateTime Expires { get; set; }

        public RefreshToken()
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            Expires = DateTime.UtcNow.AddDays(2);
            Created = DateTime.UtcNow;

        }
    }
}
