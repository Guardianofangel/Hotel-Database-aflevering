using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Database_aflevering
{
    public class HotelFacility
    {
        public int id { get; set; }

        public int Hotel_No { get; set; }

        public int Faciletet_id { get; set; }

        public override string ToString()
        {
            return $"{id}, {Hotel_No}, {Faciletet_id}";
        }
    }
}
