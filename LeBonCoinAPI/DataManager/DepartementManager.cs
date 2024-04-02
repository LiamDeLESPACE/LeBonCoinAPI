using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class DepartementManager : IRepositoryDepartement<Departement>
    {
        readonly DataContext? dataContext;
        public DepartementManager() { }
        public DepartementManager(DataContext context)
        {
            dataContext = context;
        }
        public ActionResult<IEnumerable<Departement>> GetAll()
        {
            return dataContext.Departements.ToList();
        }

        public ActionResult<Departement> GetByString(string nom)
        {
            return dataContext.Departements.FirstOrDefault(u => u.Nom.ToUpper() == nom.ToUpper());
        }
        public void Add(Departement entity)
        {
            dataContext.Departements.Add(entity);
            dataContext.SaveChanges();
        }
        public void Update(Departement departement, Departement entity)
        {
            dataContext.Entry(departement).State = EntityState.Modified;
            departement.DepartementCode = entity.DepartementCode;
            departement.Nom = entity.Nom;

            departement.VillesDepartement = entity.VillesDepartement;
            dataContext.SaveChanges();
        }
        public void Delete(Departement departement)
        {
            dataContext.Departements.Remove(departement);
            dataContext.SaveChanges();
        }
    }
}
