using Mapper.Communication.Errors;
using Mapper.Domain.Entities.Abstractions;
using Mapper.Domain.Enums;
using System.ComponentModel.DataAnnotations;


namespace Mapper.Domain.Entities
{
    public class Equipament : AbsGuidDAO
    {
        [StringLength(100,MinimumLength =4,ErrorMessageResourceName = "CAMPO_TAMANHO_MAXIMO", ErrorMessageResourceType = typeof(ResError))]
        public string InternalCode { get; private set; } = string.Empty;
        public ETypeEquipament Type { get; set; }
        public EStatusEqipament Status { get; set; }
        [StringLength(120, MinimumLength = 3, ErrorMessageResourceName = "CAMPO_TAMANHO_MAXIMO", ErrorMessageResourceType = typeof(ResError))]
        public string Device { get; set; } = string.Empty;

        /// <summary>
        /// Criando Equipamento
        /// </summary>
        /// <param name="active"></param>
        /// <param name="count"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <param name="device"></param>

        public Equipament(bool active,int count, ETypeEquipament type, EStatusEqipament status, string device)
        {
            
            InternalCode = genarateCode(count,type);
            Type = type;
            Status = status;
            Create_at = DateTime.UtcNow.AddHours(-3);
            Update_at = DateTime.UtcNow.AddHours(-3);
            Active = active;
            Device = device;
        }

        /// <summary>
        /// Carregando Equipamento
        /// </summary>
        /// <param name="id"></param>
        /// <param name="create"></param>
        /// <param name="update"></param>
        /// <param name="active"></param>
        /// <param name="code"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <param name="device"></param>
        public Equipament(Guid id, DateTime create, DateTime update, bool active, string code,  ETypeEquipament type, EStatusEqipament status, string device) : base(id, create, update, active)
        {
            InternalCode = code;
            Type = type;
            Status= status;
            Device = device;
        }



        private string genarateCode(int value, ETypeEquipament type) => $"{type.getInternalCode()}-{value:0000000}";
 
    }
}
