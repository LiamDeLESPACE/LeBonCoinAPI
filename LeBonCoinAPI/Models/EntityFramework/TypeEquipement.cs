using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_typeequipement_tye")]
    public class TypeEquipement
    {
        public TypeEquipement()
        {
            EquipementsTypeEquipement = new HashSet<Equipement>();
        }
        [Key]
        [Column("tye_id")]
        public int TypeEquipementId { get; set; }

        [Required]
        [Column("tye_nom")]
        [StringLength(50)]
        public string Nom { get; set; }

        //Equipement
        [InverseProperty(nameof(Equipement.TypeEquipementEquipement))]
        public virtual ICollection<Equipement> EquipementsTypeEquipement { get; set; }

    }
}
