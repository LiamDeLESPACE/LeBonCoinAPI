using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_j_cherchertypelogement_ctl")]
    public class ChercherTypeLogement
    {
        [Key]
        [Column("ctl_idtypelogement")]
        public int IdTypeLogement { get; set; }


        [Key]
        [Column("ctl_idrecherche")]
        public int IdRecherche { get; set; }

        [ForeignKey(nameof(IdRecherche))]
        [InverseProperty(nameof(Recherche.ChercheTypesLogementsRecherche))]
        public virtual Recherche RechercheDuChercheTypeLogement { get; set; } = null!;

        [ForeignKey(nameof(IdTypeLogement))]
        [InverseProperty(nameof(TypeLogement.ChercheTypesLogements))]
        public virtual TypeLogement TypeLogementRecherche { get; set; } = null!;

    }
}
