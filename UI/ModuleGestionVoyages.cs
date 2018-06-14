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
        private static readonly List<InformationAffichage> strategieAffichageEntitesMetier =
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
            this.menu.AjouterElement(new ElementMenu("2", "Ajouter un nouveau Voyage")
            {
                FonctionAExecuter = this.AjouterVoyage
            });
            this.menu.AjouterElement(new ElementMenu("3", "Supprimer un Voyage")
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

        private void AfficherVoyages()
        {
            ConsoleHelper.AfficherEntete("Voyages disponibles");

            Console.WriteLine("TO DO");
        }

        private void AjouterVoyage()
        {
            ConsoleHelper.AfficherEntete("Ajouter un voyage");

            Console.WriteLine("TO DO");
        }

        private void SupprimerVoyage()
        {
            ConsoleHelper.AfficherEntete("Supprimer un voyage");

            Console.WriteLine("TO DO");
        }
    }
}
