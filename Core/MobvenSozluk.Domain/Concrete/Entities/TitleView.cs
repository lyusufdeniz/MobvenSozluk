using MobvenSozluk.Domain.Abstract;

namespace MobvenSozluk.Domain.Concrete.Entities;

public class TitleView : IBaseEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string IpAddress { get; set; }
    public DateTime VisitDate { get; set; }

    public int TitleId { get; set; }
    public Title Title { get; set; }
    
}
