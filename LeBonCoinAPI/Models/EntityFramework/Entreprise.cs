using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_entreprise_ent")]
    public class Entreprise : Profil
    {
        public Entreprise(int secteurId, string siret, int adresseId, string hashMdp) : base(hashMdp)
        {
            SecteurId = secteurId;
            Siret = siret;
            AdresseId = adresseId;
        }

        public Entreprise(int secteurId, string siret, int adresseId, string hashMdp, string telephone) : base(hashMdp, telephone)
        {
            SecteurId = secteurId;
            Siret = siret;
            AdresseId = adresseId;
        }

        public Entreprise(int secteurId, string siret, int adresseId, string hashMdp, string nom, string telephone): this(secteurId, siret, adresseId, hashMdp, telephone) 
        {
            Nom = nom;
        }

        [Required]
        [Column("sct_id")]
        public int SecteurId { get; set; }

        [Required]
        [Column("ent_siret")]
        [StringLength(14)]
        [RegularExpression("^[0-9]{14}$", ErrorMessage = "Un SIRET contient 14 chiffres (9 chiffres du SIREN + 5 chiffres de l'etablissement)")]
        public string Siret { get; set; }

        [Required]
        [Column("adr_id")]
        public int AdresseId { get; set; }


        [Column("ent_nom")]
        [StringLength(100)]
        public string? Nom { get; set; }

        //Secteur Activite
        [ForeignKey(nameof(SecteurId))]
        [InverseProperty(nameof(SecteurActivite.EntreprisesSecteurActivite))]
        public virtual SecteurActivite SecteurActiviteEntreprise { get; set; } = null!;

        //Adresse
        [ForeignKey(nameof(AdresseId))]
        [InverseProperty(nameof(Adresse.EntreprisesAdresse))]
        public virtual Adresse AdresseEntreprise { get; set; } = null!;

    }
}
