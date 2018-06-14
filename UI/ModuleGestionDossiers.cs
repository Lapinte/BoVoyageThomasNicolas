using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVoyage_Thomas_Nicolas.Metier;
using BoVoyage.Framework.UI;

namespace BoVoyage_Thomas_Nicolas.UI
{
    public class ModuleGestionDossiers
    {
        // On définit ici les propriétés qu'on veut afficher
        //  et la manière de les afficher
        private static readonly List<InformationAffichage> strategieAffichageEntitesMetier =
            new List<InformationAffichage>
            {
                InformationAffichage.Creer<DossierReservation>(x=>x.Id, "Id", 3),
                InformationAffichage.Creer<DossierReservation>(x=>x.EtatDossierReservation, "Etat", 15),
            };

        private Menu menu;

        public ModuleGestionDossiers(Application application)
        {
            this.Application = application;
        }

        private Application Application { get; }

        private void InitialiserMenu()
        {
            this.menu = new Menu("Gestion des Dossiers");
            this.menu.AjouterElement(new ElementMenu("1", "Afficher les Dossiers")
            {
                FonctionAExecuter = this.AfficherDossiers
            });
            this.menu.AjouterElement(new ElementMenu("2", "Ajouter un Dossier")
            {
                FonctionAExecuter = this.AjouterDossier
            });
            this.menu.AjouterElement(new ElementMenu("3", "Supprimer un Dossier")
            {
                FonctionAExecuter = this.SupprimerDossier
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

        private void AfficherDossiers()
        {
            ConsoleHelper.AfficherEntete("Dossiers");

            Console.WriteLine("TO DO");
        }

        private void AjouterDossier()
        {
            ConsoleHelper.AfficherEntete("Ajouter un Dossier");

            using (var bd = Application.GetBaseDonnees())
            {
                var dossier = new DossierReservation();

                var listeVoyages = bd.Voyages.ToList();
                ConsoleHelper.AfficherListe(listeVoyages, StrategiesAffichage.GetStrategieVoyage());

                var idVoyage = ConsoleSaisie.SaisirEntierObligatoire("Choisissez un Voyage (ID) : ");
                if (!bd.Voyages.Any(x => x.Id == idVoyage))
                {
                    ConsoleHelper.AfficherMessageErreur("Ce Voyage n'existe pas, retour au menu");
                    return;
                }

                dossier.Voyage = bd.Voyages.Single(x => x.Id == idVoyage);

                var listeClients = Application.GetBaseDonnees().Clients.ToList();
                ConsoleHelper.AfficherListe(listeClients, strategieAffichageEntitesMetier);
            }
        }

        private void SupprimerDossier()
        {
            ConsoleHelper.AfficherEntete("Supprimer un Dossier");

            Console.WriteLine("TO DO");
        }
    }
}
