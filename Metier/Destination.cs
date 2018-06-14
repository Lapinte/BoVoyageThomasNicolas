using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyage_Thomas_Nicolas.Metier
{
    public class Destination
    {
        public int Id { get; set; }

        public Continent Continent { get; set; }

        public Pays Pays { get; set; }

        public Region Region { get; set; }

        public Description Description { get; set; }
    }
}
