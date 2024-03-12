using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_j_profilavis_pra")]
    public class ProfilAvis
    {
        [Key]
        [Column("pra_idavis")]
        public int IdAvis { get; set; }

        [Key]
        [Column("pra_idprofil")]
        public int IdProfil;

    }
}
