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
        public int IdIncident { get; set; }

        [Key]
        [Column("loc_id")]
        public int IdLocataire { get; set; }

        [Key]
        [Column("res_id")]
        public int IdReservation { get; set; }

        [Required]
        [Column("inc_description")]
        public string Description { get; set; } = null!;

        [Column("inc_justification")]
        public string? Justification { get; set; }

        [Required]
        [Column("inc_codeetat")]
        public int CodeEtat { get; set; }

        [ForeignKey(nameof(IdLocataire))]
        [InverseProperty(nameof(Locataire.SignalementsLocataire))]
        public virtual Locataire LocataireSignalant { get; set; } = null!;
    }
}
