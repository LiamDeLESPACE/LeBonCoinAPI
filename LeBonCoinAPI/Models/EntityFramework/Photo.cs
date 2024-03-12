using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_photo_pho")]
    public class Photo
    {
        [Key]
        [Column("pho_idphoto")]
        public int IdPhoto { get; set; }

        [Key]
        [Column("pho_idannonce")]
        public int IdAnnonce { get; set; }

        [Key]
        [Column("pho_idprofil")]
        public int IdProfil { get; set; }


        [Required]
        [Column("pho_donneephoto")]
        public string DonneePhoto { get; set; } = null!;
    }
}
