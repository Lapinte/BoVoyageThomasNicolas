using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyage_Thomas_Nicolas.Metier
{
    [Table("Reservations")]
    public class DossierReservation
    {
        public int Id { get; set; }

        public int NumeroCarteBancaire { get; set; }

        public decimal PrixTotal { get; set; }

        public string EtatDossierReservation { get; set; }

        [ForeignKey("IdVoyage")]
        public virtual Voyage Voyage { get; set; }
        public int IdVoyage { get; set; }

        [ForeignKey("IdClient")]
        public virtual Client Client { get; set; }
        public int IdClient { get; set; }

        public virtual ICollection<Participant> Participants { get; set; }

        public bool AssuranceAnnulation { get; set; }

    
    

    }
}
