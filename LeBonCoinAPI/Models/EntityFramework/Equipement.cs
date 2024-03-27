using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_equipement_equ")]
    public class Equipement
    {
        public Equipement()
        {
            EquipementsPossedesDesEquipement = new HashSet<PossedeEquipement>();
        }

        [Key]
        [Column("equ_id")]
        public int EquipementId { get; set; }

        [Required]
        [Column("tye_id")]
        public int TypeEquipementId { get; set; }

        [Required]
        [Column("equ_nom")]
        [StringLength(50)]
        public string Nom { get; set; }

        //PossedeEquipement
        [InverseProperty(nameof(PossedeEquipement.EquipementPossede))]
        public virtual ICollection<PossedeEquipement> EquipementsPossedesDesEquipement { get; set; }

        //TypeEquipement
        [ForeignKey(nameof(TypeEquipementId))]
        [InverseProperty(nameof(TypeEquipement.EquipementsTypeEquipement))]
        public virtual TypeEquipement TypeEquipementEquipement { get; set; } = null!;

    }
}
