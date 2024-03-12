using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_departement_dep")]
    public class Departement
    {
        [Key]
        [Column("dep_iddepartement")]
        public int IdDepartement { get; set; }

        [Column("dep_code")]
        [StringLength(3)]
        public string Code { get; set; } = null!;

        [Column("dep_nom")]
        [StringLength(50)]
        public string Nom { get; set; } = null!;
    }
}
