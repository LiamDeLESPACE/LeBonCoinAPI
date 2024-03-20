using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_entreprise_ent")]
    public class Entreprise : Profil
    {
        public Entreprise()
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

        [Required]
        [Column("sct_id")]
        public int SecteurId { get; set; }

        [Required]
        [Column("ent_siret")]
        [StringLength(14)]
        public string Siret { get; set; } = null!;

        [Column("ent_nom")]
        [StringLength(100)]
        public string? Nom { get; set; }

        //Secteur Activite
        [ForeignKey(nameof(SecteurId))]
        [InverseProperty(nameof(SecteurActivite.EntreprisesSecteurActivite))]
        public virtual SecteurActivite SecteurEntreprise { get; set; } = null!;

    }
}
