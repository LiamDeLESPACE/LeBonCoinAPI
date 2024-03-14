using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_entreprise_ent")]
    public class Entreprise
    {
        public Entreprise()
        {
            ComptesUtilisateursEntreprise = new HashSet<CompteUtilisateur>();
        }

        [Key]
        [Column("ent_id")]
        public int EntrepriseId { get; set; }

        [Required]
        [Column("ent_siret")]
        [StringLength(14)]
        public string Siret { get; set; } = null!;

        [Column("ent_nom")]
        [StringLength(50)]
        public string? Nom { get; set; }

        [Column("ent_secteuractivite")]
        [StringLength(50)]
        public string? SecteurActivite { get; set; }

        //CompteUtilisateur
        [InverseProperty(nameof(CompteUtilisateur.EntrepriseCompteUtilisateur))]
        public virtual ICollection<CompteUtilisateur> ComptesUtilisateursEntreprise { get; set; }

    }
}
