using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Domain.Abstract
{
    public interface IHasCreatedDate
    {
        public DateTime CreatedDate { get; set; } 
        public DateTime UpdatedDate { get; set; } 

    }
}
