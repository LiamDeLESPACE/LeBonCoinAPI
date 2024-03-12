﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_adresse_adr")]
    public class Adresse
    {
        [Key]
        [Column("adr_id")]
        public int IdAdresse { get; set; }

        [Key]
        [Column("vil_codeinsee")]
        [StringLength(11)]
        public string CodeInsee { get; set; } = null!;

        [Required]
        [Column("adr_rue")]
        [StringLength(100)]
        public string Rue { get; set; } = null!;

        [Required]
        [Column("adr_numero")]
        public int Numero { get; set; }

        [Required]
        [Column("adr_pays")]
        [StringLength(50)]
        public string Pays { get; set; } = null!;

    }
}
