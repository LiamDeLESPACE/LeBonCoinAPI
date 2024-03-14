using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_incident_inc")]
    public class Incident
    {
        [Key]
        [Column("inc_id")]
        public int IncidentId { get; set; }

        [Key]
        [Column("loc_id")]
        public int LocataireId { get; set; }

        [Key]
        [Column("res_id")]
        public int ReservationId { get; set; }

        [Required]
        [Column("inc_description")]
        public string Description { get; set; } = null!;

        [Column("inc_justification")]
        public string? Justification { get; set; }

        [Required]
        [Column("inc_codeetat")]
        public int CodeEtat { get; set; }

        //Locataire
        [ForeignKey(nameof(LocataireId))]
        [InverseProperty(nameof(Locataire.SignalementsLocataire))]
        public virtual Locataire LocataireSignalant { get; set; } = null!;

        //Reservation
        [ForeignKey(nameof(ReservationId))]
        [InverseProperty(nameof(Reservation.SignalementsReservation))]
        public virtual Reservation ReservationSignale { get; set; } = null!;
    }
}
