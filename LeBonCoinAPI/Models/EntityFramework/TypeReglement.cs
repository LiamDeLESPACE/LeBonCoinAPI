using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_typereglement_tyr")]
    public class TypeReglement
    {
        [Key]
        [Column("tyr_id")]
        public int IdTypeReglement { get; set; }

        [Required]
        [Column("tyr_libelle")]
        [StringLength(50)]
        public string Libelle { get; set; } = null!;
    }
}
