using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_j_cherchercritere_cri")]
    public class ChercheCritere
    {
        [Key]
        [Column("cri_id")]
        public int CritereId { get; set; }

        [Key]
        [Column("rch_id")]
        public int RechercheId { get; set; }

        [ForeignKey(nameof(CritereId))]
        [InverseProperty(nameof(Critere.ChercheCriteres))]
        public virtual Critere CritereCherche { get; set; } = null!;

        [ForeignKey(nameof(RechercheId))]
        [InverseProperty(nameof(Recherche.ChercheCriteresRecherche))]
        public virtual Recherche RechercheDuCritereCherche { get; set; } = null!;

    }
}
