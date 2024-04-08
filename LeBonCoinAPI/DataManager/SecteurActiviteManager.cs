using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class SecteurActiviteManager : IRepository<SecteurActivite>
    {
        readonly DataContext? dataContext;
        public SecteurActiviteManager() { }
        public SecteurActiviteManager(DataContext context)
        {
            dataContext = context;
        }
        public async Task<ActionResult<IEnumerable<SecteurActivite>>> GetAll()
        {
            return await dataContext.SecteurActivites.ToListAsync();
        }

        public async Task<ActionResult<SecteurActivite>> GetById(int id)
        {
            return await dataContext.SecteurActivites.FindAsync(id);
        }
        public async Task Add(SecteurActivite entity)
        {
            await dataContext.SecteurActivites.AddAsync(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(SecteurActivite secteurActivite, SecteurActivite entity)
        {
            dataContext.Entry(secteurActivite).State = EntityState.Modified;            
            secteurActivite.NomSecteur = entity.NomSecteur;            
            
            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(SecteurActivite secteurActivite)
        {
            dataContext.SecteurActivites.Remove(secteurActivite);
            await dataContext.SaveChangesAsync();
        }
    }
}
