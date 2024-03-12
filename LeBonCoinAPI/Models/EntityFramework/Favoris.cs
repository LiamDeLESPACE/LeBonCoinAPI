using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_j_favoris_fav")]
    public class Favoris
    {
        [Key]
        [Column("fav_idprofil")]
        public int Idprofil { get; set; }

        [Key]
        [Column("fav_idannonce")]
        public int IdAnnonce;
    }
}
