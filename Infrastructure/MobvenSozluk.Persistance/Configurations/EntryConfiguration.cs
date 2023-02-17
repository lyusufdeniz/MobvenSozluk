using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobvenSozluk.Domain.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Persistance.Configurations
{
    internal class EntryConfiguration : IEntityTypeConfiguration<Entry>
    {
        public void Configure(EntityTypeBuilder<Entry> builder)
        {
            builder.Property(x => x.Body).IsRequired();
            builder.HasOne(x => x.User).WithMany(x => x.Entries).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Title).WithMany(x => x.Entries);
        }
    }
}
