using Mapper.Communication.Entities;
using Mapper.Domain.Entities.Abstractions;

namespace Mapper.Domain.Entities.Machine.ComputerParts
{
    public class GPU : AbsIntComputerPartDAO
    {
        public string Name { get; set; } = string.Empty;
        public int VRam { get; set; }
        public string GBorMB { get; set; } = string.Empty;
        public int GDDR { get; set; }

        public string About() => String.Format(ResEntities.GPU_ABOUT, Name,VRam,GBorMB.ToUpper(),GDDR);
        
    }
}
