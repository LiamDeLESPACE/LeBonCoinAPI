using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_proprietaire_prp")]
    public class Proprietaire
    {
        public Proprietaire()
        {
            ProfilProprietaire = new HashSet<Profil>();
            AnnoncesProprietaire = new HashSet<Annonce>();
        }
        [Key]
        [Column("prp_id")]
        public int ProprietaireId { get; set; }

        //Annonce
        [InverseProperty(nameof(Annonce.ProprietaireAnnonce))]
        public virtual ICollection<Annonce> AnnoncesProprietaire { get; set; }

        //Profil
        [InverseProperty(nameof(Profil.ProprietaireProfil))]
        public virtual ICollection<Profil> ProfilProprietaire { get; set; }

    }
}
