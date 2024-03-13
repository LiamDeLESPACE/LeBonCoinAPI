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
            Avis = new HashSet<ProfilAvis>();
        }

        [Key]
        [Column("pro_id")]
        public int IdProfil { get; set; }

        [Key]
        [Column("prp_id")]
        public int IdProprietaire { get; set; }

        [Key]
        [Column("cut_id")]
        public int IdCu { get; set; }

        [Key]
        [Column("loc_id")]
        public int IdLocataire { get; set; }

        [Key]
        [Column("adr_id")]
        public int IdAdresse { get; set; }

        [Key]
        [Column("pho_id")]
        public int IdPhoto { get; set; }

        [Column("pro_tempsreponse")]
        public int TempsReponse { get; set; }

        [Required]
        [Column("pro_datemembre")]
        public DateTime DateMembre { get; set; }

        [Required]
        [Column("pro_recommandation")]
        public bool Recommandation { get; set; }

        //ProfilAvis
        [InverseProperty(nameof(ProfilAvis.AvisProfil))]
        public virtual ICollection<ProfilAvis> Avis { get; set; }

        //Adresse
        [ForeignKey(nameof(IdAdresse))]
        [InverseProperty(nameof(Adresse.Adresses))]
        public virtual Adresse Adr { get; set; } = null!;

        //CompteUtilisateur
        [ForeignKey(nameof(IdCu))]
        [InverseProperty(nameof(CompteUtilisateur.Compte))]
        public virtual Adresse CompteUti { get; set; } = null!;

        //proprietaire
        [ForeignKey(nameof(IdProprietaire))]
        [InverseProperty(nameof(Proprietaire.ProprietaireProfile))]
        public virtual Adresse Proprio { get; set; } = null!;




    }
}
