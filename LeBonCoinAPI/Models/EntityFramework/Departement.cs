using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_departement_dep")]
    public class Departement
    {
        public Departement()
        {
            VillesDepartement = new HashSet<Ville>();
        }

        [Key]
        [Column("dep_id")]
        public int DepartementId { get; set; }

        [Required]
        [Column("dep_code")]
        [StringLength(3)]
        public string Code { get; set; } = null!;

        [Required]
        [Column("dep_nom")]
        [StringLength(50)]
        public string Nom { get; set; } = null!;

        [InverseProperty(nameof(Ville.DepartementDeLaVille))]
        public virtual ICollection<Ville> VillesDepartement { get; set; }

    }
}
