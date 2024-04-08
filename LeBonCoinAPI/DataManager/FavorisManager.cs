using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class FavorisManager : IRepositoryFavoris<Favoris>
    {
        readonly DataContext? dataContext;
        public FavorisManager() { }
        public FavorisManager(DataContext context)
        {
            dataContext = context;
        }
        public async Task<ActionResult<IEnumerable<Favoris>>> GetAll()
        {
            return await dataContext.lesFavoris.ToListAsync();
        }

        public async Task<ActionResult<Favoris>> GetByIds(int idProfil, int idAnnonce)
        {
            return await dataContext.lesFavoris.FirstOrDefaultAsync(c => c.ProfilId == idProfil && c.AnnonceId == idAnnonce);
        }

        public async Task<ActionResult<IEnumerable<Favoris>>> GetByIdProfil(int idProfil)
        {
            return await (from f in dataContext.lesFavoris where f.ProfilId == idProfil select f).ToListAsync();
        }
        public async Task Add(Favoris entity)
        {
            await dataContext.lesFavoris.AddAsync(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(Favoris favoris, Favoris entity)
        {
            dataContext.Entry(favoris).State = EntityState.Modified;            

            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(Favoris favoris)
        {
            dataContext.lesFavoris.Remove(favoris);
            await dataContext.SaveChangesAsync();
        }
    }
}
