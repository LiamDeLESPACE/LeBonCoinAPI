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

        public Reglement(string reglementId, int reservationId)
        {
            ReglementId = reglementId;
            ReservationId = reservationId;
        }

        [Key]
        [Column("rgl_id")]
        public string ReglementId { get; set; }

        [Required]
        [Column("res_id")]
        public int ReservationId { get; set; }


        //Reservation
        [ForeignKey(nameof(ReservationId))]
        [InverseProperty(nameof(Reservation.ReglementsReservation))]
        public virtual Reservation ReservationReglement { get; set; }  

        public override bool Equals(object? obj)
        {
            return obj is Reglement reglement &&
                   ReglementId == reglement.ReglementId &&
                   ReservationId == reglement.ReservationId;
        }
    }
}
