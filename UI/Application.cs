using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVoyage.Framework.UI;
using System.Data.SqlClient;
using BoVoyage_Thomas_Nicolas.DAL;
using System.Configuration;

namespace BoVoyage_Thomas_Nicolas.UI
{
    public class Application
    {
        private Menu menuPrincipal;

        private ModuleGestionDestinations moduleGestionDestinations;
        private ModuleGestionAgences moduleGestionAgences;
        private ModuleGestionDossiers moduleGestionDossiers;

        public ModuleGestionClients ModuleGestionClients { get; set; }
        public ModuleGestionVoyages ModuleGestionVoyages { get; set; }

        private void InitialiserModules()
        {
            this.ModuleGestionVoyages = new ModuleGestionVoyages(this);
            this.ModuleGestionClients = new ModuleGestionClients(this);
            this.moduleGestionDestinations = new ModuleGestionDestinations(this);
            this.moduleGestionAgences = new ModuleGestionAgences(this);
            this.moduleGestionDossiers = new ModuleGestionDossiers(this);
        }

        private void InitialiserMenuPrincipal()
        {
            this.menuPrincipal = new Menu("Menu principal");
            this.menuPrincipal.AjouterElement(new ElementMenu("1", "Gestion des Voyages")
            {
                AfficherLigneRetourMenuApresExecution = false,
                FonctionAExecuter = this.ModuleGestionVoyages.Demarrer
            });
            this.menuPrincipal.AjouterElement(new ElementMenu("2", "Gestion des Clients")
            {
                AfficherLigneRetourMenuApresExecution = false,
                FonctionAExecuter = this.ModuleGestionClients.Demarrer
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
            this.menuPrincipal.AjouterElement(new ElementMenu("5", "Gestion des Dossiers de Reservation")
            {
                AfficherLigneRetourMenuApresExecution = false,
                FonctionAExecuter = this.moduleGestionDossiers.Demarrer
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

        public static SqlConnection GetConnexion()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Connexion"].ConnectionString;
            return new SqlConnection(connectionString);
        }

        public static BaseDonnees GetBaseDonnees()
        {
            return new BaseDonnees();
        }
    }
}
