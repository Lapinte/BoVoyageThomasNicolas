using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BoVoyage_Thomas_Nicolas.Metier;


namespace BoVoyage_Thomas_Nicolas.DAL
{
    public class BaseDonnees : DbContext
    {
        public BaseDonnees(string connectionString = "Connexion")
            : base(connectionString)
        {
        }

        public DbSet<AgenceVoyage> Agences { get; set; }

        public DbSet<Destination> Destinations { get; set; }

        public DbSet<Voyage> Voyages { get; set; }

        public DbSet<DossierReservation> DossierReservations { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Participant> Participants { get; set; }
    }
}
