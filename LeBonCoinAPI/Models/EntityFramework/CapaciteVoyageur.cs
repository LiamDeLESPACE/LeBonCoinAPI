using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_capacitevoyageur_cvo")]
    public class CapaciteVoyageur
    {

        [Key]
        [Column("cvo_idcapacitevoyageur")]
        public int IdCapaciteVoyageur { get; set; }


        [Required]
        [Column("cvo_nbadultes")]
        public int NbAdulte { get; set; }

        [Required]
        [Column("cvo_nbenfants")]
        public int NbEnfants { get; set; }


        [Required]
        [Column("cvo_nbBebes")]
        public int NbBebes { get; set; }

        [Required]
        [Column("cvo_nbAnimaux")]
        public int NbAnimaux { get; set; }
    }
}
