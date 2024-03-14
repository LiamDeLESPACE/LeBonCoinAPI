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


        //Critere
        [ForeignKey(nameof(CritereId))]
        [InverseProperty(nameof(Critere.RecherchesDuCritere))]
        public virtual Critere CritereCherche { get; set; } = null!;


        //Recherche
        [ForeignKey(nameof(RechercheId))]
        [InverseProperty(nameof(Recherche.CriteresDeLaRecherche))]
        public virtual Recherche RechercheDuCritereCherche { get; set; } = null!;

    }
}
