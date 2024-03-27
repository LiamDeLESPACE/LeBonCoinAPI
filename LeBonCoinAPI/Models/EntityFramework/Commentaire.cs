using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_j_commentaire_cmt")]
    public class Commentaire
    {
        [Key]
        [Column("prf_id")]
        public int ProfilId { get; set; }

        [Key]
        [Column("res_id")]
        public int ReservationId { get; set; }

        [Required]
        [Column("cmt_contenu")]
        public string Contenu { get; set; } = null!;

        //Profil
        [ForeignKey(nameof(ProfilId))]
        [InverseProperty(nameof(Profil.CommentairesProfil))]
        public virtual Profil ProfilCommentaire { get; set; } = null!;

        //Reservation
        [ForeignKey(nameof(ReservationId))]
        [InverseProperty(nameof(Reservation.CommentairesReservation))]
        public virtual Reservation ReservationCommentaire { get; set; } = null!;
    }
}
