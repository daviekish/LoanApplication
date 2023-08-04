using DomainLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Banks: BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double FlatRate  { get; set; }
        public double ReducingBalance { get; set; }

    }
}
