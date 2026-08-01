using Mapper.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mapper.Infrastructure.DAO
{
    public class MapperContext : DbContext
    {

        public MapperContext(DbContextOptions<MapperContext> options) : base(options)
        {

        }

        public DbSet<Client> ClientsInfo { get; set; }
        public DbSet<Address> AddresInfo { get; set; }
    }
}
