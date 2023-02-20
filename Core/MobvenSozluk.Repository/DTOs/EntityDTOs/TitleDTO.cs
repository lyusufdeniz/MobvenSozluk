namespace MobvenSozluk.Repository.DTOs.EntityDTOs
{
    public class TitleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UpVotes { get; set; }
        public int Views { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }

    }
}
