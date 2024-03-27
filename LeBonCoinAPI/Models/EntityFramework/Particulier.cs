using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_particulier_prt")]
    public class Particulier : Profil
    {
        public Particulier()
        {
          
        }

        [Required]
        [Column("prt_email")]
        [StringLength(100)]
        public string Email { get; set; }

        [Column("prt_civilite")]
        [StringLength(1)]
        public string? Civilite { get; set; }

        [Column("prt_nom")]
        [StringLength(50)]
        public string? Nom { get; set; }

        [Column("prt_prenom")]
        [StringLength(50)]
        public string? Prenom { get; set; }

        [Column("prt_datenaissance", TypeName = "date")]
        public DateTime? DateNaissance { get; set; }

        [Column("adr_id")]
        public int? AdresseId { get; set; }

        //Adresse
        [ForeignKey(nameof(AdresseId))]
        [InverseProperty(nameof(Adresse.ParticuliersAdresse))]
        public virtual Adresse? AdresseParticulier { get; set; }

    }
}
