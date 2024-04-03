using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class VilleManager : IRepositoryVille<Ville>
    {
        readonly DataContext? dataContext;
        public VilleManager() { }
        public VilleManager(DataContext context)
        {
            dataContext = context;
        }
        public ActionResult<IEnumerable<Ville>> GetAll()
        {
            return dataContext.Villes.ToList();
        }

        public ActionResult<Ville> GetByString(string codeInsee)
        {
            return dataContext.Villes.FirstOrDefault(u => u.CodeInsee.ToUpper() == codeInsee.ToUpper());
        }
        public void Add(Ville entity)
        {
            dataContext.Villes.Add(entity);
            dataContext.SaveChanges();
        }
        public void Update(Ville ville, Ville entity)
        {
            dataContext.Entry(ville).State = EntityState.Modified;
            ville.DepartementCode = entity.DepartementCode;
            ville.Nom = entity.Nom;
            ville.CodePostal = entity.CodePostal; 
                        
            dataContext.SaveChanges();
        }
        public void Delete(Ville ville)
        {
            dataContext.Villes.Remove(ville);
            dataContext.SaveChanges();
        }
    }
}
