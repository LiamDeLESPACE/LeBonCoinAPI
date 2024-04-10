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
            foreach (Annonce anonce in annonces)
            {
                anonce.AdresseAnnonce = (await new AdresseManager().GetById(anonce.AdresseId)).Value;
                if(anonce.AdresseAnnonce != null)
                {
                    anonce.AdresseAnnonce.VilleAdresse = (await new VilleManager().GetByInsee(anonce.AdresseAnnonce.CodeInsee)).Value;
                }
            }
            return annonces;
        }

        public async Task<ActionResult<Annonce>> GetById(int id)
        {
            return await dataContext.Annonces.FindAsync(id);
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
