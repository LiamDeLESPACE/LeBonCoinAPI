using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_j_annonceavis_anv")]
    public class AnnonceAvis
    {
        [Key]
        [Column("anv_idavis")]
        public int IdAvis { get; set; }

        [Key]
        [Column("anv_idannonce")]
        public int IdAnnonce;

        [ForeignKey(nameof(IdAnnonce))]
        [InverseProperty(nameof(Annonce.Avis))]
        public virtual Annonce AvisAnnoces { get; set; } = null!;

    }
}
