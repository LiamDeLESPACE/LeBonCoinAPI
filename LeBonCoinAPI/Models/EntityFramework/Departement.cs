using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_departement_dep")]
    public class Departement
    {
        [Key]
        [Column("dep_id")]
        public int IdDepartement { get; set; }

        [Required]
        [Column("dep_code")]
        [StringLength(3)]
        public string Code { get; set; } = null!;

        [Required]
        [Column("dep_nom")]
        [StringLength(50)]
        public string Nom { get; set; } = null!;
    }
}
