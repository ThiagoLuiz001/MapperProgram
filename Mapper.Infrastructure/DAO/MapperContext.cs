using Mapper.Domain.Entities;
using Mapper.Domain.Entities.Machine;
using Mapper.Domain.Entities.Machine.ComputerParts;
using Microsoft.EntityFrameworkCore;

namespace Mapper.Infrastructure.DAO
{
    public class MapperContext : DbContext
    {

        public MapperContext(DbContextOptions<MapperContext> options) : base(options)
        {

        }

        public DbSet<Equipament> Equipaments { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Motherboard> Motherboards { get; set; }
        public DbSet<CPU> CPUs { get; set; }
        public DbSet<Components> ComputerComponets { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<IP> IPs { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MapperContext).Assembly);
        }
    }
}
