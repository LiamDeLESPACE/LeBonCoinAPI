using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_profil_prf")]
    public abstract class Profil
    {
        public Profil()
        {
            CartesBancairesProfil = new HashSet<CarteBancaire>();
            PhotosProfil = new HashSet<Photo>();
            ReservationsProfil = new HashSet<Reservation>();
            SignalementsProfil = new HashSet<Signale>();
            AnnoncesProfil = new HashSet<Annonce>();
            FavorisProfil = new HashSet<Favoris>();
            AvisProfil = new HashSet<Avis>();
        }

        [Key]
        [Column("prf_id")]
        public int ProfilId { get; set; }

        [Required]
        [Column("adr_id")]
        public int AdresseId { get; set; }

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

        /*//Entreprise
        [InverseProperty(nameof(Entreprise.ProfilEntreprise))]
        public virtual ICollection<Entreprise> EntreprisesProfil { get; set; }

        //Particulier
        [InverseProperty(nameof(Particulier.ProfilParticulier))]
        public virtual ICollection<Particulier> ParticuliersProfil { get; set; }*/


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

        //Adresse
        [ForeignKey(nameof(ProfilId))]
        [InverseProperty(nameof(Adresse.ProfilsAdresse))]
        public virtual Adresse AdresseProfil { get; set; } = null!;

        //Avis
        [InverseProperty(nameof(Avis.ProfilAvis))]
        public virtual ICollection<Avis> AvisProfil { get; set; }

    }
}
