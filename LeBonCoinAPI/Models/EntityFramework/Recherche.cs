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
            CriteresDeLaRecherche = new HashSet<ChercheCritere>();
            ChercheTypesLogementsRecherche = new HashSet<ChercherTypeLogement>();
        }

        [Key]
        [Column("rch_id")]
        public int RechercheId { get; set; }


        [Key]
        [Column("cvo_id")]
        public int CapaciteVoyageurId { get; set; }

        [Key]
        [Column("pro_id")]
        public int ProfilId { get; set; }


        [Required]
        [Column("vil_codeinsee")]
        [RegularExpression(@"^[0-9]{5}$", ErrorMessage = "Le code insee doit contenir 5 chiffres")]
        public string CodeInsee { get; set; } = null!;

        //ChercheCritere
        [InverseProperty(nameof(ChercheCritere.RechercheDuCritere))]
        public virtual ICollection<ChercheCritere> CriteresDeLaRecherche { get; set; }

        //ChercheTypeLogement
        [InverseProperty(nameof(ChercherTypeLogement.RechercheDuChercheTypeLogement))]
        public virtual ICollection<ChercherTypeLogement> ChercheTypesLogementsRecherche { get; set; }

        
        //CapaciteVoyageur
        [ForeignKey(nameof(CapaciteVoyageurId))]
        [InverseProperty(nameof(CapaciteVoyageur.RecherchesCapaciteVoyageur))]
        public virtual CapaciteVoyageur CapaciteVoyageurRecherche { get; set; }

        //Vile
        [ForeignKey(nameof(CodeInsee))]
        [InverseProperty(nameof(Ville.RecherchesVille))]
        public virtual Ville VilleRecherche { get; set; } = null!;


        //Profil
        [ForeignKey(nameof(ProfilId))]
        [InverseProperty(nameof(Profil.RecherchesProfil))]
        public virtual Profil ProfilDeLaRecherche { get; set; } = null!;

    }
}
