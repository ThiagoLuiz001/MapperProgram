using Mapper.Domain.Entities.Abstractions;
using Mapper.Communication.Entities;


namespace Mapper.Domain.Entities.Machine.ComputerParts
{
    public class CPU : AbsIntComputerPartDAO
    {
        public string Name { get; set; } = string.Empty;
        public int? Generation { get; set; }
        public int Core { get; set; }
        public int Threads { get; set; }
        public string Frequency { get; set; } = string.Empty;

        public string About() => String.Format(ResEntities.CPU_ABOUT, Name, Maker, Core, Threads, Generation);
    }
}
