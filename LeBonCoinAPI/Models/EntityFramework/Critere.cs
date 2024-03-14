using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_critere_cri")]
    public class Critere
    {
        public Critere()
        {
            ContientsCritere = new HashSet<Contient>();
            RecherchesDuCritere = new HashSet<ChercheCritere>();
        }

        [Key]
        [Column("cri_idcritere")]
        public int CritereId { get; set; }

        [Key]
        [Column("ccr_idcategoriecritere")]
        public int CategorieCritereId { get; set; }

        [Required]
        [Column("cri_libellecritere")]
        public string libellecritere { get; set; } = null!;

        //Contient
        [InverseProperty(nameof(Contient.CritereContient))]
        public virtual ICollection<Contient> ContientsCritere { get; set; }

        //CategorieCritere
        [ForeignKey(nameof(CategorieCritereId))]
        [InverseProperty(nameof(CategorieCritere.CriteresCategorie))]
        public virtual CategorieCritere CategorieDuCritere { get; set; } = null!;

        //ChercheCritere
        [InverseProperty(nameof(ChercheCritere.CritereCherche))]
        public virtual ICollection<ChercheCritere> RecherchesDuCritere { get; set; }

    }
}
