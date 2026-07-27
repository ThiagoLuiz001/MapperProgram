

namespace Mapper.Domain.Entities.Abstractions
{
    public abstract class AbsIntDAO
    {
        public int Id { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime Update_at { get; set; }
        public bool Active { get; set; }
    }
}
