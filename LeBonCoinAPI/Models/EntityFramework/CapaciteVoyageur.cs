using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_capacitevoyageur_cvo")]
    public class CapaciteVoyageur
    {
        public CapaciteVoyageur()
        {
            RecherchesCapaciteVoyageur = new HashSet<Recherche>();
            AnnoncesCapaciteVoyageur = new HashSet<Annonce>();
        }

        [Key]
        [Column("cvo_id")]
        public int CapaciteVoyageurId { get; set; }


        [Required]
        [Column("cvo_nbadultes")]
        [Range(0, 99)]
        public int NbAdulte { get; set; }

        [Required]
        [Column("cvo_nbenfants")]
        [Range(0, 99)]
        public int NbEnfants { get; set; }


        [Required]
        [Column("cvo_nbBebes")]
        [Range(0, 99)]
        public int NbBebes { get; set; }

        [Required]
        [Column("cvo_nbAnimaux")]
        [Range(0, 99)]
        public int NbAnimaux { get; set; }

        //Recherche
        [InverseProperty(nameof(Recherche.CapaciteVoyageurRecherche))]
        public virtual ICollection<Recherche> RecherchesCapaciteVoyageur { get; set; }

        //Annonce
        [InverseProperty(nameof(Annonce.CapaciteVoyageurAnnonce))]
        public virtual ICollection<Annonce> AnnoncesCapaciteVoyageur { get; set; }

    }
}
