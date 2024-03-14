using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_avis_avi")]
    public class Avis
    {
        public Avis()
        {
            AnnoncesAvis = new HashSet<AnnonceAvis>();
            ProfilsAvis = new HashSet<ProfilAvis>();
        }
        [Key]
        [Column("avi_id")]
        public int AvisId { get; set; }

        [Key]
        [Column("pro_id")]
        public int ProfilId { get; set; }



        [StringLength(100)]
        [Column("avi_titre")]
        public string? Titre { get; set; }


        [Column("avi_description")]
        public string? DescriptionAvis { get; set; }

        [Required]
        [Column("avi_note")]
        [RegularExpression(@"^[1-5]{1}$", ErrorMessage = "La note s doit être un chiffre compris entre 1 et 5.")]
        public int Note { get; set; }

        //Profil de l'Avis
        [ForeignKey(nameof(ProfilId))]
        [InverseProperty(nameof(Profil.AvisDepose))]
        public virtual Profil ProfilDeLAvis { get; set; } = null!;

        //liste ProfilAvis
        [InverseProperty(nameof(ProfilAvis.AvisProfil))]
        public virtual ICollection<ProfilAvis> ProfilsAvis { get; set; }

        //liste AnnonceAvis
        [InverseProperty(nameof(AnnonceAvis.AvisAnnonceAvis))]
        public virtual ICollection<AnnonceAvis> AnnoncesAvis { get; set; }


    }
}
