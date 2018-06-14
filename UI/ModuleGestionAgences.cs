using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVoyage_Thomas_Nicolas.Metier;
using BoVoyage.Framework.UI;

namespace BoVoyage_Thomas_Nicolas.UI
{
    public class ModuleGestionAgences
    {
        // On définit ici les propriétés qu'on veut afficher
        //  et la manière de les afficher
        private static readonly List<InformationAffichage> strategieAffichageEntitesMetier =
            new List<InformationAffichage>
            {
                InformationAffichage.Creer<AgenceVoyage>(x=>x.Id, "Id", 3),
                InformationAffichage.Creer<AgenceVoyage>(x=>x.Nom, "Nom", 20),
            };

        private Menu menu;

        public ModuleGestionAgences(Application application)
        {
            this.Application = application;
        }

        private Application Application { get; }

        private void InitialiserMenu()
        {
            this.menu = new Menu("Gestion des Agences de Voyage");
            this.menu.AjouterElement(new ElementMenu("1", "Afficher les Agences")
            {
                FonctionAExecuter = this.AfficherAgences
            });
            this.menu.AjouterElement(new ElementMenu("2", "Ajouter une Agence")
            {
                FonctionAExecuter = this.AjouterAgence
            });
            this.menu.AjouterElement(new ElementMenu("3", "Supprimer une Agence")
            {
                FonctionAExecuter = this.SupprimerAgence
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

        private void AfficherAgences()
        {
            ConsoleHelper.AfficherEntete("Agences");

            var liste = Application.GetBaseDonnees().Agences.ToList();
            ConsoleHelper.AfficherListe(liste, strategieAffichageEntitesMetier);
        }

        private void AjouterAgence()
        {
            ConsoleHelper.AfficherEntete("Ajouter une Agence");

            var agence = new AgenceVoyage
            {
                Nom = ConsoleSaisie.SaisirChaineObligatoire("Nom : ")
            };

            using (var bd = Application.GetBaseDonnees())
            {
                bd.Agences.Add(agence);
                bd.SaveChanges();
            }
        }

        private void SupprimerAgence()
        {
            ConsoleHelper.AfficherEntete("Supprimer une Agence");

            var liste = Application.GetBaseDonnees().Agences.ToList();
            ConsoleHelper.AfficherListe(liste, strategieAffichageEntitesMetier);

            var id = ConsoleSaisie.SaisirEntierObligatoire("Id à supprimer : ");
            using (var bd = Application.GetBaseDonnees())
            {
                var agence = bd.Agences.Single(x => x.Id == id);
                bd.Agences.Remove(agence);
                bd.SaveChanges();
            }
        }
    }
}
