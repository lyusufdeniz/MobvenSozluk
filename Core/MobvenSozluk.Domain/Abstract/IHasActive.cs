using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Domain.Abstract
{
    public interface IHasActive
    {
        public bool IsActive { get; set; }
    }
}
