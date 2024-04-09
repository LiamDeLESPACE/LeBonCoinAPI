using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_reservation_res")]
    public class Reservation
    {
        public Reservation()
        {
            ReglementsReservation = new HashSet<Reglement>();
            CommentairesReservation = new HashSet<Commentaire>();
        }

        public Reservation(int annonceId, int profilId, DateTime dateArrivee, DateTime dateDepart, int nbVoyageurs, 
            string nom, string prenom, string telephone) : this()
        {
            AnnonceId = annonceId;
            ProfilId = profilId;
            DateArrivee = dateArrivee;
            DateDepart = dateDepart;
            NombreVoyageur = nbVoyageurs;
            Nom = nom;
            Prenom = prenom;
            Telephone = telephone;
        }


        [Key]
        [Column("res_id")]
        public int ReservationId { get; set; }

        [Required]
        [Column("ann_id")]
        public int AnnonceId { get; set; }

        [Required]
        [Column("prf_id")]
        public int ProfilId { get; set; }

        [Required]
        [Column("res_datearrivee", TypeName = "date")]
        public DateTime DateArrivee { get; set; }

        [Required]
        [Column("res_datedepart", TypeName = "date")]
        public DateTime DateDepart { get; set; }

        [Required]
        [Column("res_nombrevoyageur")]
        public int NombreVoyageur { get; set; }

        [Required]
        [Column("res_nom")]
        [StringLength(50)]
        public string Nom { get; set; }

        [Required]
        [Column("res_prenom")]
        [StringLength(50)]
        public string Prenom { get; set; }

        [Required]
        [Column("res_telephone")]
        [StringLength(10)]
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Le telephone doit contenir 10 chiffres")]
        public string Telephone { get; set; }


        //Reglement
        [InverseProperty(nameof(Reglement.ReservationReglement))]
        public virtual ICollection<Reglement> ReglementsReservation { get; set; }

        //Commentaire
        [InverseProperty(nameof(Commentaire.ReservationCommentaire))]
        public virtual ICollection<Commentaire> CommentairesReservation { get; set; }
        
        //Profil
        [ForeignKey(nameof(ProfilId))]
        [InverseProperty(nameof(Profil.ReservationsProfil))]
        public virtual Profil ProfilReservation { get; set; }  

        //Annonce
        [ForeignKey(nameof(AnnonceId))]
        [InverseProperty(nameof(Annonce.ReservationsAnnonce))]
        public virtual Annonce AnnonceReservation { get; set; }  

        public override bool Equals(object? obj)
        {
            return obj is Reservation reservation &&
                   ReservationId == reservation.ReservationId &&
                   AnnonceId == reservation.AnnonceId &&
                   ProfilId == reservation.ProfilId &&
                   DateArrivee == reservation.DateArrivee &&
                   DateDepart == reservation.DateDepart &&
                   NombreVoyageur == reservation.NombreVoyageur &&
                   Nom == reservation.Nom &&
                   Prenom == reservation.Prenom &&
                   Telephone == reservation.Telephone;
        }
    }
}
