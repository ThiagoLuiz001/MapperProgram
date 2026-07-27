
using Mapper.Domain.Entities.Abstractions;

namespace Mapper.Domain.Entities.Commons
{
    public class Person : AbsIntDAO
    {
        public string Name { get; set; } = string.Empty;
        public string DocumentCPF { get; set; } = string.Empty;
        public string? DocumentRG { get; set; }

        public DateOnly BirthDay { get; set; }
        public int Age { get => getAge(); }

        private int getAge()
        {
           var dt = Convert.ToDateTime(BirthDay);
           TimeSpan result =  DateTime.Now.Subtract(dt);
            return (int)result.TotalDays;
        }
    }
}
