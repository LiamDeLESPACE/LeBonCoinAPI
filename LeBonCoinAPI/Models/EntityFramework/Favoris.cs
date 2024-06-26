﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_j_favoris_fav")]
    public class Favoris
    {
        public Favoris(int annonceId,int profilId)
        {
            AnnonceId = annonceId;
            ProfilId = profilId;
        }

        public Favoris()
        {
            
        }

        [Key]
        [Column("ann_id")]
        public int AnnonceId { get; set; }

        [Key]
        [Column("prf_id")]
        public int ProfilId { get; set; }


        //Annonce
        [ForeignKey(nameof(AnnonceId))]
        [InverseProperty(nameof(Annonce.FavorisAnnonce))]
        public virtual Annonce AnnonceFavoris { get; set; }  

        //Profil
        [ForeignKey(nameof(ProfilId))]
        [InverseProperty(nameof(Profil.FavorisProfil))]
        public virtual Profil ProfilFavoris { get; set; }  

        public override bool Equals(object? obj)
        {
            return obj is Favoris favoris &&
                   AnnonceId == favoris.AnnonceId &&
                   ProfilId == favoris.ProfilId;
        }
    }
}
