using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_proprietaire_prp")]
    public class Proprietaire
    {
        [Key]
        [Column("prp_idproprietaire")]
        public int IdProprietaire { get; set; }

        [Key]
        [Column("prp_idprofil")]
        public int IdProfil { get; set; }
    }
}
