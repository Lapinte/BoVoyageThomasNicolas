using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyage_Thomas_Nicolas.Metier
{
    public class Voyage
    {
        public int Id { get; set; }

        public DateTime DateAller { get; set; }
       
        public DateTime DateRetour { get; set; }

        public int PlacesDisponibles { get; set; }

        public decimal TarifToutCompris { get; set; }

        [ForeignKey("IdDestination")]
        public virtual Destination Destination { get; set; }
        public int IdDestination { get; set; }

        [ForeignKey("IdAgence")]
        public virtual AgenceVoyage Agence { get; set; }
        public int IdAgence { get; set; }
    }
}
