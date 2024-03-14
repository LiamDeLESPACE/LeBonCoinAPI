using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_referencecartebancaire_rcb")]
    public class ReferenceCarteBancaire
    {
        [Key]
        [Column("rcb_id")]
        public int ReferenceCarteBancaireId { get; set; }

        [Required]
        [Column("loc_id")]
        public int LocataireId { get; set; }

        [Required]
        [Column("rcb_code")]
        [StringLength(16)]
        [RegularExpression(@"^[0-9]{16}$", ErrorMessage = "Le code de carte bancaire doit contenir 16 chiffres")]
        public string? Code { get; set; }

        [Required]
        [Column("rcb_expiration", TypeName = "date")]
        public DateTime? Expiration { get; set; }

        [Required]
        [Column("rcb_prenom")]
        [StringLength(50)]
        public string? Prenom { get; set; }

        [Required]
        [Column("rcb_nom")]
        [StringLength(50)]
        public string? Nom { get; set; }

        [Required]
        [Column("rcb_signature")]
        [StringLength(3)]
        [RegularExpression(@"^[0-9]{3}$", ErrorMessage = "La signature doit contenir 3 chiffres")]
        public string? Signature { get; set; }

        [Column("rcb_estconcervee")]
        public bool EstConcervee { get; set; }

        //Locataire
        [ForeignKey(nameof(LocataireId))]
        [InverseProperty(nameof(Locataire.ReferencesCarteLocataire))]
        public virtual Locataire LocataireReferent { get; set; } = null!;
    }
}
