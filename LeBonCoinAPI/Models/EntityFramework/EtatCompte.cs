using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_etatcompte_eta")]
    public class EtatCompte
    {
        public EtatCompte()
        {
            CompteUtilisateursEtatCompte = new HashSet<CompteUtilisateur>();
        }

        [Key]        
        [Column("eta_codeetatcompteutilisateur")]
        public int CodeEtatCompteUtilisateur { get; set; }

        [Column("eta_libelle")]
        [StringLength(40)]
        public string? Libelle { get; set; }

        [InverseProperty(nameof(CompteUtilisateur.EtatCompteUtilisateur))]
        public virtual ICollection<CompteUtilisateur> CompteUtilisateursEtatCompte { get; set; }
    }
}
