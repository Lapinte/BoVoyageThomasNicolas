using BoVoyage.Framework.UI;
using BoVoyage_Thomas_Nicolas.Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyage_Thomas_Nicolas.UI
{
    public class StrategiesAffichage
    {
        public static List<InformationAffichage> GetStrategieDestination()
        {
            List<InformationAffichage> strategieAffichageDestinations =
            new List<InformationAffichage>
            {
                InformationAffichage.Creer<Destination>(x=>x.Id, "Id", 3),
                InformationAffichage.Creer<Destination>(x=>x.Continent, "Continent", 20),
                InformationAffichage.Creer<Destination>(x=>x.Pays, "Pays", 15),
                InformationAffichage.Creer<Destination>(x=>x.Region, "Région", 15),
                InformationAffichage.Creer<Destination>(x=>x.Description, "Description", 80),
            };
            return strategieAffichageDestinations;
        }

        public static List<InformationAffichage> GetStrategieVoyage()
        {
            List<InformationAffichage> strategieAffichageVoyages =
            new List<InformationAffichage>
            {
                InformationAffichage.Creer<Voyage>(x=>x.Id, "Id", 3),
                InformationAffichage.Creer<Voyage>(x=>x.Destination, "Destination", 20),
                InformationAffichage.Creer<Voyage>(x=>x.DateAller, "DateAller", 15),
                InformationAffichage.Creer<Voyage>(x=>x.DateRetour, "DateRetour", 15),
                InformationAffichage.Creer<Voyage>(x=>x.PlacesDisponibles, "PlacesDisponibles", 20),
                InformationAffichage.Creer<Voyage>(x=>x.TarifToutCompris, "Tarif TTC par personne", 25),
            };
            return strategieAffichageVoyages;
        }
    }
}
