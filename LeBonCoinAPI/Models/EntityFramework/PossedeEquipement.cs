using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_j_possedeequipement_peq")]
    public class PossedeEquipement
    {
        public PossedeEquipement()
        {
            
        }

        public PossedeEquipement(int annonceId, int equipementId)
        {
            AnnonceId = annonceId;
            EquipementId = equipementId;
        }

        [Key]
        [Column("ann_id")]
        public int AnnonceId { get; set; }

        [Key]
        [Column("equ_id")]
        public int EquipementId { get; set; }

        //Annonce
        [ForeignKey(nameof(AnnonceId))]
        [InverseProperty(nameof(Annonce.EquipementsPossedesAnnonce))]
        public virtual Annonce AnnonceEquipementPossede { get; set; }  

        //Equipement
        [ForeignKey(nameof(EquipementId))]
        [InverseProperty(nameof(Equipement.EquipementsPossedesDesEquipement))]
        public virtual Equipement EquipementPossede { get; set; }  

        public override bool Equals(object? obj)
        {
            return obj is PossedeEquipement equipement &&
                   AnnonceId == equipement.AnnonceId &&
                   EquipementId == equipement.EquipementId;
        }
    }
}
