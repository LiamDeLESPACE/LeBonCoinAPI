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
        public ActionResult<IEnumerable<Signale>> GetAll()
        {
            return dataContext.Signales.ToList();
        }

        public ActionResult<Signale> GetByIds(int idAnnonce, int idProfil)
        {
            return dataContext.Signales.FirstOrDefault(c => c.AnnonceId == idAnnonce && c.ProfilId == idProfil);
        }
        public ActionResult<Signale> GetByIdProfil(int idProfil)
        {
            return dataContext.Signales.FirstOrDefault(u => u.ProfilId == idProfil);
        }

        public ActionResult<Signale> GetByIdAnnonce(int idAnnonce)
        {
            return dataContext.Signales.FirstOrDefault(u => u.AnnonceId == idAnnonce);
        }

        public void Add(Signale entity)
        {
            dataContext.Signales.Add(entity);
            dataContext.SaveChanges();
        }
        public void Update(Signale signale, Signale entity)
        {
            dataContext.Entry(signale).State = EntityState.Modified;          
            
            
            dataContext.SaveChanges();
        }
        public void Delete(Signale signale)
        {
            dataContext.Signales.Remove(signale);
            dataContext.SaveChanges();
        }
    }
}
