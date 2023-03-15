using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Domain.Attributes
{ /// <summary>
/// Arama yapılacak alanlar için kullanılacak attribute
/// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchAttribute:Attribute
    {
    }
}
