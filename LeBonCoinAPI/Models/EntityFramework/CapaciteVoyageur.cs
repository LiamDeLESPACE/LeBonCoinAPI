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
        [RegularExpression(@"^[1-99]{1}$", ErrorMessage = "La nombre d'adulte doit être un chiffre compris entre 1 et 99.")]
        public int NbAdulte { get; set; }

        [Required]
        [Column("cvo_nbenfants")]
        [RegularExpression(@"^[1-99]{1}$", ErrorMessage = "La nombre d'adulte doit être un chiffre compris entre 1 et 99.")]
        public int NbEnfants { get; set; }


        [Required]
        [Column("cvo_nbBebes")]
        [RegularExpression(@"^[1-99]{1}$", ErrorMessage = "La nombre d'adulte doit être un chiffre compris entre 1 et 99.")]
        public int NbBebes { get; set; }

        [Required]
        [Column("cvo_nbAnimaux")]
        [RegularExpression(@"^[1-99]{1}$", ErrorMessage = "La nombre d'adulte doit être un chiffre compris entre 1 et 99.")]
        public int NbAnimaux { get; set; }
    }
}
