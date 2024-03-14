﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_typelogement_tyl")]
    public class TypeLogement
    {
        public TypeLogement()
        {
            AnnoncesTypeLogement = new HashSet<Annonce>();
            RecherchesTypeLogement = new HashSet<ChercherTypeLogement>();
        }

        [Key]
        [Column("tyl_id")]
        public int TypeLogementId { get; set; }


        [Required]
        [Column("tyl_libelle")]
        [StringLength(50)]
        public string Libelle { get; set; } = null!;

        //Annonce
        [InverseProperty(nameof(Annonce.TypeLogementAnnonce))]
        public virtual ICollection<Annonce> AnnoncesTypeLogement { get; set; }

        //ChercherTypeLogement
        [InverseProperty(nameof(ChercherTypeLogement.TypeLogementRecherche))]
        public virtual ICollection<ChercherTypeLogement> RecherchesTypeLogement { get; set; }
    }
}
