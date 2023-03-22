using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Infrastructure.Exceptions
{
    public class NullException :Exception
    {
        public NullException(string message) : base(message) { }

    }
}
