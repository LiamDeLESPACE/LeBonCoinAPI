using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_locataire_loc")]
    public class Locataire
    {
        [Key]
        [Column("loc_id")]
        public int IdLocataire { get; set; }

        [Key]
        [Column("pro_id")]
        public int IdProfil { get; set; }

        //ReferenceCarteBancaire
        [InverseProperty(nameof(ReferenceCarteBancaire.LocataireReferent))]
        public virtual ICollection<ReferenceCarteBancaire> ReferencesCarteLocataire { get; set; }

        //Incident
        [InverseProperty(nameof(Incident.LocataireSignalant))]
        public virtual ICollection<Incident> SignalementLocataire { get; set; }

        //Profil
        [InverseProperty(nameof(Profil.LocataireProfile))]
        public virtual ICollection<Profil> Locataires { get; set; }
    }
}
