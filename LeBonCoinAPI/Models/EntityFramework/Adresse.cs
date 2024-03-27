﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LeBonCoinAPI.Models.EntityFramework
{
    [Table("t_e_adresse_adr")]
    public class Adresse
    {
        public Adresse()
        {
            EntreprisesAdresse = new HashSet<Entreprise>();
            ParticuliersAdresse = new HashSet<Particulier>();
            AnnoncesAdresse = new HashSet<Annonce>();
        }
        [Key]
        [Column("adr_id")]
        public int AdresseId { get; set; }

        [Required]
        [Column("vil_codeinsee")]
        [StringLength(5)]
        public string CodeInsee { get; set; }

        [Required]
        [Column("adr_rue")]
        [StringLength(100)]
        public string Rue { get; set; }

        [Required]
        [Column("adr_numero")]
        public int Numero { get; set; }

        //Entreprises
        [InverseProperty(nameof(Entreprise.AdresseEntreprise))]
        public virtual ICollection<Entreprise> EntreprisesAdresse { get; set; }

        //Particuliers
        [InverseProperty(nameof(Particulier.AdresseParticulier))]
        public virtual ICollection<Particulier> ParticuliersAdresse { get; set; }

        //Annonce
        [InverseProperty(nameof(Annonce.AdresseAnnonce))]
        public virtual ICollection<Annonce> AnnoncesAdresse { get; set; }

        //Ville
        [ForeignKey(nameof(CodeInsee))]
        [InverseProperty(nameof(Ville.AdressesVille))]
        public virtual Ville VilleAdresse { get; set; } = null!;

    }
}
