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
            EntreprisesProfil = new HashSet<Entreprise>();
            ParticuliersProfil = new HashSet<Particulier>();
            AdminsProfil = new HashSet<Admin>();
            PhotosProfil = new HashSet<Photo>();
        }

        [Key]
        [Column("prf_id")]
        public int ProfilId { get; set; }

        [Required]
        [Column("adr_id")]
        public string AdresseId { get; set; }

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

        //Entreprise
        [InverseProperty(nameof(Entreprise.ProfilEntreprise))]
        public virtual ICollection<Entreprise> EntreprisesProfil { get; set; }

        //Particulier
        [InverseProperty(nameof(Particulier.ProfilParticulier))]
        public virtual ICollection<Particulier> ParticuliersProfil { get; set; }

        //Admin
        [InverseProperty(nameof(Admin.ProfilAdmin))]
        public virtual ICollection<Admin> AdminsProfil { get; set; }

        //Photo
        [InverseProperty(nameof(Photo.ProfilPhoto))]
        public virtual ICollection<Photo> PhotosProfil { get; set; }

        //Adresse
        [ForeignKey(nameof(ProfilId))]
        [InverseProperty(nameof(Adresse.ProfilsAdresse))]
        public virtual Profil AdresseProfil { get; set; } = null!;
    }
}
