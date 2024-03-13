using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_avis_avi")]
    public class Avis
    {
        [Key]
        [Column("avi_id")]
        public int IdAvis { get; set; }

        [Key]
        [Column("pro_id")]
        public int IdProfil { get; set; }



        [StringLength(100)]
        [Column("avi_titre")]
        public string? Titre { get; set; }


        [Column("avi_description")]
        public string? DescriptionAvis { get; set; }

        [Required]
        [Column("avi_note")]
        [RegularExpression(@"^[1-5]{1}$", ErrorMessage = "La note s doit être un chiffre compris entre 1 et 5.")]
        public int Note { get; set; }

        [InverseProperty(nameof(ProfilAvis.IdAvis))]
        public virtual ICollection<Avis> AvisProfil { get; set; }
    }
}
