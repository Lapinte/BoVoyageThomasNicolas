using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyage_Thomas_Nicolas.Metier
{
    public class DossierReservation
    {
        public int Id { get; set; }

        public string NumeroCarteBancaire { get; set; }

        public decimal PrixTotal { get; set; }

        public int EtatDossierReservation { get; set; }

        public Voyage Voyage { get; set; }

        
        public Client Client { get; set; }
        

        public Participant Participant { get; set; }

    }
}
