using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_ville_vil")]
    public class Ville
    {
        public Ville(string codeINSEE, string depCode, string nom, string codePostal)  : this()
        {            
            CodeInsee = codeINSEE;
            DepartementCode = depCode;
            Nom = nom;
            CodePostal = codePostal;
        }

        public Ville()
        {
            AdressesVille = new HashSet<Adresse>();
        }

        [Key]
        [Column("vil_codeinsee")]
        [StringLength(5)]
        [RegularExpression("^[0-9]{5}$", ErrorMessage = "Un code Insee compte 5 chiffres")]
        public string CodeInsee { get; set; }

        [Required]
        [Column("dep_code")]
        [StringLength(3)]
        [RegularExpression("^[0-9]{2,3} | [0-9]{1,2}[ABDM] $", ErrorMessage ="Le code de département est composé de 2 chiffres, " +
            "3 chiffres, 1 chiffre et une lettre (Corse) ou 2 chiffres et une lettre (Lyon)")]
        public string DepartementCode { get; set; }

        [Required]
        [Column("vil_nom")]
        [StringLength(100)]
        public string Nom { get; set; }

        [Required]
        [Column("vil_codepostal")]
        [StringLength(5)]
        [RegularExpression("^[0-9]{5}$", ErrorMessage = "Un code postal compte 5 chiffres")]
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
