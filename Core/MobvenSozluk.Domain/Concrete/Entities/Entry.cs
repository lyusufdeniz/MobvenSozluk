using MobvenSozluk.Domain.Abstract;
using MobvenSozluk.Domain.Attributes;

namespace MobvenSozluk.Domain.Concrete.Entities
{
    public class Entry: IBaseEntity, IHasCreatedDate, IHasActive, IHasDeletable
    {
        [Sort]
        public int Id { get; set; }
        [Search]
        public string Body { get; set; }
        [Sort]
        [Filter]
        public int UpVotes { get; set; }
        [Sort]
        [Filter]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }//isactive - IsActiVe
        public bool IsDeleted { get; set; }

        /* Related Entities */

        public int UserId { get; set; }
        public User User { get; set; }
        public int TitleId { get; set; }
        public Title Title { get; set; }

    }
}
