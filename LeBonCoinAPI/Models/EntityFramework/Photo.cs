using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_photo_pho")]
    public class Photo
    {
        [Key]
        [Column("pho_id")]
        public int IdPhoto { get; set; }

        [Key]
        [Column("ann_id")]
        public int IdAnnonce { get; set; }

        [Key]
        [Column("pro_id")]
        public int IdProfil { get; set; }


        [Required]
        [Column("pho_donneephoto")]
        public string DonneePhoto { get; set; } = null!;
    }
}
