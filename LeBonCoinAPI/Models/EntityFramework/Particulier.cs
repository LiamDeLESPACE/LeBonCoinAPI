using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_particulier_prt")]
    public class Particulier : Profil
    {
        public Particulier()
        {
          
        }

        [Key]
        [Column("prf_id")]
        public int ProfilId { get; set; }

        [Required]
        [Column("adr_id")]
        public string AdresseId { get; set; }

        [Required]
        [Column("prf_hashmdp")]
        public string HashMotDePasse { get; set; }

        [Column("prf_telephone")]
        [StringLength(10)]
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Le telephone doit contenir 10 chiffres")]
        public string? Telephone { get; set; }

        [Column("prt_email")]
        [StringLength(100)]
        public string? Email { get; set; }

        [Column("prt_civilite")]
        [StringLength(1)]
        public string? Civilite { get; set; }

        [Column("prt_nom")]
        [StringLength(50)]
        public string? Nom { get; set; }

        [Column("prt_prenom")]
        [StringLength(50)]
        public string? Prenom { get; set; }

        [Column("prt_datenaissance", TypeName = "date")]
        public DateTime? DateNaissance { get; set; }

    }
}
