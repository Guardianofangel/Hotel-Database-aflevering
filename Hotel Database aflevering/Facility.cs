using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Database_aflevering
{
    public class Facility
    {
       public int Faciletet_id { get; set; }
       public string Name { get; set; }

        public override string ToString()
        {
            return $"{Faciletet_id} + {Name}";
        }

    }
}
