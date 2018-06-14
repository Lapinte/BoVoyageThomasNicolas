using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVoyage.Framework.UI;

namespace BoVoyage_Thomas_Nicolas.UI
{
    public class Application
    {
        private Menu menuPrincipal;
        private ModuleGestionVoyages moduleGestionVoyages;
        private ModuleGestionClients moduleGestionClients;
        private ModuleGestionDestinations moduleGestionDestinations;
        private ModuleGestionAgences moduleGestionAgences;

        private void InitialiserModules()
        {
            this.moduleGestionVoyages = new ModuleGestionVoyages(this);
            this.moduleGestionClients = new ModuleGestionClients(this);
            this.moduleGestionDestinations = new ModuleGestionDestinations(this);
            this.moduleGestionAgences = new ModuleGestionAgences(this);
        }

        private void InitialiserMenuPrincipal()
        {
            this.menuPrincipal = new Menu("Menu principal");
            this.menuPrincipal.AjouterElement(new ElementMenu("1", "Gestion des Voyages")
            {
                AfficherLigneRetourMenuApresExecution = false,
                FonctionAExecuter = this.moduleGestionVoyages.Demarrer
            });
            this.menuPrincipal.AjouterElement(new ElementMenu("2", "Gestion des Clients")
            {
                AfficherLigneRetourMenuApresExecution = false,
                FonctionAExecuter = this.moduleGestionClients.Demarrer
            });
            this.menuPrincipal.AjouterElement(new ElementMenu("3", "Gestion des Destinations")
            {
                AfficherLigneRetourMenuApresExecution = false,
                FonctionAExecuter = this.moduleGestionDestinations.Demarrer
            });
            this.menuPrincipal.AjouterElement(new ElementMenu("4", "Gestion des Agences")
            {
                AfficherLigneRetourMenuApresExecution = false,
                FonctionAExecuter = this.moduleGestionAgences.Demarrer
            });
            this.menuPrincipal.AjouterElement(new ElementMenuQuitterMenu("Q", "Quitter")
            {
                FonctionAExecuter = () => Environment.Exit(1)
            });
        }

        public void Demarrer()
        {
            this.InitialiserModules();
            this.InitialiserMenuPrincipal();

            this.menuPrincipal.Afficher();
        }
    }
}
