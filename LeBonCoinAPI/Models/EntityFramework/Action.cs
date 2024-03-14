using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_action_act")]
    public class Action
    {
        public Action()
        {
            FormulairesChatbotAction = new HashSet<FormulaireChatbot>();
        }
        [Key]
        [Column("act_id")]
        public int ActionId { get; set; }

        [Column("act_libelle")]
        [StringLength(50)]
        public string? Libelle { get; set; }

        //FormulaireChatBot
        [InverseProperty(nameof(FormulaireChatbot.ActionFormulaire))]
        public virtual ICollection<FormulaireChatbot> FormulairesChatbotAction { get; set; }

    }
}
