using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class SignaleManager : IRepositorySignale<Signale>
    {
        readonly DataContext? dataContext;
        public SignaleManager() { }
        public SignaleManager(DataContext context)
        {
            dataContext = context;
        }
        public async Task<ActionResult<IEnumerable<Signale>>> GetAll()
        {
            return await dataContext.Signales.ToListAsync();
        }

        public async Task<ActionResult<Signale>> GetByIds(int idAnnonce, int idProfil)
        {
            return await dataContext.Signales.FirstOrDefaultAsync(c => c.AnnonceId == idAnnonce && c.ProfilId == idProfil);
        }
        public async Task<ActionResult<Signale>> GetByIdProfil(int idProfil)
        {
            return await dataContext.Signales.FirstOrDefaultAsync(u => u.ProfilId == idProfil);
        }

        public async Task<ActionResult<Signale>> GetByIdAnnonce(int idAnnonce)
        {
            return await dataContext.Signales.FirstOrDefaultAsync(u => u.AnnonceId == idAnnonce);
        }

        public async Task Add(Signale entity)
        {
            dataContext.Signales.Add(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(Signale signale, Signale entity)
        {
            dataContext.Entry(signale).State = EntityState.Modified;          
            
            
            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(Signale signale)
        {
            dataContext.Signales.Remove(signale);
            await dataContext.SaveChangesAsync();
        }
    }
}
