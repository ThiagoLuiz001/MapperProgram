using Mapper.Domain.Entities.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper.Domain.Entities.Abstractions
{
    public abstract class AbsIntComputerPartDAO
    {
        public int Id { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime Update_at { get; set; }
        public string Maker { get; set; } = string.Empty
        public DateOnly Purchased { get; set; }
        public double Price { get; set; }
        public int MonthsAssurance { get; set; } = 3;
        public int IdComputer { get; set; }
        public Computer? Computer { get; set; }
        public bool Active { get; set; }

    }
}
