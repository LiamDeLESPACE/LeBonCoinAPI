using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_j_recherche_rch")]
    public class Recherche
    {
        public Recherche()
        {
            ChercheCritereDeLaCherche = new HashSet<ChercheCritere>();
        }

        [Key]
        [Column("rch_id")]
        public int IdRecherche { get; set; }


        [Key]
        [Column("cvo_id")]
        public int IdCapaciteVoyageur { get; set; }

        [Key]
        [Column("pro_id")]
        public int IdProfil { get; set; }


        [Required]
        [Column("vil_codeinsee")]
        [RegularExpression(@"^[0-9]{5}$", ErrorMessage = "Le code insee doit contenir 5 chiffres")]
        public string CodeInsee { get; set; } = null!;

        [InverseProperty(nameof(ChercheCritere.RechercheDuCritereCherche))]
        public virtual ICollection<ChercheCritere> ChercheCritereDeLaCherche { get; set; }

        [ForeignKey(nameof(IdCapaciteVoyageur))]
        [InverseProperty(nameof(CapaciteVoyageur.RecherchesCapacite))]
        public virtual CapaciteVoyageur CapaciteRecherche { get; set; } = null!;

    }
}
