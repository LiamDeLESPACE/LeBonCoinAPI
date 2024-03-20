using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_reglement_rgl")]
    public class Reglement
    {
        public Reglement()
        {
            
        }

        [Key]
        [Column("rgl_id")]
        [StringLength(50)]
        public string? ReglementId { get; set; }

        [Required]
        [Column("res_id")]
        public int ReservationId { get; set; }


        //Reservation
        [ForeignKey(nameof(ReservationId))]
        [InverseProperty(nameof(Reservation.ReglementsReservation))]
        public virtual Reservation ReservationReglement { get; set; } = null!;
    }
}
