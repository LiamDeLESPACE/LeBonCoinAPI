using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_j_profilavis_pra")]
    public class ProfilAvis
    {
        [Key]
        [Column("avi_id")]
        public int AvisId { get; set; }

        [Key]
        [Column("pro_id")]
        public int ProfilId;

        //Avis
        [ForeignKey(nameof(AvisId))]
        [InverseProperty(nameof(Avis.AvisTypeProfilAvis))]
        public virtual Avis AviAvisProfil { get; set; } = null!;

        //Profil
        [ForeignKey(nameof(ProfilId))]
        [InverseProperty(nameof(Profil.AvisRecus))]
        public virtual Profil ProfilAvi { get; set; } = null!;
    }
}
