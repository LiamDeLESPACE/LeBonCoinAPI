using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_j_reglement_rgl")]
    public class Reglement
    {

        [Key]
        [Column("rgl_numtransaction")]
        [StringLength(50)]
        public string? NumeroTransaction { get; set; }

        [Key]
        [Column("tyr_id")]
        public int TypeReglementId { get; set; }

        [Key]
        [Column("res_id")]
        public int ReservationId { get; set; }

        //TypeReglement
        [ForeignKey(nameof(TypeReglementId))]
        [InverseProperty(nameof(TypeReglement.ReglementsDuType))]
        public virtual TypeReglement TypeDeReglement { get; set; } = null!;

        //Reservation
        [ForeignKey(nameof(ReservationId))]
        [InverseProperty(nameof(Reservation.ReglementsDeLaReservation))]
        public virtual Reservation ReservationReglement { get; set; } = null!;
    }
}
