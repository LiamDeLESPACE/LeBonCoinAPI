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
            AdressesVille = new HashSet<Adresse>();
        }

        [Key]
        [Column("vil_codeinsee")]
        [StringLength(11)]
        public string CodeInsee { get; set; } = null!;

        [Key]
        [Column("dep_id")]
        public int DepartementId { get; set; }

        [Required]
        [Column("vil_nom")]
        [StringLength(100)]
        public string Nom { get; set; } = null!;

        [Required]
        [Column("vil_codepostal")]
        [StringLength(5)]
        public string CodePostal { get; set; } = null!;

        //Departement
        [ForeignKey(nameof(DepartementId))]
        [InverseProperty(nameof(Departement.VillesDepartement))]
        public virtual Departement DepartementVille { get; set; } = null!;

        //Recherche
        [InverseProperty(nameof(Recherche.VilleRecherche))]
        public virtual ICollection<Recherche> RecherchesVille { get; set; }

        //Adresse
        [InverseProperty(nameof(Adresse.VilleAdresse))]
        public virtual ICollection<Adresse> AdressesVille { get; set; }

    }
}
