using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_reservation_res")]
    public class Reservation
    {
        [Key]
        [Column("res_id")]
        public int IdReservation { get; set; }

        [Key]
        [Column("ann_id")]
        public int IdAnnonce { get; set; }

        [Key]
        [Column("loc_id")]
        public int IdLocataire { get; set; }

        [Required]
        [Column("res_datearrivee")]
        public DateTime DateArrivee { get; set; }

        [Required]
        [Column("res_datedepart")]
        public DateTime DateDepart { get; set; }

        [Required]
        [Column("res_nombrevoyageur")]
        public int NombreVoyageur { get; set; }

        [ForeignKey(nameof(IdAnnonce))]
        [InverseProperty(nameof(Annonce.ReservationsAnnonce))]
        public virtual Annonce AnnonceReservation { get; set; } = null!;
    }
}
