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
        public int IdAvis { get; set; }

        [Key]
        [Column("pro_id")]
        public int IdProfil;

        [ForeignKey(nameof(IdProfil))]
        [InverseProperty(nameof(Profil.Avis))]
        public virtual Profil AvisProfil { get; set; } = null!;
    }
}
