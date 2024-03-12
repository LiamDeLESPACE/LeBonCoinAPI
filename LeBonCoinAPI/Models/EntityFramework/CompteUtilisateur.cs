using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_compteutilisateur_cut")]
    public class CompteUtilisateur
    {
        [Key]
        [Column("cut_idcu")]
        public int IdCu { get; set; }

        [Key]
        [Column("cut_idparticulier")]
        public int IdParticulier { get; set; }

        [Key]
        [Column("cut_identreprise")]
        public int IdEntreprise { get; set; }

        [Key]
        [Column("cut_codeetatcompteutilisateur")]
        public int CodeEtatCompteUtilisateur { get; set; }

        [Required]
        [Column("cut_motdepasse")]
        [StringLength(100)]
        public string MotDePasse { get; set; } = null!;

        [Column("cut_telephone")]
        [StringLength(10)]
        public string? Telephone { get; set; }

        [Required]
        [Column("cut_telephoneverifier")]
        public bool TelephoneVerifier { get; set; }
    }
}
