using Mapper.Communication.Entities;
using Mapper.Domain.Entities.Abstractions;

namespace Mapper.Domain.Entities.Machine
{
    public class IP : AbsIntDAO
    {
        public string Ipv4 { get; set; } = string.Empty;
        public string SubRede { get; set; } = string.Empty;
        public string Gateway { get; set; } = string.Empty;
        public string? Dns1 { get; set; }
        public string? Dns2 { get; set; }
        public string Dominio { get; set; } = string.Empty;

        public string About() => String.Format(ResEntities.IP_ABOUT, Ipv4, SubRede, Gateway, Dns1 == null ? '-' : Dns1, Dns2 == null ? '-' : Dns2,Dominio);

    }
}
