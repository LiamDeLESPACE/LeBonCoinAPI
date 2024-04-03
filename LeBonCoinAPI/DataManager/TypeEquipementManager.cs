using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class TypeEquipementManager : IRepositoryTypeEquipement<TypeEquipement>
    {
        readonly DataContext? dataContext;
        public TypeEquipementManager() { }
        public TypeEquipementManager(DataContext context)
        {
            dataContext = context;
        }
        public ActionResult<IEnumerable<TypeEquipement>> GetAll()
        {
            return dataContext.TypeEquipements.ToList();
        }

        public ActionResult<TypeEquipement> GetById(int id)
        {
            return dataContext.TypeEquipements.FirstOrDefault(u => u.TypeEquipementId == id);
        }
        public void Add(TypeEquipement entity)
        {
            dataContext.TypeEquipements.Add(entity);
            dataContext.SaveChanges();
        }
        public void Update(TypeEquipement typeEquipement, TypeEquipement entity)
        {
            dataContext.Entry(typeEquipement).State = EntityState.Modified;            
            typeEquipement.Nom = entity.Nom;            
            
            dataContext.SaveChanges();
        }
        public void Delete(TypeEquipement typeEquipement)
        {
            dataContext.TypeEquipements.Remove(typeEquipement);
            dataContext.SaveChanges();
        }
    }
}
