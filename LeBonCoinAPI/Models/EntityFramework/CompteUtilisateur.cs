using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_compteutilisateur_cut")]
    public class CompteUtilisateur
    {
        [Key]
        [Column("cut_id")]
        public int IdCu { get; set; }

        [Key]
        [Column("par_id")]
        public int IdParticulier { get; set; }

        [Key]
        [Column("ent_id")]
        public int IdEntreprise { get; set; }

        [Key]
        [Column("eta_codeetatcompteutilisateur")]
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

        [InverseProperty(nameof(Profil.CompteUti))]
        public virtual ICollection<Profil> Compte { get; set; }
    }
}
