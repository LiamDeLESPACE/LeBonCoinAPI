using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_typereglement_tyr")]
    public class TypeReglement
    {
        public TypeReglement()
        {
            ReglementsDuType = new HashSet<Reglement>();
        }
        [Key]
        [Column("tyr_id")]
        public int IdTypeReglement { get; set; }

        [Required]
        [Column("tyr_libelle")]
        [StringLength(50)]
        public string Libelle { get; set; } = null!;

        [InverseProperty(nameof(Reglement.TypeDeReglement))]
        public virtual ICollection<Reglement> ReglementsDuType { get; set; }
    }
}
