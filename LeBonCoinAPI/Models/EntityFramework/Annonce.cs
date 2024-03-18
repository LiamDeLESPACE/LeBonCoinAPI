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
            CriteresAnnonce = new HashSet<Contient>();
            ReservationsAnnonce = new HashSet<Reservation>();
            PhotosAnnonce = new HashSet<Photo>();
            DatesAnnonce = new HashSet<Datec>();

        }

        [Key]
        [Column("ann_id")]
        public int AnnonceId { get; set; }

        [Required]
        [Column("prp_id")]
        public int ProprietaireId { get; set; }

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
        [InverseProperty(nameof(Datec.AnnonceDate))]
        public virtual ICollection<Datec> DatesAnnonce { get; set; }

        //Favoris
        [InverseProperty(nameof(Favoris.AnnonceFavoris))]
        public virtual ICollection<Favoris> FavorisAnnonce { get; set; }

        //AnnonceAvis
        [InverseProperty(nameof(AnnonceAvis.AnnonceAvisAnnonce))]
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
        [InverseProperty(nameof(Contient.AnnonceCritere))]
        public virtual ICollection<Contient> CriteresAnnonce { get; set; }

        //Reservation
        [InverseProperty(nameof(Reservation.AnnonceReservation))]
        public virtual ICollection<Reservation> ReservationsAnnonce { get; set; }

        //CapaciteVoyageur
        [ForeignKey(nameof(CapacitevoyageurId))]
        [InverseProperty(nameof(CapaciteVoyageur.AnnoncesCapaciteVoyageur))]
        public virtual CapaciteVoyageur CapaciteVoyageurAnnonce { get; set; } = null!;

        //Photo
        [InverseProperty(nameof(Photo.AnnoncePhoto))]
        public virtual ICollection<Photo> PhotosAnnonce { get; set; }

        //Adresse
        [ForeignKey(nameof(AdresseId))]
        [InverseProperty(nameof(Adresse.AnnoncesAdresse))]
        public virtual Adresse AdresseAnnonce { get; set; } = null!;

    }
}
