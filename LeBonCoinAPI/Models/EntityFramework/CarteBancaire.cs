using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_cartebancaire_cab")]
    public class CarteBancaire
    {
        public CarteBancaire(int profilId, string numCarte)
        {
            ProfilId = profilId;
            Numero = numCarte;
        }

        public CarteBancaire()
        {
            
        }

        [Key]
        [Column("cab_id")]
        public int CarteId { get; set; }

        [Required]
        [Column("prf_id")]
        public int ProfilId { get; set; }

        [Required]
        [Column("cab_numero")]
        [StringLength(16)]
        [RegularExpression("^[0-9]{16}$", ErrorMessage = "Un numéro de carte compte 16 chiffres")]
        public string Numero { get; set; }

        //Profil
        [ForeignKey(nameof(ProfilId))]
        [InverseProperty(nameof(Profil.CartesBancairesProfil))]
        public virtual Profil ProfilCarteBancaire { get; set; } = null!;
    }
}
