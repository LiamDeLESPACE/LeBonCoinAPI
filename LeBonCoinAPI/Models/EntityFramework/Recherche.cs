using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_j_recherche_rch")]
    public class Recherche
    {
        [Key]
        [Column("rch_idrecherche")]
        public int IdRecherche { get; set; }


        [Key]
        [Column("rch_idcapacitevoyageur")]
        public int IdCapaciteVoyageur { get; set; }

        [Key]
        [Column("rch_idprofil")]
        public int IdProfil { get; set; }


        [Required]
        [Column("rch_codeinsee")]
        public string CodeInsee { get; set; } = null!;
    }
}
