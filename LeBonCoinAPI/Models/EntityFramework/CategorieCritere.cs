using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_categoriecritere_ccr")]
    public class CategorieCritere
    {
        public CategorieCritere()
        {
            CriteresCategorie = new HashSet<Critere>();
        }

        [Key]
        [Column("ccr_id")]
        public int IdCategorieCritere { get; set; }

        [Required]
        [Column("ccr_libelle")]
        [StringLength(50)]
        public string Libelle { get; set; } = null!;

        [InverseProperty(nameof(Critere.CategorieDuCritere))]
        public virtual ICollection<Critere> CriteresCategorie { get; set; }

    }
}
