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
            SignalementsAnnonce = new HashSet<Signale>();
        }

        [Key]
        [Column("ann_id")]
        public int AnnonceId { get; set; }

        [Required]
        [Column("adr_id")]
        public int AdresseId { get; set; }

        [Required]
        [Column("tyl_id")]
        public int TypeLogementId { get; set; }

        [Required]
        [Column("prf_id")]
        public int ProfilId { get; set; }

        [Required]
        [Column("ann_titre")]
        [StringLength(100)]
        public string Titre { get; set; }

        [Required]
        [Column("ann_dureeminimumsejour")]
        public int DureeMinimumSejour { get; set; }

        [Required]
        [Column("ann_active")]
        public bool Active { get; set; }

        [Required]
        [Column("ann_datepublication", TypeName = "date")]
        public DateTime DatePublication { get; set; }

        [Required]
        [Column("ann_description")]
        public string Description { get; set; }

        [Column("ann_etoile")]
        [Range(0,5)]
        public int Etoile { get; set; }

        [Required]
        [Column("ann_nombrepersonnesmax")]
        public int NombrePersonnesMax { get; set; }

        [Required]
        [Column("ann_prixparnuit")]
        public double PrixParNuit { get; set; }

        [Required]
        [Column("ann_nombrechambres")]
        public int NombreChambres { get; set; }


        //PossedeEquipement
        [InverseProperty(nameof(PossedeEquipement.AnnonceEquipementPossede))]
        public virtual ICollection<PossedeEquipement> EquipementsPossedesAnnonce { get; set; }

        //Signale
        [InverseProperty(nameof(Signale.AnnonceSignalement))]
        public virtual ICollection<Signale> SignalementsAnnonce { get; set; }

        //Adresse
        [ForeignKey(nameof(AdresseId))]
        [InverseProperty(nameof(Adresse.AnnoncesAdresse))]
        public virtual Adresse AdresseAnnonce { get; set; } = null!;

        //TypeLogement
        [ForeignKey(nameof(TypeLogementId))]
        [InverseProperty(nameof(TypeLogement.AnnoncesTypeLogement))]
        public virtual TypeLogement TypeLogementAnnonce { get; set; } = null!;

        //Profil
        [ForeignKey(nameof(ProfilId))]
        [InverseProperty(nameof(Profil.AnnoncesProfil))]
        public virtual Profil ProfilAnnonce { get; set; } = null!;
    }
}
