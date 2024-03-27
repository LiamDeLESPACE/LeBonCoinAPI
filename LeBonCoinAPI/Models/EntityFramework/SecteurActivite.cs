using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_secteuractivite_sct")]
    public class SecteurActivite
    {
        public SecteurActivite()
        {
            EntreprisesSecteurActivite = new HashSet<Entreprise>();
        }
        [Key]
        [Column("sct_id")]
        public int SecteurId { get; set; }

        [Required]
        [Column("sct_nomsecteur")]
        [StringLength(50)]
        public string NomSecteur { get; set; }

        //Entreprises
        [InverseProperty(nameof(Entreprise.SecteurActiviteEntreprise))]
        public virtual ICollection<Entreprise> EntreprisesSecteurActivite{ get; set; }
    }
}
