using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_annonce_ann")]
    public class Annonce
    {
        public Annonce()
        {
            EquipementsPossedesAnnonce = new HashSet<PossedeEquipement>();


        }

        [Key]
        [Column("ann_id")]
        public int AnnonceId { get; set; }

        [Required]
        [Column("prp_id")]
        public int ProprietaireId { get; set; }

        [Required]
        [Column("tyl_id")]
        public int TypeLogementId { get; set; }

        [Required]
        [Column("cpv_id")]
        public int CapaciteVoyageurId { get; set; }

        [Required]
        [Column("adr_id")]
        public int AdresseId { get; set; }

        [Required]
        [Column("ann_titre")]
        [StringLength(100)]
        public string Titre { get; set; } = null!;

        [Required]
        [Column("ann_dureeminimumsejour")]
        public int DureeMinimumSejour { get; set; }

        [Required]
        [Column("ann_estactive")]
        public bool EstActive { get; set; }

        [Required]
        [Column("ann_datepublication", TypeName = "date")]
        public DateTime DatePublication { get; set; }

        [Required]
        [Column("ann_description")]
        public string Description { get; set; } = null!;

        [Column("ann_etoile")]
        [Range(0,5)]
        public int Etoile { get; set; }

        //PossedeEquipement
        [InverseProperty(nameof(PossedeEquipement.AnnonceEquipementPossede))]
        public virtual ICollection<PossedeEquipement> EquipementsPossedesAnnonce { get; set; }

        //Adresse
        [ForeignKey(nameof(AdresseId))]
        [InverseProperty(nameof(Adresse.AnnoncesAdresse))]
        public virtual Adresse AdresseAnnonce { get; set; } = null!;

        //TypeLogement
        [ForeignKey(nameof(TypeLogementId))]
        [InverseProperty(nameof(TypeLogement.AnnoncesTypeLogement))]
        public virtual TypeLogement TypeLogementAnnonce { get; set; } = null!;


    }
}
