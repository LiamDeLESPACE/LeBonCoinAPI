using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_j_recherche_rch")]
    public class Recherche
    {
        public Recherche()
        {
            ChercheCriteresRecherche = new HashSet<ChercheCritere>();
            ChercheTypesLogementsRecherche = new HashSet<ChercherTypeLogement>();
        }

        [Key]
        [Column("rch_id")]
        public int IdRecherche { get; set; }


        [Key]
        [Column("cvo_id")]
        public int IdCapaciteVoyageur { get; set; }

        [Key]
        [Column("pro_id")]
        public int IdProfil { get; set; }


        [Required]
        [Column("vil_codeinsee")]
        [RegularExpression(@"^[0-9]{5}$", ErrorMessage = "Le code insee doit contenir 5 chiffres")]
        public string CodeInsee { get; set; } = null!;

        [InverseProperty(nameof(ChercheCritere.RechercheDuCritereCherche))]
        public virtual ICollection<ChercheCritere> ChercheCriteresRecherche { get; set; }

        [InverseProperty(nameof(ChercherTypeLogement.RechercheDuChercheTypeLogement))]
        public virtual ICollection<ChercherTypeLogement> ChercheTypesLogementsRecherche { get; set; }

        [ForeignKey(nameof(IdCapaciteVoyageur))]
        [InverseProperty(nameof(CapaciteVoyageur.RecherchesCapaciteVoyageur))]
        public virtual CapaciteVoyageur CapaciteVoyageurDeLaRecherche { get; set; } = null!;

        [ForeignKey(nameof(CodeInsee))]
        [InverseProperty(nameof(Ville.RecherchesVille))]
        public virtual Ville VilleDeLaRecherche { get; set; } = null!;

        [ForeignKey(nameof(IdProfil))]
        [InverseProperty(nameof(Profil.RecherchesProfil))]
        public virtual Profil ProfilDeLaRecherche { get; set; } = null!;

    }
}
