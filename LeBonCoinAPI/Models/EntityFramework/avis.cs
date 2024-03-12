﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_avis_avi")]
    public class avis
    {
        [Key]
        [Column("avi_idavis")]
        public int IdAvis { get; set; }

        [Key]
        [Column("avi_idprofil")]
        public int IdProfil { get; set; }



        [StringLength(100)]
        [Column("avi_titre")]
        public string TitreAvis { get; set; }


        [Column("avi_description")]
        public string DescriptionAvis { get; set; }

        [Required]
        [Column("avi_note")]
        public int NoteAvis { get; set; }
    }
}
