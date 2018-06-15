using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVoyage_Thomas_Nicolas.Metier;
using BoVoyage.Framework.UI;
using BoVoyage_Thomas_Nicolas.DAL;

namespace BoVoyage_Thomas_Nicolas.UI
{
    public class ModuleGestionClients
    {
        private Menu menu;

        public ModuleGestionClients(Application application)
        {
            this.Application = application;
        }

        private Application Application { get; }

        public void InitialiserMenu()
        {
            this.menu = new Menu("Gestion des Clients");
            this.menu.AjouterElement(new ElementMenu("1", "Afficher les Clients")
            {
                FonctionAExecuter = this.AfficherClients
            });
            this.menu.AjouterElement(new ElementMenu("2", "Afficher les Participants")
            {
                FonctionAExecuter = this.AfficherParticipants
            });
            this.menu.AjouterElement(new ElementMenu("3", "Rechercher un Client")
            {
                FonctionAExecuter = this.RechercherClient
            });
            this.menu.AjouterElement(new ElementMenu("4", "Rechercher un Participant")
            {
                FonctionAExecuter = this.RechercherParticipant
            });
            this.menu.AjouterElement(new ElementMenu("5", "Ajouter un Client")
            {
                FonctionAExecuter = this.AjouterClient
            });
            this.menu.AjouterElement(new ElementMenu("6", "Supprimer un Client")
            {
                FonctionAExecuter = this.SupprimerClient
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

        public void AfficherClients()
        {
            ConsoleHelper.AfficherEntete("Clients");

            using (var bd = Application.GetBaseDonnees())
            {
                var liste = bd.Clients.ToList();
                ConsoleHelper.AfficherListe(liste, StrategiesAffichage.GetStrategieClient());
            }
        }

        private void AfficherParticipants()
        {
            ConsoleHelper.AfficherEntete("Participants");

            using (var bd = Application.GetBaseDonnees())
            {
                var liste = bd.Participants.ToList();
                ConsoleHelper.AfficherListe(liste, StrategiesAffichage.GetStrategieParticipant());
            }
        }

        private void AjouterClient()
        {
            ConsoleHelper.AfficherEntete("Ajouter un client");


            using (var bd = Application.GetBaseDonnees())
            {
                var client = new Client
                {
                    Civilite = ConsoleSaisie.SaisirChaineObligatoire("Civilité : "),
                    Nom = ConsoleSaisie.SaisirChaineObligatoire("Nom : "),
                    Prenom = ConsoleSaisie.SaisirChaineObligatoire("Prenom : "),
                    Adresse = ConsoleSaisie.SaisirChaineObligatoire("Adresse : "),
                    Telephone = ConsoleSaisie.SaisirChaineObligatoire("Téléphone : "),
                    DateNaissance = ConsoleSaisie.SaisirDateObligatoire("Date de naissance : "),
                    Email = ConsoleSaisie.SaisirChaineObligatoire("Email : "),

                };

                bd.Clients.Add(client);
                bd.SaveChanges();
            }
        }

        public Participant AjouterParticipant()
        {
            ConsoleHelper.AfficherEntete("Ajouter un participant");
            
                var participant = new Participant
                {
                    Civilite = ConsoleSaisie.SaisirChaineObligatoire("Civilité : "),
                    Nom = ConsoleSaisie.SaisirChaineObligatoire("Nom : "),
                    Prenom = ConsoleSaisie.SaisirChaineObligatoire("Prenom : "),
                    Adresse = ConsoleSaisie.SaisirChaineObligatoire("Adresse : "),
                    Telephone = ConsoleSaisie.SaisirChaineObligatoire("Téléphone : "),
                    DateNaissance = ConsoleSaisie.SaisirDateObligatoire("Date de naissance : "),
                };
                
                return participant;
            
        }

        private void SupprimerClient()
        {
            ConsoleHelper.AfficherEntete("Supprimer un Client");

            RechercherClient();
            var idClient = ConsoleSaisie.SaisirEntierObligatoire("Client à supprimer (Id) ");
            using (var bd = Application.GetBaseDonnees())
            {
                if (!bd.Clients.Any(x => x.Id == idClient))
                {
                    ConsoleHelper.AfficherMessageErreur("Ce Client n'existe pas, retour au menu");
                    return;
                }
                var client = bd.Clients.Single(x => x.Id == idClient);
                bd.Clients.Remove(client);
                bd.SaveChanges();
            }
        }

        private void RechercherClient()
        {
            ConsoleHelper.AfficherEntete("Rechercher un Client");

            var saisi = ConsoleSaisie.SaisirChaineObligatoire("Entrez un nom (ou une partie) : ");

            using (var bd = Application.GetBaseDonnees())
            {
                var liste = bd.Clients.Where(x => x.Nom.Contains(saisi)).ToList();
                ConsoleHelper.AfficherListe(liste, StrategiesAffichage.GetStrategieClient());
            }
        }

        private void RechercherParticipant()
        {
            ConsoleHelper.AfficherEntete("Rechercher un Participant");

            var saisi = ConsoleSaisie.SaisirChaineObligatoire("Entrez un nom (ou une partie) : ");

            using (var bd = Application.GetBaseDonnees())
            {
                var liste = bd.Participants.Where(x => x.Nom.Contains(saisi)).ToList();
                ConsoleHelper.AfficherListe(liste, StrategiesAffichage.GetStrategieParticipant());
            }
        }
    }
}
