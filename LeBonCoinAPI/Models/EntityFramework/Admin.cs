using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_admin_adm")]
    public class Admin : Profil
    {
        public Admin()
        {
            
        }

        //[Key]
        //[Column("prf_id")]
        //public int ProfilId { get; set; }

        //[Required]
        //[Column("adr_id")]
        //public int AdresseId { get; set; }

        //[Required]
        //[Column("prf_hashmdp")]
        //public string HashMotDePasse { get; set; }

        //[Column("prf_telephone")]
        //[StringLength(10)]
        //[RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Le telephone doit contenir 10 chiffres")]
        //public string? Telephone { get; set; }

        [Required]
        [Column("adm_service")]
        [StringLength(50)]
        public string Service { get; set; }

        [Column("adm_email")]
        [StringLength(100)]
        public string? Email { get; set; }
        
    }
}
