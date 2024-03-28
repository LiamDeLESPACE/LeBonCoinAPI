using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_typelogement_tyl")]
    public class TypeLogement
    {
        public TypeLogement()
        {
            AnnoncesTypeLogement = new HashSet<Annonce>();
        }

        public TypeLogement(string nomTypeLogement): this()
        {
            Nom = nomTypeLogement;
        }

        [Key]
        [Column("tyl_id")]
        public int TypeLogementId { get; set; }

        [Required]
        [Column("tyl_nom")]
        [StringLength(50)]
        public string Nom { get; set; }

        //Annonce
        [InverseProperty(nameof(Annonce.TypeLogementAnnonce))]
        public virtual ICollection<Annonce> AnnoncesTypeLogement { get; set; }

    }
}
