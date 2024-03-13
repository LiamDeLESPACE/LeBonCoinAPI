using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_profil_pro")]
    public class Profil
    {
        [Key]
        [Column("pro_id")]
        public int IdProfil { get; set; }

        [Key]
        [Column("prp_id")]
        public int IdProprietaire { get; set; }

        [Key]
        [Column("cut_id")]
        public int IdCu { get; set; }

        [Key]
        [Column("loc_id")]
        public int IdLocataire { get; set; }

        [Key]
        [Column("adr_id")]
        public int IdAdresse { get; set; }

        [Key]
        [Column("pho_id")]
        public int IdPhoto { get; set; }

        [Column("pro_tempsreponse")]
        public int TempsReponse { get; set; }

        [Required]
        [Column("pro_datemembre")]
        public DateTime DateMembre { get; set; }

        [Required]
        [Column("pro_recommandation")]
        public bool Recommandation { get; set; }

        [InverseProperty(nameof(ProfilAvis.IdProfil))]
        public virtual ICollection<Avis> ProfilCorrespondant { get; set; }
    }
}
