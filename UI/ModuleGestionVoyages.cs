using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVoyage_Thomas_Nicolas.Metier;
using BoVoyage.Framework.UI;


namespace BoVoyage_Thomas_Nicolas.UI
{
    public class ModuleGestionVoyages
    {
        // On définit ici les propriétés qu'on veut afficher
        //  et la manière de les afficher
        private static readonly List<InformationAffichage> strategieAffichageVoyages =
            new List<InformationAffichage>
            {
                InformationAffichage.Creer<Voyage>(x=>x.Id, "Id", 3),
                InformationAffichage.Creer<Voyage>(x=>x.Destination, "Destination", 20),
            };

        private Menu menu;

        public ModuleGestionVoyages(Application application)
        {
            this.Application = application;
        }

        private Application Application { get; }

        private void InitialiserMenu()
        {
            this.menu = new Menu("Gestion des Voyages");
            this.menu.AjouterElement(new ElementMenu("1", "Afficher les Voyages disponibles")
            {
                FonctionAExecuter = this.AfficherVoyages
            });
            this.menu.AjouterElement(new ElementMenu("2", "Rechercher les Voyages disponibles par pays")
            {
                FonctionAExecuter = this.RechercherVoyage
            });
            this.menu.AjouterElement(new ElementMenu("3", "Ajouter un nouveau Voyage")
            {
                FonctionAExecuter = this.AjouterVoyage
            });
            this.menu.AjouterElement(new ElementMenu("4", "Supprimer un Voyage")
            {
                FonctionAExecuter = this.SupprimerVoyage
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

        public void AfficherVoyages()
        {
            ConsoleHelper.AfficherEntete("Voyages disponibles");

            using (var bd = Application.GetBaseDonnees())
            {
                var listeVoyages = bd.Voyages.ToList();
                ConsoleHelper.AfficherListe(listeVoyages, StrategiesAffichage.GetStrategieVoyage());
            }
        }

        private void AjouterVoyage()
        {
            ConsoleHelper.AfficherEntete("Ajouter un voyage");

            using (var bd = Application.GetBaseDonnees())
            {
                var voyage = new Voyage();


                //choix destination
                var listeDestination = bd.Destinations.ToList();
                ConsoleHelper.AfficherListe(listeDestination, StrategiesAffichage.GetStrategieDestination());
                var idDestination = ConsoleSaisie.SaisirEntierObligatoire("Choisissez une destination (ID) : ");
                if (!bd.Destinations.Any(x => x.Id == idDestination))
                {
                    ConsoleHelper.AfficherMessageErreur("Cette Destination n'existe pas, retour au menu");
                    return;
                }
                voyage.Destination = bd.Destinations.Single(x => x.Id == idDestination);

                //choix d'une agence
                var listeAgence = bd.Agences.ToList();
                ConsoleHelper.AfficherListe(listeAgence, StrategiesAffichage.GetStrategieAgence());
                var idAgence = ConsoleSaisie.SaisirEntierObligatoire("Choisissez une Agence (ID) : ");
                if (!bd.Agences.Any(x => x.Id == idAgence))
                {
                    ConsoleHelper.AfficherMessageErreur("Cette Agence n'existe pas, retour au menu");
                    return;
                }
                voyage.Agence = bd.Agences.Single(x => x.Id == idAgence);

                var dateAller = ConsoleSaisie.SaisirDateObligatoire("Choisissez la date de début du Voyage (AAAA-MM-JJ): ");
                var dateRetour = ConsoleSaisie.SaisirDateObligatoire("Choisissez la date de fin du Voyage (AAAA-MM-JJ): ");
                if (dateRetour <= dateAller)
                {
                    ConsoleHelper.AfficherMessageErreur("La date de fin doit être postèrieur à la date de début, retour au menu");
                    return;
                }
                voyage.DateAller = dateAller;
                voyage.DateRetour = dateRetour;
                voyage.PlacesDisponibles = ConsoleSaisie.SaisirEntierObligatoire("Choisissez le nombre de places disponibles");
                voyage.TarifToutCompris = ConsoleSaisie.SaisirDecimalObligatoire("Choisissez un tarif TTC par participant");

                bd.Voyages.Add(voyage);
                bd.SaveChanges();

            }
        }

        private void SupprimerVoyage()
        {
            ConsoleHelper.AfficherEntete("Supprimer un Voyage");

            AfficherVoyages();
            var idVoyage = ConsoleSaisie.SaisirEntierObligatoire("Voyage à supprimer (Id): ");
            
            using (var bd = Application.GetBaseDonnees())
            {
                if (!bd.Voyages.Any(x => x.Id == idVoyage))
                {
                    ConsoleHelper.AfficherMessageErreur("Ce Voyage n'existe pas, retour au menu");
                    return;
                }
                var voyage = bd.Voyages.Single(x => x.Id == idVoyage);
                bd.Voyages.Remove(voyage);
                bd.SaveChanges();
            }
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

        private void RechercherVoyage()
        {
            ConsoleHelper.AfficherEntete("Voyages disponibles pour ce pays");

            AfficherDestinations();

            //var listeVoyages = from Voyage where 

            var saisieVoyage = ConsoleSaisie.SaisirChaineObligatoire("Entrez le nom d'un Pays (ou une partie) : ");

            using (var bd = Application.GetBaseDonnees())
            {
                var voyages = bd.Voyages.Where(x => x.Destination.Pays.Contains(saisieVoyage)).ToList();

                ConsoleHelper.AfficherListe(voyages, StrategiesAffichage.GetStrategieVoyage());

            }
        }
    }
}
