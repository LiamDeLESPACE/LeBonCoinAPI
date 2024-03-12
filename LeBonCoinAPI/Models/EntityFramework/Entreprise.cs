using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_entreprise_ent")]
    public class Entreprise
    {
        [Key]
        [Column("ent_id")]
        public int IdEntreprise { get; set; }

        [Key]
        [Column("cut_id")]
        public int IdCu { get; set; }

        [Required]
        [Column("ent_siret")]
        [StringLength(14)]
        public string Siret { get; set; } = null!;

        [Column("ent_nom")]
        [StringLength(50)]
        public string? Nom { get; set; }

        [Column("ent_secteuractivite")]
        [StringLength(50)]
        public string? SecteurActivite { get; set; }
    }
}
