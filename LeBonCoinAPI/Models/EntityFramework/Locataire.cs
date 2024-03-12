using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_locataire_loc")]
    public class Locataire
    {
        [Key]
        [Column("loc_idlocataire")]
        public int IdLocataire { get; set; }

        [Key]
        [Column("loc_idprofil")]
        public int IdProfil { get; set; }
    }
}
