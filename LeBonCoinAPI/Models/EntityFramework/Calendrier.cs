using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_j_calendrier_cln")]
    public class Calendrier
    {
        public Calendrier()
        {
            DatesCalendrier = new HashSet<Datec>();
        }

        [Key]
        [Column("cln_id")]
        public int CalendrierId { get; set; }

        [Required]
        [Column("ann_id")]
        public int AnnonceId { get; set; }

        [InverseProperty(nameof(Datec.CalendrierDate))]
        public virtual ICollection<Datec> DatesCalendrier { get; set; }

        [ForeignKey(nameof(AnnonceId))]
        [InverseProperty(nameof(Annonce.AnnonceCalendrier))]
        public virtual Annonce CalendrierAnnonce { get; set; } = null!;
    }
}
