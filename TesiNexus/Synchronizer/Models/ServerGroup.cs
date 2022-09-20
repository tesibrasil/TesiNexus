using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesiNexus.Synchronizer.Models
{
    public class ServerGroup
    {
        public int ServerGroupID { get; set; }
        public String Name { get; set; }
        public List<Destination> Destinations { get; set; }


        public ServerGroup()
        {
            //
        }
    }
}
