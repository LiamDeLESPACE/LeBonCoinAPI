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
            ReglementsReservation = new HashSet<Reglement>();
            
        }

        [Key]
        [Column("res_id")]
        public int ReservationId { get; set; }

        [Required]
        [Column("ann_id")]
        public int AnnonceId { get; set; }

        [Required]
        [Column("prf_id")]
        public int ProfilId { get; set; }

        [Required]
        [Column("res_datearrivee")]
        public DateTime DateArrivee { get; set; }

        [Required]
        [Column("res_datedepart")]
        public DateTime DateDepart { get; set; }

        [Required]
        [Column("res_nombrevoyageur")]
        public int NombreVoyageur { get; set; }

        [Required]
        [Column("res_nom")]
        [StringLength(50)]
        public string? Nom { get; set; }

        [Required]
        [Column("res_prenom")]
        [StringLength(50)]
        public string? Prenom { get; set; }

        [Required]
        [Column("res_telephone")]
        [StringLength(10)]
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Le telephone doit contenir 10 chiffres")]
        public string? Telephone { get; set; }


        //Reglement
        [InverseProperty(nameof(Reglement.ReservationReglement))]
        public virtual ICollection<Reglement> ReglementsReservation { get; set; }

        //Profil
        [ForeignKey(nameof(ProfilId))]
        [InverseProperty(nameof(Profil.ReservationsProfil))]
        public virtual Profil ProfilReservation { get; set; } = null!;

        //Annonce
        [ForeignKey(nameof(AnnonceId))]
        [InverseProperty(nameof(Annonce.ReservationsAnnonce))]
        public virtual Annonce AnnonceReservation { get; set; } = null!;
    }
}
