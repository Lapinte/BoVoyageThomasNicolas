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

        private void InitialiserModules()
        {
            this.moduleGestionVoyages = new ModuleGestionVoyages(this);
        }

        private void InitialiserMenuPrincipal()
        {
            this.menuPrincipal = new Menu("Menu principal");
            this.menuPrincipal.AjouterElement(new ElementMenu("1", "Gestion des Voyages")
            {
                AfficherLigneRetourMenuApresExecution = false,
                FonctionAExecuter = this.moduleGestionVoyages.Demarrer
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
