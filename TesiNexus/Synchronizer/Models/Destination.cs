using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesiNexus.Synchronizer.Models
{
    public class Destination
    {
        public int DestinationID { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }


        public Destination()
        {
            //
        }
    }
}
