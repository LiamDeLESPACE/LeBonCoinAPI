﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_categoriecritere_ccr")]
    public class CategorieCritere
    {
        [Key]
        [Column("ccr_id")]
        public int IdCategorieCritere { get; set; }

        [Required]
        [Column("ccr_libelle")]
        [StringLength(50)]
        public string Libelle { get; set; } = null!;
    }
}
