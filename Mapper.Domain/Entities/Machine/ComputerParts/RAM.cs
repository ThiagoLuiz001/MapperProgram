using Mapper.Domain.Entities.Abstractions;
using Mapper.Communication.Entities;



namespace Mapper.Domain.Entities.Machine;

public class RAM : AbsIntComputerPartDAO
{
    public int Capacity { get; set; }
    public string Model { get; set; } = string.Empty;
    public int DDR { get; set; }
    public string Frequency { get; set; } = string.Empty;
    public string About() => String.Format(ResEntities.RAM_ABOUT, Model, Capacity, DDR, Frequency);
}
