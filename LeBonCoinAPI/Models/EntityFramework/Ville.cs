using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_ville_vil")]
    public class Ville
    {
        public Ville()
        {
            RecherchesVille = new HashSet<Recherche>();
        }

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

        [ForeignKey(nameof(IdDepartement))]
        [InverseProperty(nameof(Departement.VillesDepartement))]
        public virtual Departement DepartementDeLaVille { get; set; } = null!;

        [InverseProperty(nameof(Recherche.VilleDeLaRecherche))]
        public virtual ICollection<Recherche> RecherchesVille { get; set; }


    }
}
