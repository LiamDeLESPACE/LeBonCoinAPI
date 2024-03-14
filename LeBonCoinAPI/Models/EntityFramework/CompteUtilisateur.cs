using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_compteutilisateur_cut")]
    public class CompteUtilisateur
    {
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
        [InverseProperty(nameof(Profil.CompteUti))]
        public virtual ICollection<Profil> Compte { get; set; }

        //EtatCompte
        [ForeignKey(nameof(CodeEtatCompteUtilisateur))]
        [InverseProperty(nameof(EtatCompte.CompteUtilisateursEtatCompte))]
        public virtual EtatCompte EtatCompteUtilisateur { get; set; } = null!;

        //Particulier
        [ForeignKey(nameof(ParticulierId))]
        [InverseProperty(nameof(Particulier.CompteUtilisateursParticulier))]
        public virtual Particulier ParticulierCompteUtilisateur { get; set; } = null!;

        //Entreprise
        [ForeignKey(nameof(EntrepriseId))]
        [InverseProperty(nameof(Entreprise.CompteUtilisateursEntreprise))]
        public virtual Entreprise EntrepriseCompteUtilisateur { get; set; } = null!;
    }
}
