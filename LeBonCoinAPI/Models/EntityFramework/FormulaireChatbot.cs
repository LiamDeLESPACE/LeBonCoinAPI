using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_formulairechatbot_fcb")]
    public class FormulaireChatbot
    {
        [Key]
        [Column("fcb_id")]
        public int FormulaireChatbotId { get; set; }

        [Required]
        [Column("act_id")]
        public int ActionId { get; set; }

        [Column("fcb_nom")]
        [StringLength(30)]
        public string? Nom { get; set; }

        [Required]
        [Column("fcb_mail")]
        [EmailAddress]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Le nombre de caractère d'un email doit être compris entre 10 et 100 caractères.")]
        public string Mail { get; set; } = null!;

        [Column("fcb_telephone")]
        [StringLength(10)]
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Le telephone doit contenir 10 chiffres")]
        public string? Telephone { get; set; }

        [Column("fcb_question")]
        public string? Question { get; set; }

        //Action
        [ForeignKey(nameof(ActionId))]
        [InverseProperty(nameof(Action.FormulairesChatbot))]
        public virtual Action ActionFormulaire { get; set; } = null!;


    }
}
