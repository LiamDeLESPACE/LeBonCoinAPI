using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_proprietaire_pro")]
    public class Proprietaire
    {
        [Key]
        [Column("pro_idproprietaire")]
        public int IdProprietaire { get; set; }

        [Key]
        [Column("pro_idprofil")]
        public int IdProfil { get; set; }
    }
}
