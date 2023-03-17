using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Domain.Attributes
{
    /// <summary>
    /// Filtrelenebilir Alanlar İçin Kullanılacak Attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FilterAttribute:Attribute
    {
    }
}
