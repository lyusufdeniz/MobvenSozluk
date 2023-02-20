namespace MobvenSozluk.Repository.DTOs.EntityDTOs
{
    public class EntryDTO
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int UpVotes { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public int TitleId { get; set; }
    }
}
