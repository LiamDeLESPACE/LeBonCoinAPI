using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_cartebancaire_cab")]
    public class CarteBancaire
    {
        [Key]
        [Column("cab_id")]
        public int CarteId { get; set; }

        [Required]
        [Column("prf_id")]
        public int ProfilId { get; set; }

        [Required]
        [Column("cab_numero")]
        [StringLength(16)]
        public string Numero { get; set; }

        //Profil
        [ForeignKey(nameof(ProfilId))]
        [InverseProperty(nameof(Profil.CartesBancairesProfil))]
        public virtual Profil ProfilCarteBancaire { get; set; } = null!;
    }
}
