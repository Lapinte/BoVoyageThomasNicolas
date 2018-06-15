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
            this.menu.AjouterElement(new ElementMenu("4", "Modifier le statut d'un Dossier")
            {
                FonctionAExecuter = this.ModifierStatutDossier
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
            ConsoleHelper.AfficherEntete("Dossiers existants");

            using (var bd = Application.GetBaseDonnees())
            {
                var listeDossiers = bd.DossierReservations.ToList();
                ConsoleHelper.AfficherListe(listeDossiers, StrategiesAffichage.GetStrategieDossier());
            }

        }

        private void AjouterDossier()
        {
            ConsoleHelper.AfficherEntete("Ajouter un Dossier");

            using (var bd = Application.GetBaseDonnees())
            {
                var dossier = new DossierReservation();

                //Choix d'un voyage
                this.Application.ModuleGestionVoyages.AfficherVoyages();
                var idVoyage = ConsoleSaisie.SaisirEntierObligatoire("Choisissez un Voyage (ID) : ");
                if (!bd.Voyages.Any(x => x.Id == idVoyage))
                {
                    ConsoleHelper.AfficherMessageErreur("Ce Voyage n'existe pas, retour au menu");
                    return;
                }
                dossier.Voyage = bd.Voyages.Single(x => x.Id == idVoyage);

                //Choix d'un client
                this.Application.ModuleGestionClients.AfficherClients();
                var idClient = ConsoleSaisie.SaisirEntierObligatoire("Choisissez un Client (ID) : ");
                if (!bd.Clients.Any(x => x.Id == idClient))
                {
                    ConsoleHelper.AfficherMessageErreur("Ce Client n'existe pas, retour au menu");
                    return;
                }
                dossier.Client = bd.Clients.Single(x => x.Id == idClient);

                //Demande numéro carte bancaire
                dossier.NumeroCarteBancaire = ConsoleSaisie.SaisirEntierObligatoire("Entrez votre numéro de carte bancaire : ");

                //Demande d'une assurance annulation
                var annulation = ConsoleSaisie.SaisirEntierObligatoire("Voulez-vous une assurance annulation? (200€) 1=Oui 2=Non : ");
                if (annulation != 1 && annulation != 2)
                {
                    ConsoleHelper.AfficherMessageErreur("Choix impossible, retour au menu");
                    return;
                }
                if (annulation == 1)
                {
                    dossier.AssuranceAnnulation = true;
                }
                else
                {
                    dossier.AssuranceAnnulation = false;
                }

                //Création d'une liste de participants 
                var listeParticipants = new List<Participant>();

                //Conditions de sortie de la boucle d'ajout de participant
                var ajoutOuiOuNon = ConsoleSaisie.SaisirEntierObligatoire("Ajouter un participant ? 1=Oui  2=Non");
                while (listeParticipants.Count() < 9 && ajoutOuiOuNon == 1)
                {
                    //Ajout d'un participant à la liste
                    if (ajoutOuiOuNon != 1 && ajoutOuiOuNon != 2)
                    {
                        ConsoleHelper.AfficherMessageErreur("Choix impossible, retour au menu");
                        return;
                    }
                    Participant participant = this.Application.ModuleGestionClients.AjouterParticipant();
                    participant.IdReservation = dossier.Id;
                    listeParticipants.Add(participant);
                    bd.Participants.Add(participant);
                    ajoutOuiOuNon = ConsoleSaisie.SaisirEntierObligatoire("Ajouter un participant ? 1=Oui  2=Non");

                }
                dossier.Participants = listeParticipants;

                //Création du statut en attente
                dossier.EtatDossierReservation = "En attente";

                //Calcul du prix total avec prise en compte de l'assurance annulation et de l'age des participants
                var tarif = dossier.Voyage.TarifToutCompris;
                decimal nbAdultes = dossier.Participants.Count(x => x.Age > 12);
                decimal nbEnfants = dossier.Participants.Count(x => x.Age <= 12);
                if (dossier.AssuranceAnnulation == true)
                {
                    dossier.PrixTotal = (tarif * (nbAdultes + (nbEnfants * 0.6M))) + 200;
                }
                else
                {
                    dossier.PrixTotal = tarif * (nbAdultes + (nbEnfants * 0.6M));
                }

                bd.DossierReservations.Add(dossier);
                bd.SaveChanges();
            }
        }

        private void SupprimerDossier()
        {
            ConsoleHelper.AfficherEntete("Supprimer un Dossier:");

            AfficherDossiers();
            var idDossier = ConsoleSaisie.SaisirEntierObligatoire("Dossier à supprimer (Id): ");
            using (var bd = Application.GetBaseDonnees())
            {
                if (!bd.DossierReservations.Any(x => x.Id == idDossier))
                {
                    ConsoleHelper.AfficherMessageErreur("Ce Dossier n'existe pas, retour au menu");
                    return;
                }
                var dossier = bd.DossierReservations.Single(x => x.Id == idDossier);
                bd.DossierReservations.Remove(dossier);
                bd.SaveChanges();
            }
        }
        private void ModifierStatutDossier()
        {
            ConsoleHelper.AfficherEntete("Modifier un Dossier:");

            AfficherDossiers();
            var id = ConsoleSaisie.SaisirEntierObligatoire("Id à modifier : ");
            
            using (var bd = Application.GetBaseDonnees())
            {
                var dossier = bd.DossierReservations.Single(x => x.Id == id);
                string nouveauStatut = ConsoleSaisie.SaisirChaineObligatoire("Saisir un nouveau statut:");

                dossier.EtatDossierReservation = nouveauStatut;

                bd.SaveChanges();
            }
        }
    }
}
