using Mapper.Domain.Entities.Abstractions;
using Mapper.Domain.Enums;


namespace Mapper.Domain.Entities.Machine.ComputerParts
{
    public class Components :AbsIntComputerPartDAO
    {
        public ETypeComponent Type { get; set; }
        public string Description { get; set; } = string.Empty;

        public string About() => $"{Type.ToString()}: {Description}";

    }
}
