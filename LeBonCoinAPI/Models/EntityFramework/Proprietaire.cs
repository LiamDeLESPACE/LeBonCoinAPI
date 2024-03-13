using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_proprietaire_prp")]
    public class Proprietaire
    {
        [Key]
        [Column("prp_id")]
        public int IdProprietaire { get; set; }

        [Key]
        [Column("pro_id")]
        public int IdProfil { get; set; }

        [InverseProperty(nameof(Annonce.Proprietaires))]
        public virtual ICollection<Contient> Proprio { get; set; }
    }
}
