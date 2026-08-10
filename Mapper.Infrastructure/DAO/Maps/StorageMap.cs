using Mapper.Domain.Entities.Machine;
using Mapper.Domain.Entities.Machine.ComputerParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Mapper.Infrastructure.DAO.Maps
{
    public class StorageMap : IEntityTypeConfiguration<Storage>
    {
        public void Configure(EntityTypeBuilder<Storage> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x=> x.Computer)
                .WithMany(p=> p.Storages)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Create_at).IsRequired();
            builder.Property(x=> x.Update_at).IsRequired();
            builder.Property(x => x.Active).IsRequired();

        }
    }
}
