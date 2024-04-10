using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class AnnonceManager : IRepository<Annonce>
    {
        readonly DataContext? dataContext;
        public AnnonceManager() { }
        public AnnonceManager(DataContext context)
        {
            dataContext = context;
        }
        public async Task<ActionResult<IEnumerable<Annonce>>> GetAll()
        {
            List<Annonce> annonces = await dataContext.Annonces.ToListAsync();
            List<Adresse> adresses = (await new AdresseManager(dataContext).GetAll()).Value.ToList();
            List<PossedeEquipement> possedeEquipements = (await new PossedeEquipementManager(dataContext).GetAll()).Value.ToList();
            List<TypeLogement> typeLogements = (await new TypeLogementManager(dataContext).GetAll()).Value.ToList();
            List<Signale> signales = (await new SignaleManager(dataContext).GetAll()).Value.ToList();
            List<Favoris> favoris = (await new FavorisManager(dataContext).GetAll()).Value.ToList();

            foreach (Adresse adresse in adresses)
            {
               adresse.AnnoncesAdresse = null;
            }
            foreach (PossedeEquipement pe in possedeEquipements)
            {
                pe.AnnonceEquipementPossede = null;
            }
            foreach (TypeLogement tl in typeLogements)
            {
                tl.AnnoncesTypeLogement = null;
            }
            foreach (Signale s in signales)
            {
                s.AnnonceSignalement = null;
            }
            foreach (Favoris f in favoris)
            {
                f.AnnonceFavoris = null;
            }
            return annonces;
        }

        public async Task<ActionResult<Annonce>> GetById(int id)
        {
            Annonce annonce = await dataContext.Annonces.FindAsync(id);
            if(annonce != null)
            {
                annonce.AdresseAnnonce = (await new AdresseManager(dataContext).GetById(annonce.AdresseId)).Value;
                if(annonce.AdresseAnnonce != null)
                    annonce.AdresseAnnonce.AnnoncesAdresse = null;
                annonce.TypeLogementAnnonce = (await new TypeLogementManager(dataContext).GetById(annonce.TypeLogementId)).Value;
                if(annonce.TypeLogementAnnonce != null)
                    annonce.TypeLogementAnnonce.AnnoncesTypeLogement = null;
                annonce.SignalementsAnnonce = (await new SignaleManager(dataContext).GetByIdAnnonce(annonce.AnnonceId)).Value.ToList();
                if(annonce.SignalementsAnnonce != null)
                {
                    foreach (Signale s in annonce.SignalementsAnnonce)
                    {
                        s.AnnonceSignalement = null;
                    }
                }
            }
            return annonce;
        }
        public async Task Add(Annonce entity)
        {
            await dataContext.Annonces.AddAsync(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(Annonce annonce, Annonce entity)
        {
            dataContext.Entry(annonce).State = EntityState.Modified;            
            annonce.AdresseId = entity.AdresseId;
            annonce.TypeLogementId = entity.TypeLogementId;
            annonce.ProfilId = entity.ProfilId;
            annonce.Titre = entity.Titre;
            annonce.DureeMinimumSejour = entity.DureeMinimumSejour;
            annonce.Active = entity.Active;
            annonce.DatePublication = entity.DatePublication;
            annonce.Description = entity.Description;
            annonce.Etoile = entity.Etoile;
            annonce.NombrePersonnesMax = entity.NombrePersonnesMax;
            annonce.PrixParNuit = entity.PrixParNuit;
            annonce.NombreChambres = entity.NombreChambres;            

            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(Annonce annonce)
        {
            dataContext.Annonces.Remove(annonce);
            await dataContext.SaveChangesAsync();
        }
    }
}
