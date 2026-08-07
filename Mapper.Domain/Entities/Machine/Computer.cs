using Mapper.Communication.Entities;
using Mapper.Domain.Entities.Abstractions;
using Mapper.Domain.Entities.Machine.ComputerParts;
using Mapper.Domain.Enums;
using System.Text;

namespace Mapper.Domain.Entities.Machine
{
    public class Computer : AbsIntDAO
    {
        public Guid? EquipamentId { get; set; }
        public Equipament? Equipament { get; set; }


        public ETypeComputer Type { get; set; }
        public List<Motherboard>? Motherboards { get; set; } 
        public List<CPU>? CPUs { get; set; } 
        public List<RAM>? Memory { get; set; }
        public List<Components>? Components { get; set; }
        public List<GPU>? GPUs { get; set; } 
        public List<Storage>? Storages { get; set; }
        public string OperatinalSystem { get; set; } = string.Empty;
        public string MachineName { get; set; } = string.Empty;
        public string MACAddress { get; set; } = string.Empty;
        public string? Model { get; set; }
        public string? Maker { get; set; }


        public int IpId { get; set; }
        public IP? Ip { get; set; }


        public string? Observations { get; set; }


        public decimal TotalCost()
        {
            var cost = new List<double>();
            cost.Add(Motherboards?.Sum(m => m.Price) ?? 0);
            cost.Add(CPUs?.Sum(m=> m.Price) ?? 0);
            cost.Add(Memory?.Sum(m => m.Price) ?? 0);
            cost.Add(Storages?.Sum(s => s.Price) ?? 0);
            cost.Add(Components?.Sum(s=> s.Price) ?? 0);
            cost.Add(GPUs?.Sum(s=> s.Price) ?? 0);
            return (decimal)cost.Sum();
        }

        public decimal ActualCost()
        {
            var cost = new List<double>();
            cost.Add(Motherboards?.Where(x => x.Active).Sum(m => m.Price) ?? 0);
            cost.Add(CPUs?.Where(x=> x.Active).Sum(m => m.Price) ?? 0);
            cost.Add(Memory?.Where(x => x.Active).Sum(m => m.Price) ?? 0);
            cost.Add(Storages?.Where(x => x.Active).Sum(m => m.Price) ?? 0);
            cost.Add(Components?.Where(x=> x.Active).Sum(s => s.Price) ?? 0);
            cost.Add(GPUs?.Where(x=> x.Active).Sum(s => s.Price) ?? 0);
            return (decimal)cost.Sum();
        }

        public string About() => String.Format(ResEntities.ABOUT_COMPUTER, Type.ToString(), MachineName, OperatinalSystem, MACAddress, getHardware(), Ip!.About());

        private string getHardware()
        {
            int n = 1;
            StringBuilder sb = new StringBuilder();
            var motherboard = Motherboards?.LastOrDefault(x=> x.Active);
            var cpu = CPUs?.LastOrDefault(x=> x.Active);
            var gpu = GPUs?.LastOrDefault(x => x.Active);
            var rams = Memory?.Where(x=> x.Active).ToList();
            var components = Components?.Where(x => x.Active).ToList();
            var storages = Storages?.Where(x=> x.Active).ToList();

            sb.Append($"{ResEntities.MOTHERBOARD}: {motherboard!.About()},");
            sb.Append($"{ResEntities.CPU}: {cpu!.About()},");
            sb.Append($"{ResEntities.GPU}: {gpu!.About()},");
            string info = string.Empty;
            foreach(var ram in rams!)
            {
                info += $"{n}ª) {ram.About()},\n";
            }
            sb.Append($"{ResEntities.RAM}: \n{info}\n");
            n = 1;
            info = string.Empty;
            foreach(var storage in storages!)
            {
                info += $"{n}ª) {storage.About()},\n";
            }
            sb.Append($"{ResEntities.STORAGE}: \n {info}\n");
            info = string.Empty;
            foreach(var component in components!)
            {
                info += component.About() + "\n";
            }
            sb.Append(info);
            return sb.ToString();
        }
  

    }
}
