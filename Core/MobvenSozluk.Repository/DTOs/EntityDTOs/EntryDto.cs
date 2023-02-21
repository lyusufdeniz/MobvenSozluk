using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Repository.DTOs
{
    public class EntryDto
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int UpVotes { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public int TitleId { get; set; }
    }
}
