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

        }

        [Key]
        [Column("pho_id")]
        public int PhotoId { get; set; }


        [Column("prf_id")]
        public int? ProfilId { get; set; }


        [Column("ann_id")]
        public int? AnnonceId { get; set; }

        [Required]
        [Column("pho_url")]
        public string URL { get; set; } = null!;


        //Profil
        [ForeignKey(nameof(ProfilId))]
        [InverseProperty(nameof(Profil.PhotosProfil))]
        public Profil? ProfilPhoto { get; set; }

        //Annonce
        [ForeignKey(nameof(AnnonceId))]
        [InverseProperty(nameof(Annonce.PhotosAnnonce))]
        public Annonce? AnnoncePhoto { get; set; }



    }
}
