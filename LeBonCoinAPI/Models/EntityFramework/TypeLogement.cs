using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_typelogement_tyl")]
    public class TypeLogement
    {
        [Key]
        [Column("tyl_idlogement")]
        public int IdLogement { get; set; }


        [Required]
        [Column("tyl_libelle")]
        public string libelle { get; set; } = null!;
    }
}
