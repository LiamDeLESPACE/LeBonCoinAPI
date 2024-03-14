using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_photo_pho")]
    public class Photo
    {
        public Photo()
        {
            ProfilsPhoto = new HashSet<Profil>();
        }
        [Key]
        [Column("pho_id")]
        public int PhotoId { get; set; }

        [Column("ann_id")]
        public int AnnonceId { get; set; }

        [Required]
        [Column("pho_donneephoto")]
        public string DonneePhoto { get; set; } = null!;

        //Profil
        [InverseProperty(nameof(Profil.PhotoProfil))]
        public virtual ICollection<Profil> ProfilsPhoto { get; set; }

        //Annonce
        [ForeignKey(nameof(AnnonceId))]
        [InverseProperty(nameof(Annonce.PhotosAnnonce))]
        public virtual Annonce AnnoncePhoto { get; set; }



    }
}
