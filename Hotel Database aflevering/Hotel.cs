using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Database_aflevering
{
    public class Hotel
    {
        public int Hotel_No { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return $"Hotel No : {Hotel_No}, Hotel Name : {Name}, Hotel Adress {Address} ";
        }
    }
    
}
