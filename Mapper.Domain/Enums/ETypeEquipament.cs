

namespace Mapper.Domain.Enums
{
    public enum ETypeEquipament
    {
        Computer = 0,
        Peripheral = 1,
        Printer = 2
    }

    public static class EEquipamentExtensions
    {
        public static string getInternalCode(this ETypeEquipament equipament)
        {
            return equipament switch
            {
                ETypeEquipament.Computer => "COMP",
                ETypeEquipament.Printer => "PRINT",
                ETypeEquipament.Peripheral => "I/O",
                _ => throw new ArgumentOutOfRangeException(nameof(equipament), equipament, null)
            };
        }
    }


}
