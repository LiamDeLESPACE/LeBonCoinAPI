﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_annonce_ann")]
    public class Annonce
    {
        [Key]
        [Column("ann_id")]
        public int AnnonceId { get; set; }

        [Required]
        [Column("prp_id")]
        public int ProprietaireId { get; set; }

        [Column("cln_id")]
        public int CalendrierId { get; set; }

        [Required]
        [Column("tyl_id")]
        public int TypelogementId { get; set; }

        [Required]
        [Column("cpv_id")]
        public int CapacitevoyageurId { get; set; }

        [Required]
        [Column("adr_id")]
        public int AdresseId { get; set; }

        [Column("ann_titre")]
        [StringLength(100)]
        public string? Titre { get; set; }

        [Required]
        [Column("ann_dureeminimumsejour")]
        public int DureeMinimumSejour { get; set; }

        [Required]
        [Column("ann_estactive")]
        public bool EstActive { get; set; }

        [Required]
        [Column("ann_datepublication", TypeName = "date")]
        public DateTime? DatePublication { get; set; }

        [Column("ann_description")]
        public string? Description { get; set; }

        [Column("ann_etoile")]
        public int Etoile { get; set; }


        //calendrier
        [ForeignKey(nameof(CalendrierId))]
        [InverseProperty(nameof(Calendrier.CalendrierAnnonce))]
        public virtual Calendrier AnnonceCalendrier { get; set; } = null!;

        //Favoris
        [InverseProperty(nameof(Favoris.AnnonceFavoris))]
        public virtual ICollection<FormulaireChatbot> Fav { get; set; }

        //AnnonceAvis
        [InverseProperty(nameof(AnnonceAvis.AvisAnnoces))]
        public virtual ICollection<FormulaireChatbot> Avis { get; set; }

        //TypeLogement
        [ForeignKey(nameof(TypelogementId))]
        [InverseProperty(nameof(TypeLogement.TypesLo))]
        public virtual TypeLogement TypesLogements { get; set; } = null!;

        //liam bite xd
    }
}
