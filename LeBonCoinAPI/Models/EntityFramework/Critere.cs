using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_critere_cri")]
    public class Critere
    {
        [Key]
        [Column("cri_idcritere")]
        public int IdCritere { get; set; }

        [Key]
        [Column("ccr_idcategoriecritere")]
        public int IdCategorieCritere { get; set; }

        [Required]
        [Column("cri_libellecritere")]
        public string libellecritere { get; set; } = null!;

        [InverseProperty(nameof(Contient.CritereContient))]
        public virtual ICollection<Contient> ContientsCritere { get; set; }

        [ForeignKey(nameof(IdCategorieCritere))]
        [InverseProperty(nameof(CategorieCritere.CriteresCategorie))]
        public virtual CategorieCritere CategorieCriteres { get; set; } = null!;

    }
}
