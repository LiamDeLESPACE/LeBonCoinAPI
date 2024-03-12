using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_j_reglement_rgl")]
    public class Reglement
    {

        [Key]
        [Column("rgl_numtransaction")]
        [StringLength(50)]
        public string? NumeroTransaction { get; set; }

        [Key]
        [Column("tyr_id")]
        public int IdTypeReglement { get; set; }

        [Key]
        [Column("res_id")]
        public int IdReservation { get; set; }
    }
}
