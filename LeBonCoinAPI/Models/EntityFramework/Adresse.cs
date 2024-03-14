using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_adresse_adr")]
    public class Adresse
    {
        public Adresse()
        {
            ProfilsAdresse = new HashSet<Profil>();
            AnnoncesAdresse = new HashSet<Annonce>();
        }
        [Key]
        [Column("adr_id")]
        public int AdresseId { get; set; }

        [Key]
        [Column("vil_codeinsee")]
        [StringLength(11)]
        public string CodeInsee { get; set; } = null!;

        [Required]
        [Column("adr_rue")]
        [StringLength(100)]
        public string Rue { get; set; } = null!;

        [Required]
        [Column("adr_numero")]
        public int Numero { get; set; }

        [Required]
        [Column("adr_pays")]
        [StringLength(50)]
        public string Pays { get; set; } = null!;

        //Profil
        [InverseProperty(nameof(Profil.AdresseProfil))]
        public virtual ICollection<Profil> ProfilsAdresse { get; set; }

        //Annonce
        [InverseProperty(nameof(Annonce.AdresseAnnonce))]
        public virtual ICollection<Annonce> AnnoncesAdresse { get; set; }

        //Ville
        [ForeignKey(nameof(CodeInsee))]
        [InverseProperty(nameof(Ville.AdressesVille))]
        public virtual Ville VilleAdresse { get; set; } = null!;

    }
}
