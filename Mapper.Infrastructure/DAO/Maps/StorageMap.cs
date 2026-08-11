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

            builder.HasIndex(x => x.Name);
            builder.HasIndex(x => x.IdComputer);
            builder.HasIndex(x => x.Active);

            builder.HasOne(x=> x.Computer)
                .WithMany(p=> p.Storages)
                .HasForeignKey(x=>x.IdComputer)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Maker)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x=> x.Capacity).IsRequired();

            builder.Property(x=> x.Type)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x=> x.Name)
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(x=> x.Occupied)
                .IsRequired()
                .HasPrecision(18, 2); ;

            builder.Property(x => x.MonthsAssurance).IsRequired();

            builder.Property(x=> x.Price)
                .IsRequired()
                .HasPrecision(18, 2); 

            builder.Property(x=>x.Purchased).IsRequired();

            builder.Property(x => x.Create_at)
                .IsRequired();
                
            builder.Property(x=> x.Update_at).IsRequired().ValueGeneratedOnUpdate();
            builder.Property(x => x.Active).IsRequired();

        }
    }
}
