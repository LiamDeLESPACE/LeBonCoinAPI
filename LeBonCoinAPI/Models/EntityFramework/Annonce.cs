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
            FavorisAnnonce = new HashSet<Favoris>();
            AvisAnnonce = new HashSet<AnnonceAvis>();
            ContientsAnnonce = new HashSet<Contient>();
            ReservationsAnnonce = new HashSet<Reservation>();
            PhotosAnnonce = new HashSet<Photo>();

        }

        [Key]
        [Column("ann_id")]
        public int AnnonceId { get; set; }

        [Required]
        [Column("prp_id")]
        public int ProprietaireId { get; set; }

        [Column("dat_id")]
        public DateTime DatecId { get; set; }

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
        [Range(0,5)]
        public int Etoile { get; set; }


        //Date
        [ForeignKey(nameof(DatecId))]
        [InverseProperty(nameof(Datec.DateAnnonce))]
        public virtual Datec AnnonceDate { get; set; } = null!;
        
        //Favoris
        [InverseProperty(nameof(Favoris.AnnonceFavoris))]
        public virtual ICollection<Favoris> FavorisAnnonce { get; set; }

        //AnnonceAvis
        [InverseProperty(nameof(AnnonceAvis.AnnonceAvi))]
        public virtual ICollection<AnnonceAvis> AvisAnnonce { get; set; }

        //TypeLogement
        [ForeignKey(nameof(TypelogementId))]
        [InverseProperty(nameof(TypeLogement.AnnoncesTypeLogement))]
        public virtual TypeLogement TypeLogementAnnonce { get; set; } = null!;

        //Proprietaire
        [ForeignKey(nameof(ProprietaireId))]
        [InverseProperty(nameof(Proprietaire.AnnoncesProprietaire))]
        public virtual Proprietaire ProprietaireAnnonce { get; set; } = null!;

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
