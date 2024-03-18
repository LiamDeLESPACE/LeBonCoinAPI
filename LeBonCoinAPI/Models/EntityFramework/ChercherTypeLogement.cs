using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_j_cherchertypelogement_ctl")]
    public class ChercherTypeLogement
    {
        [Key]
        [Column("tyl_id")]
        public int TypeLogementId { get; set; }


        [Key]
        [Column("rch_id")]
        public int RechercheId { get; set; }

        //Recherche
        [ForeignKey(nameof(RechercheId))]
        [InverseProperty(nameof(Recherche.TypesLogementsDeLaRecherche))]
        public virtual Recherche RechercheDuTypeLogement { get; set; } = null!;

        //TypeLogement
        [ForeignKey(nameof(TypeLogementId))]
        [InverseProperty(nameof(TypeLogement.RecherchesTypeLogement))]
        public virtual TypeLogement TypeLogementRecherche { get; set; } = null!;

    }
}
