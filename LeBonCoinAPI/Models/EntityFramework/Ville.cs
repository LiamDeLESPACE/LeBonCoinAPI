using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_ville_vil")]
    public class Ville
    {
        [Key]
        [Column("vil_codeinsee")]
        [StringLength(11)]
        public string CodeInsee { get; set; } = null!;

        [Key]
        [Column("dep_id")]
        public int IdDepartement { get; set; }

        [Required]
        [Column("vil_nom")]
        [StringLength(100)]
        public string Nom { get; set; } = null!;

        [Required]
        [Column("vil_codepostal")]
        [StringLength(5)]
        public string CodePostal { get; set; } = null!;
    }
}
