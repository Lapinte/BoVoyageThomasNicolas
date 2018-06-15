using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVoyage_Thomas_Nicolas.Metier;
using BoVoyage.Framework.UI;

namespace BoVoyage_Thomas_Nicolas.UI
{
    public class ModuleGestionDestinations
    {
        // On définit ici les propriétés qu'on veut afficher
        //  et la manière de les afficher
        public static readonly List<InformationAffichage> strategieAffichageDestinations =
            new List<InformationAffichage>
            {
                InformationAffichage.Creer<Destination>(x=>x.Id, "Id", 3),
                InformationAffichage.Creer<Destination>(x=>x.Continent, "Continent", 20),
                InformationAffichage.Creer<Destination>(x=>x.Pays, "Pays", 15),
                InformationAffichage.Creer<Destination>(x=>x.Region, "Région", 15),
                InformationAffichage.Creer<Destination>(x=>x.Description, "Description", 80),
            };

        private Menu menu;

        public ModuleGestionDestinations(Application application)
        {
            this.Application = application;
        }

        private Application Application { get; }

        private void InitialiserMenu()
        {
            this.menu = new Menu("Gestion des Destinations");
            this.menu.AjouterElement(new ElementMenu("1", "Afficher les Destinations disponibles")
            {
                FonctionAExecuter = this.AfficherDestinations
            });
            this.menu.AjouterElement(new ElementMenu("2", "Ajouter une Destination")
            {
                FonctionAExecuter = this.AjouterDestination
            });
            this.menu.AjouterElement(new ElementMenu("3", "Supprimer une Destination")
            {
                FonctionAExecuter = this.SupprimerDestination
            });
            this.menu.AjouterElement(new ElementMenuQuitterMenu("R", "Revenir au menu principal..."));
        }

        public void Demarrer()
        {
            if (this.menu == null)
            {
                this.InitialiserMenu();
            }

            this.menu.Afficher();
        }

        private void AfficherDestinations()
        {
            ConsoleHelper.AfficherEntete("Destinations disponibles");

                using (var bd = Application.GetBaseDonnees())
                {
                    var listeDestinations = bd.Destinations.ToList();
                    ConsoleHelper.AfficherListe(listeDestinations, StrategiesAffichage.GetStrategieDestination());
                }
           
        }

        private void AjouterDestination()
        {
            {
                ConsoleHelper.AfficherEntete("Ajouter une Destination");

                var destination = new Destination
                {
                    Continent = ConsoleSaisie.SaisirChaineObligatoire("Continent:"),
                    Pays = ConsoleSaisie.SaisirChaineObligatoire("Pays:"),
                    Region = ConsoleSaisie.SaisirChaineObligatoire("Region:"),
                    Description = ConsoleSaisie.SaisirChaineObligatoire("Description:"),

                };

                using (var bd = Application.GetBaseDonnees())
                {
                    bd.Destinations.Add(destination);
                    bd.SaveChanges();
                }
            }
        }

        private void SupprimerDestination()
        {
            ConsoleHelper.AfficherEntete("Supprimer une Destination");

            AfficherDestinations();
            var idDestination = ConsoleSaisie.SaisirEntierObligatoire("Destination à supprimer (Id) : ");
            using (var bd = Application.GetBaseDonnees())
            {
                if (!bd.Destinations.Any(x => x.Id == idDestination))
                {
                    ConsoleHelper.AfficherMessageErreur("Cette Destination n'existe pas, retour au menu");
                    return;
                }
                var destination = bd.Destinations.Single(x => x.Id == idDestination);
                bd.Destinations.Remove(destination);
                bd.SaveChanges();
            }
        }
    }
}
