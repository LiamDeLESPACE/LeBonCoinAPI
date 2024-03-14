using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_j_calendrier_cln")]
    public class Calendrier
    {
        public Calendrier()
        {
            DatesCalendrier = new HashSet<Datec>();
            AnnoncesCalendrier = new HashSet<Annonce>();
        }

        [Key]
        [Column("cln_id")]
        public int CalendrierId { get; set; }

        //DateC
        [InverseProperty(nameof(Datec.CalendrierDate))]
        public virtual ICollection<Datec> DatesCalendrier { get; set; }

        //Annonce
        [InverseProperty(nameof(Annonce.CalendrierAnnonce))]
        public virtual ICollection<Annonce> AnnoncesCalendrier { get; set; }
    }
}
