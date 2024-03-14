using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_j_contient_cnt")]
    public class Contient
    {
        [Key]
        [Column("cri_idcritere")]
        public int CritereId { get; set; }

        [Key]
        [Column("ann_id")]
        public int AnnonceId { get; set; }

        //Annonce
        [ForeignKey(nameof(AnnonceId))]
        [InverseProperty(nameof(Annonce.CriteresAnnonce))]
        public virtual Annonce AnnonceCritere { get; set; } = null!;

        //Critere
        [ForeignKey(nameof(CritereId))]
        [InverseProperty(nameof(Critere.AnnocesCritere))]
        public virtual Critere CritereAnnonce { get; set; } = null!;

    }
}