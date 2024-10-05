using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPZ_Lab2.Models
{
    [Serializable]
    public class PrintingHouse
    {
        public int PrintingHouseId { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

    }
}
