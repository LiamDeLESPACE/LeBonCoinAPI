using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LeBonCoinAPI.Models.EntityFramework
{

    public abstract class Profil
    {
        public Profil(string hashPassword) : this()
        {
            
            HashMotDePasse = hashPassword;
        }

        public Profil()
        {
            CartesBancairesProfil = new HashSet<CarteBancaire>();
            PhotosProfil = new HashSet<Photo>();
            ReservationsProfil = new HashSet<Reservation>();
            SignalementsProfil = new HashSet<Signale>();
            AnnoncesProfil = new HashSet<Annonce>();
            FavorisProfil = new HashSet<Favoris>();
            CommentairesProfil = new HashSet<Commentaire>();
        }

        public Profil(string hashPassword, string telephone) : this(hashPassword)
        {
            Telephone = telephone;
        }

        [Key]
        [Column("prf_id")]
        public int ProfilId { get; set; }

        [Required]
        [Column("prf_hashmdp")]
        public string HashMotDePasse { get; set; }

        [Column("prf_telephone")]
        [StringLength(10)]
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Le telephone doit contenir 10 chiffres")]
        public string? Telephone { get; set; }

        
        //CarteBancaire
        [InverseProperty(nameof(CarteBancaire.ProfilCarteBancaire))]
        public virtual ICollection<CarteBancaire> CartesBancairesProfil { get; set; }

        //Photo
        [InverseProperty(nameof(Photo.ProfilPhoto))]
        public virtual ICollection<Photo> PhotosProfil { get; set; }

        //Reservation
        [InverseProperty(nameof(Reservation.ProfilReservation))]
        public virtual ICollection<Reservation> ReservationsProfil { get; set; }

        //Signale
        [InverseProperty(nameof(Signale.ProfilSignalement))]
        public virtual ICollection<Signale> SignalementsProfil { get; set; }

        //Favoris
        [InverseProperty(nameof(Favoris.ProfilFavoris))]
        public virtual ICollection<Favoris> FavorisProfil { get; set; }

        //Annonce
        [InverseProperty(nameof(Annonce.ProfilAnnonce))]
        public virtual ICollection<Annonce> AnnoncesProfil { get; set; }

        //Commentaire
        [InverseProperty(nameof(Commentaire.ProfilCommentaire))]
        public virtual ICollection<Commentaire> CommentairesProfil { get; set; }

    }
}
