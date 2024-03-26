﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_photo_pho")]
    public class Photo
    {
        public Photo()
        {

        }

        [Key]
        [Column("pho_id")]
        public int PhotoId { get; set; }

        [Required]
        [Column("prf_id")]
        public int ProfilId { get; set; }

        [Required]
        [Column("ann_id")]
        public int AnnonceId { get; set; }

        [Required]
        [Column("pho_url")]
        public string URL { get; set; } = null!;


        //Profil
        /*[ForeignKey(nameof(ProfilId))]
        [InverseProperty(nameof(Profil.PhotosProfil))]
        public virtual Profil ProfilPhoto { get; set; } = null!;

        //Annonce
        [ForeignKey(nameof(AnnonceId))]
        [InverseProperty(nameof(Annonce.PhotosAnnonce))]
        public virtual Annonce AnnoncePhoto { get; set; } = null!;*/



    }
}
