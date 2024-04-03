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
        public ActionResult<IEnumerable<Favoris>> GetAll()
        {
            return dataContext.lesFavoris.ToList();
        }

        public ActionResult<Favoris> GetByIds(int idProfil, int idAnnonce)
        {
            return dataContext.lesFavoris.FirstOrDefault(c => c.ProfilId == idProfil && c.AnnonceId == idAnnonce);
        }

        public ActionResult<Favoris> GetByIdProfil(int idProfil)
        {
            return dataContext.lesFavoris.FirstOrDefault(u => u.ProfilId == idProfil);
        }
        public void Add(Favoris entity)
        {
            dataContext.lesFavoris.Add(entity);
            dataContext.SaveChanges();
        }
        public void Update(Favoris favoris, Favoris entity)
        {
            dataContext.Entry(favoris).State = EntityState.Modified;
            favoris.AnnonceId = entity.AnnonceId;
            favoris.ProfilId = entity.ProfilId;

            dataContext.SaveChanges();
        }
        public void Delete(Favoris favoris)
        {
            dataContext.lesFavoris.Remove(favoris);
            dataContext.SaveChanges();
        }
    }
}
