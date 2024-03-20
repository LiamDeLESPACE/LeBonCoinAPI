using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_profil_prf")]
    public abstract class Profil
    {
        public Profil()
        {

        }

        [Key]
        [Column("prf_id")]
        public int ProfilId { get; set; }

        [Required]
        [Column("adr_id")]
        public string? AdresseId { get; set; }

        [Required]
        [Column("prf_hashmdp")]
        public string? HashMotDePasse { get; set; }

        [Column("prf_telephone")]
        [StringLength(10)]
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Le telephone doit contenir 10 chiffres")]
        public string? Telephone { get; set; }

        //Adresse
        [ForeignKey(nameof(ProfilId))]
        [InverseProperty(nameof(Adresse.ProfilsAdresse))]
        public virtual Profil AdresseProfil { get; set; } = null!;
    }
}
