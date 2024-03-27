using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_j_signale_sig")]
    public class Signale
    {
        public Signale()
        {
            
        }

        [Key]
        [Column("prf_id")]
        public int ProfilId { get; set; }

        [Key]
        [Column("ann_id")]
        public int AnnonceId { get; set; }


        //Annonce
        [ForeignKey(nameof(AnnonceId))]
        [InverseProperty(nameof(Annonce.SignalementsAnnonce))]
        public virtual Annonce AnnonceSignalement { get; set; } = null!;

        //Profil
        [ForeignKey(nameof(ProfilId))]
        [InverseProperty(nameof(Profil.SignalementsProfil))]
        public virtual Profil ProfilSignalement { get; set; } = null!;
    }
}
