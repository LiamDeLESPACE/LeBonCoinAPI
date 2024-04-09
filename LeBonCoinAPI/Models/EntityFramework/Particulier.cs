using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LeBonCoinAPI.Controllers;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_particulier_prt")]
    public class Particulier : Profil
    {
        public Particulier(string email, string hashMdp) : base(hashMdp)
        {
          Email = email;
        }

        public Particulier(string email, string hashMdp, string telephone, string civilite, string nom, string prenom, 
            DateTime datenaissance, int adresseId) : base(hashMdp, telephone)
        {
            Email = email;
            Civilite = civilite;
            Nom = nom;
            Prenom = prenom;
            DateNaissance = datenaissance;
            AdresseId = adresseId;

        }

        public Particulier() : base()
        {

        }

        [Required]
        [Column("prt_email")]
        [StringLength(100)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[a-z]+$")]
        public string Email { get; set; }

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

        [Column("adr_id")]
        public int? AdresseId { get; set; }

        //Adresse
        [ForeignKey(nameof(AdresseId))]
        [InverseProperty(nameof(Adresse.ParticuliersAdresse))]
        public virtual Adresse? AdresseParticulier { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Particulier particulier &&
                   ProfilId == particulier.ProfilId &&
                   HashMotDePasse == particulier.HashMotDePasse &&
                   Telephone == particulier.Telephone &&
                   Email == particulier.Email &&
                   Civilite == particulier.Civilite &&
                   Nom == particulier.Nom &&
                   Prenom == particulier.Prenom &&
                   DateNaissance == particulier.DateNaissance &&
                   AdresseId == particulier.AdresseId;
        }
    }
}
