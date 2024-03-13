using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_reservation_res")]
    public class Reservation
    {
        public Reservation()
        {
            ReglementsDeLaReservation = new HashSet<Reglement>();
            SignalementsReservation = new HashSet<Incident>();
        }

        [Key]
        [Column("res_id")]
        public int ReservationId { get; set; }

        [Key]
        [Column("ann_id")]
        public int AnnonceId { get; set; }

        [Key]
        [Column("loc_id")]
        public int LocataireId { get; set; }

        [Required]
        [Column("res_datearrivee")]
        public DateTime DateArrivee { get; set; }

        [Required]
        [Column("res_datedepart")]
        public DateTime DateDepart { get; set; }

        [Required]
        [Column("res_nombrevoyageur")]
        public int NombreVoyageur { get; set; }

        [ForeignKey(nameof(AnnonceId))]
        [InverseProperty(nameof(Annonce.ReservationsAnnonce))]
        public virtual Annonce AnnonceReservation { get; set; } = null!;

        [ForeignKey(nameof(LocataireId))]
        [InverseProperty(nameof(Locataire.ReservationsLocataire))]
        public virtual Locataire LocataireReservation { get; set; } = null!;

        [InverseProperty(nameof(Reglement.ReservationReglement))]
        public virtual ICollection<Reglement> ReglementsDeLaReservation { get; set; }

        [InverseProperty(nameof(Incident.ReservationSignale))]
        public virtual ICollection<Incident> SignalementsReservation { get; set; }

    }
}
