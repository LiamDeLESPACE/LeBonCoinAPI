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
            Fav = new HashSet<Favoris>();
            Avis = new HashSet<AnnonceAvis>();
            ContientsAnnonce = new HashSet<Contient>();
            ReservationsAnnonce = new HashSet<Reservation>();

        }

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
        [RegularExpression(@"^[0-5]{1}$", ErrorMessage = "Le nombre d'étoiles doit être un chiffre compris entre 0 et 5.")]
        public int Etoile { get; set; }


        //Calendrier
        [ForeignKey(nameof(CalendrierId))]
        [InverseProperty(nameof(Calendrier.AnnoncesCalendrier))]
        public virtual Calendrier CalendrierAnnonce { get; set; } = null!;

        //Favoris
        [InverseProperty(nameof(Favoris.AnnonceFavoris))]
        public virtual ICollection<Favoris> Fav { get; set; }

        //AnnonceAvis
        [InverseProperty(nameof(AnnonceAvis.AvisAnnoces))]
        public virtual ICollection<AnnonceAvis> Avis { get; set; }

        //TypeLogement
        [ForeignKey(nameof(TypelogementId))]
        [InverseProperty(nameof(TypeLogement.TypesLo))]
        public virtual TypeLogement TypesLogements { get; set; } = null!;

        //Proprietaire
        [ForeignKey(nameof(ProprietaireId))]
        [InverseProperty(nameof(Proprietaire.Proprio))]
        public virtual Proprietaire Proprietaires { get; set; } = null!;

        //Contient
        [InverseProperty(nameof(Contient.AnnonceContient))]
        public virtual ICollection<Contient> ContientsAnnonce { get; set; }

        //Reservation
        [InverseProperty(nameof(Reservation.AnnonceReservation))]
        public virtual ICollection<Reservation> ReservationsAnnonce { get; set; }

        //CapaciteVoyageur
        [ForeignKey(nameof(CapacitevoyageurId))]
        [InverseProperty(nameof(CapaciteVoyageur.AnnoncesCapacite))]
        public virtual CapaciteVoyageur CapaciteAnnonce { get; set; } = null!;

        //Photo
        [InverseProperty(nameof(Photo.PhotoAnnonce))]
        public virtual ICollection<Photo> PhotosAnnonce { get; set; }
    }
}
