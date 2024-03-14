using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_compteutilisateur_cut")]
    public class CompteUtilisateur
    {
        public CompteUtilisateur()
        {
            ProfilsCompteUtilisateur = new HashSet<Profil>();
        }
        [Key]
        [Column("cut_id")]
        public int CompteUtilisateurId { get; set; }

        [Key]
        [Column("par_id")]
        public int ParticulierId { get; set; }

        [Key]
        [Column("ent_id")]
        public int EntrepriseId { get; set; }

        [Key]
        [Column("eta_codeetatcompteutilisateur")]
        public int CodeEtatCompteUtilisateur { get; set; }

        [Required]
        [Column("cut_motdepasse")]
        [StringLength(100)]
        public string MotDePasse { get; set; } = null!;

        [Column("cut_telephone")]
        [StringLength(10)]
        public string? Telephone { get; set; }

        [Required]
        [Column("cut_telephoneverifier")]
        public bool TelephoneVerifier { get; set; }

        //Profil
        [InverseProperty(nameof(Profil.CompteUtilisateurProfil))]
        public virtual ICollection<Profil> ProfilsCompteUtilisateur { get; set; }

        //EtatCompte
        [ForeignKey(nameof(CodeEtatCompteUtilisateur))]
        [InverseProperty(nameof(EtatCompte.ComptesUtilisateursEtatCompte))]
        public virtual EtatCompte EtatCompteDuCompteUtilisateur { get; set; } = null!;

        //Particulier
        [ForeignKey(nameof(ParticulierId))]
        [InverseProperty(nameof(Particulier.ComptesUtilisateursParticulier))]
        public virtual Particulier ParticulierCompteUtilisateur { get; set; }

        //Entreprise
        [ForeignKey(nameof(EntrepriseId))]
        [InverseProperty(nameof(Entreprise.ComptesUtilisateursEntreprise))]
        public virtual Entreprise EntrepriseCompteUtilisateur { get; set; }
    }
}
