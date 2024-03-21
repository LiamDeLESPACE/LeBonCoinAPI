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

        [Required]
        [Column("adm_service")]
        [StringLength(50)]
        public string Service { get; set; }

        [Column("adm_email")]
        [StringLength(100)]
        public string? Email { get; set; }
        
    }
}
