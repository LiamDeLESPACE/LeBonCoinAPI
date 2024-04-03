using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class ProfilManager : IRepositoryProfil<Profil>
    {
        readonly DataContext? dataContext;
        public ProfilManager() { }
        public ProfilManager(DataContext context)
        {
            dataContext = context;
        }
        public ActionResult<IEnumerable<Profil>> GetAll()
        {
            return dataContext.Profils.ToList();
        }

        public ActionResult<Profil> GetById(int id)
        {
            return dataContext.Profils.FirstOrDefault(u => u.ProfilId == id);
        }
        public void Add(Profil entity)
        {
            dataContext.Profils.Add(entity);
            dataContext.SaveChanges();
        }
        public void Update(Profil profil, Profil entity)
        {
            dataContext.Entry(profil).State = EntityState.Modified;            
            profil.HashMotDePasse = entity.HashMotDePasse;
            profil.Telephone = entity.Telephone;           

            dataContext.SaveChanges();
        }
        public void Delete(Profil profil)
        {
            dataContext.Profils.Remove(profil);
            dataContext.SaveChanges();
        }
    }
}
