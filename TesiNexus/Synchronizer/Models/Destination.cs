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
        public string DataBaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public Destination()
        {
            //
        }

        public string ConnectionString()
        {
            var conn = "";

            conn = $"Data Source={Source};Initial Catalog={DataBaseName};User id={UserName};Password={Password};";

            return conn;
        }
    }
}
