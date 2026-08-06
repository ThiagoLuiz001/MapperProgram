using Mapper.Communication.Entities;
using Mapper.Domain.Entities.Abstractions;
using System;


namespace Mapper.Domain.Entities.Machine.ComputerParts
{
    public class Motherboard : AbsIntComputerPartDAO
    {
        public string LGA { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string About() => String.Format(ResEntities.MOTHERBOARD_ABOUT, Description, LGA,Maker);
    }
}
