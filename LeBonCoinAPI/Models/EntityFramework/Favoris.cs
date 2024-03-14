using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_j_favoris_fav")]
    public class Favoris
    {
        [Key]
        [Column("pro_id")]
        public int ProfilId { get; set; }

        [Key]
        [Column("ann_id")]
        public int AnnonceId { get; set; }


        //Annonce
        [ForeignKey(nameof(AnnonceId))]
        [InverseProperty(nameof(Annonce.Fav))]
        public virtual Annonce AnnonceFavoris { get; set; } = null!;
    }
}
