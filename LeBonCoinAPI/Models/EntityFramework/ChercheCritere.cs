using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_j_cherchercritere_cri")]
    public class ChercheCritere
    {
        [Key]
        [Column("cri_idcritere")]
        public int IdCritere { get; set; }

        [Key]
        [Column("cri_idrecherche")]
        public int IdRecherche;

    }
}
