using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Domain.Abstract
{
    public interface IHasDeletable
    {
        public bool IsDeleted { get; set; }
    }
}
