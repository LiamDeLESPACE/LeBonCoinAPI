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

        [Required]
        [Column("sct_id")]
        public int SecteurId { get; set; }

        [Required]
        [Column("ent_siret")]
        [MinLength(14)]
        [MaxLength(14)]
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
