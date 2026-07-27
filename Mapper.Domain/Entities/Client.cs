using Mapper.Domain.Entities.Commons;

namespace Mapper.Domain.Entities
{
    public class Client : Person
    {
        public List<Address>? Addresses { get; set; }
        public Address? Address { get => getAdress(); }


        private Address? getAdress()
        {
            return Addresses!.FirstOrDefault(x=> x.Active);
        }
    }
}
