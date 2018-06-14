﻿using System;
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
        // On définit ici les propriétés qu'on veut afficher
        //  et la manière de les afficher
        private static readonly List<InformationAffichage> strategieAffichageEntitesMetier =
            new List<InformationAffichage>
            {
                InformationAffichage.Creer<Client>(x=>x.Id, "Id", 3),
                InformationAffichage.Creer<Client>(x=>x.Civilite, "Civilité", 10),
                InformationAffichage.Creer<Client>(x=>x.Nom, "Nom", 20),
                InformationAffichage.Creer<Client>(x=>x.Prenom, "Prénom", 20),
                InformationAffichage.Creer<Client>(x=>x.Adresse, "Adresse", 50),
                InformationAffichage.Creer<Client>(x=>x.Telephone, "TEL", 15),
            };

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
            this.menu.AjouterElement(new ElementMenu("2", "Rechercher un Client")
            {
                FonctionAExecuter = this.RechercherClient
            });
            this.menu.AjouterElement(new ElementMenu("3", "Ajouter un Client")
            {
                FonctionAExecuter = this.AjouterClient
            });
            this.menu.AjouterElement(new ElementMenu("4", "Supprimer un Client")
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
            ConsoleHelper.AfficherListe(liste, strategieAffichageEntitesMetier);
        }

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

            //Avec Entity Framework
            Client nouveauClient = new Client
            {
                Nom = Nom,
                Civilite = Civilite,
                Prenom = Prenom,
                Adresse = Adresse,
                Telephone = Telephone,
                Email = Email,
                DateNaissance = DateNaissance
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
            Console.WriteLine("Saisir le nom exact d'un client à modifier:");
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
                foreach (var Nom in client)
                {
                    Console.WriteLine(Nom);
                }
            }

        }
    }



    

}
