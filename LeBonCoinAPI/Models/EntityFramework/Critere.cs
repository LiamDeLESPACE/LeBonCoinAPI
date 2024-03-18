﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_critere_cri")]
    public class Critere
    {
        public Critere()
        {
            AnnocesCritere = new HashSet<Contient>();
            RecherchesCritere = new HashSet<ChercheCritere>();
        }

        [Key]
        [Column("cri_id")]
        public int CritereId { get; set; }

        [Key]
        [Column("ccr_id")]
        public int CategorieCritereId { get; set; }

        [Required]
        [Column("cri_libellecritere")]
        public string libellecritere { get; set; } = null!;

        //Contient
        [InverseProperty(nameof(Contient.CritereAnnonce))]
        public virtual ICollection<Contient> AnnocesCritere { get; set; }

        //CategorieCritere
        [ForeignKey(nameof(CategorieCritereId))]
        [InverseProperty(nameof(CategorieCritere.CriteresCategorie))]
        public virtual CategorieCritere CategorieDuCritere { get; set; } = null!;

        //ChercheCritere
        [InverseProperty(nameof(ChercheCritere.CritereRecherche))]
        public virtual ICollection<ChercheCritere> RecherchesCritere { get; set; }

    }
}
