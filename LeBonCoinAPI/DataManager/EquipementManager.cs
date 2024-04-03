using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class EquipementManager : IRepositoryEquipement<Equipement>
    {
        readonly DataContext? dataContext;
        public EquipementManager() { }
        public EquipementManager(DataContext context)
        {
            dataContext = context;
        }
        public ActionResult<IEnumerable<Equipement>> GetAll()
        {
            return dataContext.Equipements.ToList();
        }

        public ActionResult<Equipement> GetById(int id)
        {
            return dataContext.Equipements.FirstOrDefault(u => u.EquipementId == id);
        }
        public void Add(Equipement entity)
        {
            dataContext.Equipements.Add(entity);
            dataContext.SaveChanges();
        }
        public void Update(Equipement equipement, Equipement entity)
        {
            dataContext.Entry(equipement).State = EntityState.Modified;
            equipement.EquipementId = entity.EquipementId;
            equipement.TypeEquipementId = entity.TypeEquipementId;
            equipement.Nom = entity.Nom;
            equipement.EquipementsPossedesDesEquipement = entity.EquipementsPossedesDesEquipement;
            
            dataContext.SaveChanges();
        }
        public void Delete(Equipement equipement)
        {
            dataContext.Equipements.Remove(equipement);
            dataContext.SaveChanges();
        }
    }
}
