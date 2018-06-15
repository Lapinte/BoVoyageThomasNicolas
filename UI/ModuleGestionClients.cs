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

        private void InitialiserMenu()
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
                FonctionAExecuter = this.RechercherClient
            });
            this.menu.AjouterElement(new ElementMenu("4", "Ajouter un Client")
            {
                FonctionAExecuter = this.AjouterClient
            });
            this.menu.AjouterElement(new ElementMenu("5", "Supprimer un Client")
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

        private void AfficherClients()
        {
            ConsoleHelper.AfficherEntete("Clients");

            var liste = Application.GetBaseDonnees().Clients.ToList();
            ConsoleHelper.AfficherListe(liste, StrategiesAffichage.GetStrategieClient());
        }

        //private void AjouterClient()
        //{
        //    ConsoleHelper.AfficherEntete("Ajouter un Client");

        //    var client = new Client
        //    {
        //        Nom = ConsoleSaisie.SaisirChaineObligatoire("Nom : "),
        //        Civilite = ConsoleSaisie.SaisirChaineObligatoire("Civilite:"),
        //        Prenom = ConsoleSaisie.SaisirChaineObligatoire("Prenom:"),
        //        Adresse = ConsoleSaisie.SaisirChaineObligatoire("Adresse:"),
        //        //saisie d'une date 
        //        var DateNaissance = 
        //        DateNaissance = ConsoleSaisie.("Date de naissance:"),
        //        Email = ConsoleSaisie.SaisirChaineObligatoire("Email:"),
        //    };

        //    using (var bd = Application.GetBaseDonnees())
        //    {
        //        bd.Clients.Add(client);
        //        bd.SaveChanges();
        //    }
        //}
        private void AjouterClient()
        {
            ConsoleHelper.AfficherEntete("Ajouter un client");

            ConsoleHelper.AfficherEntete("Nouveau Client");


            Console.WriteLine("Civilité:");
            var Civilite = Console.ReadLine();
            Console.WriteLine("Nom:");
            var Nom = Console.ReadLine();
            Console.WriteLine("Prenom:");
            var Prenom = Console.ReadLine();
            Console.WriteLine("Adresse:");
            var Adresse = Console.ReadLine();
            Console.WriteLine("Telephone");
            var Telephone = Console.ReadLine();
            Console.WriteLine("Date de naissance:");
            var DateNaissance = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Email");
            var Email = Console.ReadLine();
            var Age = 20;
            //Avec Entity Framework
            Client nouveauClient = new Client

            {//passer en EF
                Nom = Nom,
                Civilite = Civilite,
                Prenom = Prenom,
                Adresse = Adresse,
                Telephone = Telephone,
                Email = Email,
                DateNaissance = DateNaissance,
                Age = Age
               
                //TODO : Calculer Age
            };

            using (var bd = Application.GetBaseDonnees())
            {
                bd.Clients.Add(nouveauClient);
                bd.SaveChanges();
            }
        }

        private void SupprimerClient()
        {
            ConsoleHelper.AfficherEntete("Supprimer un Client");

            //var liste = Application.GetBaseDonnees().Clients.ToList();
            //ConsoleHelper.AfficherListe(liste, strategieAffichageEntitesMetier);
            RechercherClient();
            var id = ConsoleSaisie.SaisirEntierObligatoire("Id à supprimer : ");
            using (var bd = Application.GetBaseDonnees())
            {
                var client = bd.Clients.Single(x => x.Id == id);
                bd.Clients.Remove(client);
                bd.SaveChanges();
            }
        }

        private void RechercherClient()//TODO:ajouter méthode recherche avec saisie partielle du nom
        {
            Console.WriteLine("Saisir le nom exact d'un client à afficher:");
            saisie:
            var saisie = Console.ReadLine();

            BaseDonnees context = new BaseDonnees();
            var client = from Client in context.Clients
                                   where Client.Nom == saisie
                                   select Client;
            if (client.Count() == 0)
            {
                Console.WriteLine("\nSaisie erronée, saisir le nom exact d'un produit à afficher:");
                goto saisie;
            }
            else
            {
            ConsoleHelper.AfficherListe(client, StrategiesAffichage.GetStrategieClient());
            }

        }

        private void AfficherParticipants()
        {
            ConsoleHelper.AfficherEntete("Participants");

            var liste = Application.GetBaseDonnees().Participants.ToList();
            ConsoleHelper.AfficherListe(liste, StrategiesAffichage.GetStrategieParticipant());
        }

        private void RechercherParticipant()//TODO:ajouter méthode recherche avec saisie partielle du nom
        {
            Console.WriteLine("Saisir le nom exact d'un participant à afficher:");
            saisie:
            var saisie = Console.ReadLine();

            BaseDonnees context = new BaseDonnees();
            var participant = from Participant in context.Participants
                         where Participant.Nom == saisie
                         select Participant;
            if (participant.Count() == 0)
            {
                Console.WriteLine("\nSaisie erronée, saisir le nom exact d'un produit à afficher:");
                goto saisie;
            }
            else
            {
                ConsoleHelper.AfficherListe(participant, StrategiesAffichage.GetStrategieParticipant());
            }

        }
    }



    

}
