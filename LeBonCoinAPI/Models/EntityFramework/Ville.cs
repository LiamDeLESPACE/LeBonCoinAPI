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
            AdressesVille = new HashSet<Adresse>();
        }

        [Key]
        [Column("vil_codeinsee")]
        [StringLength(5)]
        public string CodeInsee { get; set; }

        [Required]
        [Column("dep_code")]
        [StringLength(3)]
        public string DepartementCode { get; set; }

        [Required]
        [Column("vil_nom")]
        [StringLength(100)]
        public string Nom { get; set; }

        [Required]
        [Column("vil_codepostal")]
        [StringLength(5)]
        public string CodePostal { get; set; }

        //Departement
        [ForeignKey(nameof(DepartementCode))]
        [InverseProperty(nameof(Departement.VillesDepartement))]
        public virtual Departement DepartementVille { get; set; } = null!;

        //Adresse
        [InverseProperty(nameof(Adresse.VilleAdresse))]
        public virtual ICollection<Adresse> AdressesVille { get; set; }

    }
}
