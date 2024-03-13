using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_particulier_par")]
    public class Particulier
    {
        [Key]
        [Column("par_idparticulier")]
        public int IdParticulier { get; set; }

        [Key]
        [Column("cut_id")]
        public int IdCu { get; set; }

        [Column("par_nom")]
        [StringLength(50)]
        public string? Nom { get; set; }

        [Column("par_prenom")]
        [StringLength(50)]
        public string? Prenom { get; set; }

        [Column("par_datenaissance", TypeName = "date")]
        public DateTime? DateNaissance { get; set; }

        [Column("par_sexe")]
        [StringLength(1)]
        public string? Sexe { get; set; }

        [Column("par_mail")]
        [StringLength(100)]
        public string? Mail { get; set; }

        [Required]
        [Column("par_pseudo")]
        [StringLength(50)]
        public string Pseudo { get; set; } = null!;

        [ForeignKey(nameof(IdCu))]
        [InverseProperty(nameof(CompteUtilisateur.ParticulierCompteUtilisateur))]
        public virtual CompteUtilisateur CompteUtilisateurParticulier { get; set; } = null!;
    }
}
