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

        [ForeignKey(nameof(AvisId))]
        [InverseProperty(nameof(Avis.AvisProfil))]
        public virtual ICollection<Avis> AvisProfil { get; set; } = null!;

        [ForeignKey(nameof(ProfilId))]
        [InverseProperty(nameof(Profil.ProfilCorrespondant))]
        public virtual ICollection<Profil> ProfilCorrespondant { get; set; } = null!;

    }
}
