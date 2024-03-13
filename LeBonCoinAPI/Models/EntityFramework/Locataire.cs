using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.CodeAnalysis;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_locataire_loc")]
    public class Locataire
    {
        public Locataire()
        {
            ReferencesCarteLocataire = new HashSet<ReferenceCarteBancaire>();
            SignalementsLocataire = new HashSet<Incident>();
            ProfilsLocataire = new HashSet<Profil>();
            ReservationsLocataire = new HashSet<Reservation>();
        }

        [Key]
        [Column("loc_id")]
        public int IdLocataire { get; set; }

        [Key]
        [Column("pro_id")]
        public int IdProfil { get; set; }

        //ReferenceCarteBancaire
        [InverseProperty(nameof(ReferenceCarteBancaire.LocataireReferent))]
        public virtual ICollection<ReferenceCarteBancaire> ReferencesCarteLocataire { get; set; }

        //Incident
        [InverseProperty(nameof(Incident.LocataireSignalant))]
        public virtual ICollection<Incident> SignalementsLocataire { get; set; }

        //Profil
        [InverseProperty(nameof(Profil.LocataireProfil))]
        public virtual ICollection<Profil> ProfilsLocataire { get; set; }

        //Reservation
        [InverseProperty(nameof(Reservation.LocataireReservation))]
        public virtual ICollection<Reservation> ReservationsLocataire { get; set; }
    }
}
