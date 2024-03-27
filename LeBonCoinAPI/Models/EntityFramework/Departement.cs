using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_departement_dep")]
    public class Departement
    {
        public Departement(string codeDep, string nomDep)
        {
            VillesDepartement = new HashSet<Ville>();
            DepartementCode = codeDep;
            Nom = nomDep;
        }

        [Key]
        [Column("dep_code")]
        [StringLength(3)]
        [RegularExpression("^[0-9]{2,3} | [0-9]{1,2}[ABDM] $", ErrorMessage = "Le code de département est composé de 2 chiffres, " +
            "3 chiffres, 1 chiffre et une lettre (Corse) ou 2 chiffres et une lettre (Lyon)")]
        public string DepartementCode { get; set; }

        [Required]
        [Column("dep_nom")]
        [StringLength(50)]
        public string Nom { get; set; }

        //Ville
        [InverseProperty(nameof(Ville.DepartementVille))]
        public virtual ICollection<Ville> VillesDepartement { get; set; }

    }
}
