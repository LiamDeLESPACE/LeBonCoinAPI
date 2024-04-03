using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class TypeLogementManager : IRepositoryTypeLogement<TypeLogement>
    {
        readonly DataContext? dataContext;
        public TypeLogementManager() { }
        public TypeLogementManager(DataContext context)
        {
            dataContext = context;
        }
        public ActionResult<IEnumerable<TypeLogement>> GetAll()
        {
            return dataContext.TypeLogements.ToList();
        }

        public ActionResult<TypeLogement> GetById(int id)
        {
            return dataContext.TypeLogements.FirstOrDefault(u => u.TypeLogementId == id);
        }
        public void Add(TypeLogement entity)
        {
            dataContext.TypeLogements.Add(entity);
            dataContext.SaveChanges();
        }
        public void Update(TypeLogement typeLogement, TypeLogement entity)
        {
            dataContext.Entry(typeLogement).State = EntityState.Modified;            
            typeLogement.Nom = entity.Nom;            

            dataContext.SaveChanges();
        }
        public void Delete(TypeLogement typeLogement)
        {
            dataContext.TypeLogements.Remove(typeLogement);
            dataContext.SaveChanges();
        }
    }
}
