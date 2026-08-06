using Mapper.Communication.Entities;

namespace Mapper.Domain.Enums
{
    public enum EStorage
    {
        HDD = 1,
        HD = 2,
        SSD = 3,
        NVME = 4,
        EXTERNAL_HD = 5,
        EXTERNAL_SSD = 6,
        EXTERNAL_NVME = 7
    }

    public static class EStorageExtensions
    {
        public static string ToString(this EStorage storage)
        {
            return storage switch
            {
                EStorage.HDD => "HDD",
                EStorage.HD => "HD",
                EStorage.SSD => "SSD",
                EStorage.NVME => "NVMe",
                EStorage.EXTERNAL_HD => ResEntities.EXTERNAL_HD,
                EStorage.EXTERNAL_SSD => ResEntities.EXTERNAL_SSD,
                EStorage.EXTERNAL_NVME => ResEntities.EXTERNAL_NVME,
                _ => throw new ArgumentOutOfRangeException(nameof(storage), storage, null)
            };
        }
    }
}

