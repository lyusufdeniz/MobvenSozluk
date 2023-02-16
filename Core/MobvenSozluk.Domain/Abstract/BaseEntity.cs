using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Domain.Abstract
{
    public class BaseEntity<T>
    {
        [Key] // sorulacak...
        public T Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } 
        public bool IsActive { get; set; } = true;
    }
}
