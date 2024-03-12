using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_datec_dtc")]
    public class Datec
    {
        [Key]
        [Column("dtc_id", TypeName = "date")]
        public DateTime? Date { get; set; }

        [Required]
        [Column("cln_id")]
        public int CalendrierId { get; set; }

        [Column("dtc_prix")]
        public float? Prix { get; set; }

        [Column("dtc_codeetat")]
        public bool CodeEtat { get; set; }

        [ForeignKey(nameof(CalendrierId))]
        [InverseProperty(nameof(Calendrier.DatesCalendrier))]
        public virtual Calendrier CalendrierDate { get; set; } = null!;
    }
}
