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
    }
}
