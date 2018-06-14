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

        public string NumeroCarteBancaire { get; set; }

        public decimal PrixTotal { get; set; }

        public int EtatDossierReservation { get; set; }

        [ForeignKey("IdVoyage")]
        public Voyage Voyage { get; set; }
        public int IdVoyage { get; set; }

        [ForeignKey("IdClient")]
        public Client Client { get; set; }
        public int IdClient { get; set; }

        //[ForeignKey("IdParticipant")]
        //public Participant Participant { get; set; }
        //public int IdParticipant { get; set; }
    

    }
}
