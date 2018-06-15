using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyage_Thomas_Nicolas.Metier
{
    public class Participant : Personne
    {
        [ForeignKey("IdReservation")]
        public virtual DossierReservation DossierReservation { get; set; }
        public int IdReservation { get; set; }

        public override string ToString()
        {
            return $"{this.Nom}";
        }
    }
}
