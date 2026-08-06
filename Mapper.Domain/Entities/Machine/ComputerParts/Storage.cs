using Mapper.Domain.Entities.Abstractions;
using Mapper.Domain.Enums;
using Mapper.Communication.Entities;



namespace Mapper.Domain.Entities.Machine.ComputerParts
{
    public class Storage : AbsIntComputerPartDAO
    {

        public EStorage Type { get; set; }
        public int Capacity { get; set; }
        public double Occupied { get; set; }

        public double FreeSpace => Capacity - Occupied;

        public string About() => Capacity <= 0 ? "-" : String.Format(ResEntities.STORAGE_ABOUT, Type.ToString(), Capacity, Occupied, FreeSpace);
        
    }
}
