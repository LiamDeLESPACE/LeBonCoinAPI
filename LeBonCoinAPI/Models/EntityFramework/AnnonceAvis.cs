using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_j_annonceavis_anv")]
    public class AnnonceAvis
    {
        [Key]
        [Column("anv_id")]
        public int AvisId { get; set; }

        [Key]
        [Column("ann_id")]
        public int AnnonceId;

        [ForeignKey(nameof(AnnonceId))]
        [InverseProperty(nameof(Annonce.AvisAnnonce))]
        public virtual Annonce AnnonceAvi { get; set; } = null!;

    }
}
