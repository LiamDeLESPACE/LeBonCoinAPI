using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_profil_pro")]
    public class Profil
    {
        public Profil()
        {
            AvisDepose = new HashSet<Avis>();
            AvisRecus = new HashSet<ProfilAvis>();
            RecherchesProfil = new HashSet<Recherche>();
            FavorisProfil = new HashSet<Favoris>();
        }

        [Key]
        [Column("pro_id")]
        public int ProfilId { get; set; }

        [Key]
        [Column("prp_id")]
        public int ProprietaireId { get; set; }

        [Key]
        [Column("cut_id")]
        public int CompteUtilisateurId { get; set; }

        [Key]
        [Column("loc_id")]
        public int LocataireId { get; set; }

        [Key]
        [Column("adr_id")]
        public int AdresseId { get; set; }

        [Key]
        [Column("pho_id")]
        public int PhotoId { get; set; }

        [Column("pro_tempsreponse")]
        public int TempsReponse { get; set; }

        [Required]
        [Column("pro_datemembre")]
        public DateTime DateMembre { get; set; }

        [Required]
        [Column("pro_recommandation")]
        public bool Recommandation { get; set; }

        //Avis despose par
        [InverseProperty(nameof(Avis.ProfilAvi))]
        public virtual ICollection<Avis> AvisDepose { get; set; }

        //Avis recu
        [InverseProperty(nameof(ProfilAvis.ProfilAvi))]
        public virtual ICollection<ProfilAvis> AvisRecus { get; set; }

        //Recherche
        [InverseProperty(nameof(Recherche.ProfilRecherche))]
        public virtual ICollection<Recherche> RecherchesProfil { get; set; }

        //Adresse
        [ForeignKey(nameof(AdresseId))]
        [InverseProperty(nameof(Adresse.ProfilsAdresse))]
        public virtual Adresse AdresseProfil { get; set; }

        //CompteUtilisateur
        [ForeignKey(nameof(CompteUtilisateurId))]
        [InverseProperty(nameof(CompteUtilisateur.ProfilsCompteUtilisateur))]
        public virtual CompteUtilisateur CompteUtilisateurProfil { get; set; } = null!;

        //Proprietaire
        [ForeignKey(nameof(ProprietaireId))]
        [InverseProperty(nameof(Proprietaire.ProfilsProprietaire))]
        public virtual Proprietaire ProprietaireProfil { get; set; }

        //Locataire
        [ForeignKey(nameof(LocataireId))]
        [InverseProperty(nameof(Locataire.ProfilsLocataire))]
        public virtual Locataire LocataireProfil { get; set; }

        //Favoris
        [InverseProperty(nameof(Favoris.ProfilFavoris))]
        public virtual ICollection<Favoris> FavorisProfil { get; set; }

        //Photo
        [ForeignKey(nameof(PhotoId))]
        [InverseProperty(nameof(Photo.ProfilsPhoto))]
        public virtual Photo PhotoProfil { get; set; }
    }
}
