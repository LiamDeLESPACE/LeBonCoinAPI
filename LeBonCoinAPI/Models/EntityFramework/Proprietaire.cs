﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_proprietaire_prp")]
    public class Proprietaire
    {
        [Key]
        [Column("prp_id")]
        public int ProprietaireId { get; set; }

        [Key]
        [Column("pro_id")]
        public int ProfilId { get; set; }

        //Annonce
        [InverseProperty(nameof(Annonce.Proprietaires))]
        public virtual ICollection<Annonce> Proprio { get; set; }

        //Profil
        [InverseProperty(nameof(Profil.Proprio))]
        public virtual ICollection<Profil> ProprietaireProfile { get; set; }

        [ForeignKey(nameof(ProfilId))]
        [InverseProperty(nameof(Profil.ProprioProfil))]
        public virtual Profil ProfilProprio { get; set; } = null!;
    }
}
