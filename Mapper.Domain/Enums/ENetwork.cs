
using Mapper.Communication.Exceptions;
using Mapper.Domain.Entities.Machine;
using Mapper.Domain.Enums;

namespace Mapper.Domain.Enums
{
    public enum ENetwork
    {
        DHCP = 1,
        Estatico = 2
    }
}

public static class ENetworkExtensions
{
    public static IP GetIP(this ENetwork network, IP? data)
    {
        if (network == ENetwork.Estatico)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), ResExceptions.IP_ESTATICO_NOT_NULL);
            }
            return data;
        }
        else
        {
            return new IP
            {
                Ipv4 = "DHCP",
                SubRede = "-",
                Gateway = "-",
                Dns1 = "-",
                Dns2 = "-",
                Dominio = "WORKGROUP"
            };
        }
    }
}