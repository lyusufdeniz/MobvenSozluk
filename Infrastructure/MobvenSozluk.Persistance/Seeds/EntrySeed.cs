using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobvenSozluk.Domain.Concrete.Entities;

namespace MobvenSozluk.Persistance.Seeds
{
    internal class EntrySeed : IEntityTypeConfiguration<Entry>
    {
        public void Configure(EntityTypeBuilder<Entry> builder)
        {
            builder.HasData(new Entry { Id = 1, UserId = 3, Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas sed.",TitleId=1});
        }
    }
}
