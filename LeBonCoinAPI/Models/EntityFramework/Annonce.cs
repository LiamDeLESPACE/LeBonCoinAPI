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
            //ReservationsAnnonce = new HashSet<Reservation>();
            //SignalementsAnnonce = new HashSet<Signale>();
            //FavorisAnnonce = new HashSet<Favoris>();
            //PhotosAnnonce = new HashSet<Photo>();
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
        public string Titre { get; set; } = null!;


        [Column("ann_dureeminimumsejour")]
        public int DureeMinimumSejour { get; set; }

        [Required]
        [Column("ann_active")]
        public bool Active { get; set; }

        [Required]
        [Column("ann_datepublication", TypeName = "date")]
        public DateTime DatePublication { get; set; }


        [Column("ann_description")]
        public string? Description { get; set; }

        [Column("ann_etoile")]
        [Range(0,5)]
        public int Etoile { get; set; }


        [Column("ann_nombrepersonnesmax")]
        public int NombrePersonnesMax { get; set; }

        [Required]
        [Column("ann_prixparnuit")]
        public double PrixParNuit { get; set; }


        [Column("ann_nombrechambres")]
        public int NombreChambres { get; set; }


        //PossedeEquipement
        [InverseProperty(nameof(PossedeEquipement.AnnonceEquipementPossede))]
        public virtual ICollection<PossedeEquipement> EquipementsPossedesAnnonce { get; set; }

        //TypeLogement
        [ForeignKey(nameof(TypeLogementId))]
        [InverseProperty(nameof(TypeLogement.AnnoncesTypeLogement))]
        public virtual TypeLogement TypeLogementAnnonce { get; set; } = null!;

        //Reservation
        /*[InverseProperty(nameof(Reservation.AnnonceReservation))]
        public virtual ICollection<Reservation> ReservationsAnnonce { get; set; }

        //Signale
        [InverseProperty(nameof(Signale.AnnonceSignalement))]
        public virtual ICollection<Signale> SignalementsAnnonce { get; set; }

        //Favoris
        [InverseProperty(nameof(Favoris.AnnonceFavoris))]
        public virtual ICollection<Favoris> FavorisAnnonce { get; set; }*/

        //Photo
        [InverseProperty(nameof(Photo.AnnoncePhoto))]
        public virtual ICollection<Photo> PhotosAnnonce { get; set; }

        //Adresse
        [ForeignKey(nameof(AdresseId))]
        [InverseProperty(nameof(Adresse.AnnoncesAdresse))]
        public virtual Adresse AdresseAnnonce { get; set; } = null!;

        //Profil
        /*[ForeignKey(nameof(ProfilId))]
        [InverseProperty(nameof(Profil.AnnoncesProfil))]
        public virtual Profil ProfilAnnonce { get; set; } = null!;*/
    }
}
