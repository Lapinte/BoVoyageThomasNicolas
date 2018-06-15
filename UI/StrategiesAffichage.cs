using BoVoyage.Framework.UI;
using BoVoyage_Thomas_Nicolas.Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyage_Thomas_Nicolas.UI
{
    public class StrategiesAffichage
    {
        public static List<InformationAffichage> GetStrategieDestination()
        {
            List<InformationAffichage> strategieAffichageDestinations =
            new List<InformationAffichage>
            {
                InformationAffichage.Creer<Destination>(x=>x.Id, "Id", 3),
                InformationAffichage.Creer<Destination>(x=>x.Continent, "Continent", 20),
                InformationAffichage.Creer<Destination>(x=>x.Pays, "Pays", 15),
                InformationAffichage.Creer<Destination>(x=>x.Region, "Région", 15),
                InformationAffichage.Creer<Destination>(x=>x.Description, "Description", 80)
            };
            return strategieAffichageDestinations;
        }

        public static List<InformationAffichage> GetStrategieAgence()
        {
            List<InformationAffichage> strategieAffichageAgences =
            new List<InformationAffichage>
            {
                InformationAffichage.Creer<AgenceVoyage>(x=>x.Id, "Id", 3),
                InformationAffichage.Creer<AgenceVoyage>(x=>x.Nom, "Nom", 30)
            };
            return strategieAffichageAgences;
        }

        public static List<InformationAffichage> GetStrategieVoyage()
        {
            List<InformationAffichage> strategieAffichageVoyages =
            new List<InformationAffichage>
            {
                InformationAffichage.Creer<Voyage>(x=>x.Id, "Id", 3),
                InformationAffichage.Creer<Voyage>(x=>x.Destination, "Destination", 20),
                InformationAffichage.Creer<Voyage>(x=>x.DateAller, "Date Aller", 15),
                InformationAffichage.Creer<Voyage>(x=>x.DateRetour, "Date Retour", 15),
                InformationAffichage.Creer<Voyage>(x=>x.PlacesDisponibles, "Places Disponibles", 20),
                InformationAffichage.Creer<Voyage>(x=>x.TarifToutCompris, "Tarif TTC par personne", 25)
            };
            return strategieAffichageVoyages;
        }

        public static List<InformationAffichage> GetStrategieClient()
        {
            List<InformationAffichage> strategieAffichageClients =
            new List<InformationAffichage>
            {
                InformationAffichage.Creer<Client>(x=>x.Id, "Id", 3),
                InformationAffichage.Creer<Client>(x=>x.Civilite, "Civilité", 10),
                InformationAffichage.Creer<Client>(x=>x.Nom, "Nom", 20),
                InformationAffichage.Creer<Client>(x=>x.Prenom, "Prénom", 20),
                InformationAffichage.Creer<Client>(x=>x.Adresse, "Adresse", 50),
                InformationAffichage.Creer<Client>(x=>x.Telephone, "TEL", 15),
                InformationAffichage.Creer<Client>(x=>x.Email, "Email", 50),
                InformationAffichage.Creer<Client>(x=>x.DateNaissance, "Date de naissance", 15)
            };
            return strategieAffichageClients;
        }

        public static List<InformationAffichage> GetStrategieParticipant()
        {
            List<InformationAffichage> strategieAffichageParticipants =
            new List<InformationAffichage>
            {
                InformationAffichage.Creer<Client>(x=>x.Id, "Id", 3),
                InformationAffichage.Creer<Client>(x=>x.Civilite, "Civilité", 10),
                InformationAffichage.Creer<Client>(x=>x.Nom, "Nom", 20),
                InformationAffichage.Creer<Client>(x=>x.Prenom, "Prénom", 20),
                InformationAffichage.Creer<Client>(x=>x.Adresse, "Adresse", 50),
                InformationAffichage.Creer<Client>(x=>x.Telephone, "TEL", 15),
                InformationAffichage.Creer<Client>(x=>x.DateNaissance, "Date de naissance", 15)
            };
            return strategieAffichageParticipants;
        }

        public static List<InformationAffichage> GetStrategieDossier()
        {
                List<InformationAffichage> strategieAffichageParticipants =
                new List<InformationAffichage>
                {
                InformationAffichage.Creer<DossierReservation>(x=>x.Id, "Id", 3),
                InformationAffichage.Creer<DossierReservation>(x=>x.EtatDossierReservation, "Etat Dossier Réservation", 10),
                InformationAffichage.Creer<DossierReservation>(x=>x.IdVoyage, "Id_Voyage", 20),
                InformationAffichage.Creer<DossierReservation>(x=>x.Client.Nom, "Client", 20),
                InformationAffichage.Creer<DossierReservation>(x=>x.AssuranceAnnulation, "Assurance Annulation", 20),
                InformationAffichage.Creer<DossierReservation>(x=>x.PrixTotal, "Prix TTC", 20),

                };
                return strategieAffichageParticipants;
        }
    }
}
