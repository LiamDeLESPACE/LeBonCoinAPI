using Microsoft.CodeAnalysis.Emit;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_admin_adm")]
    public class Admin : Profil
    {
        public Admin(string service, string adminmail, string hashPassword) : base(hashPassword)
        {
            Service = service;
            Email = adminmail;
        }

        public Admin(string service, string adminmail, string hashPassword, string telephone) : base(hashPassword, telephone)
        {
            Service = service;
            Email = adminmail;
        }

        public Admin() : base() { }


        [Column("adm_service")]
        [MaxLength(50)]
        public string? Service { get; set; }

        [Required]
        [Column("adm_email")]
        [MaxLength(100)]
        [RegularExpression(@"^[^@\s]+@lebonendroit\.com$")]
        public string Email { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Admin admin &&
                   ProfilId == admin.ProfilId &&
                   HashMotDePasse == admin.HashMotDePasse &&
                   Telephone == admin.Telephone &&
                   Service == admin.Service &&
                   Email == admin.Email;
        }
    }
}
