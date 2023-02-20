using MobvenSozluk.Domain.Abstract;

namespace MobvenSozluk.Domain.Concrete.Entities
{
    public class Entry: IBaseEntity, IHasCreatedDate, IHasActive, IHasDeletable
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int UpVotes { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        /* Related Entities */
        public int UserId { get; set; }
        public User User { get; set; }
        public int TitleId { get; set; }
        public Title Title { get; set; }

    }
}
