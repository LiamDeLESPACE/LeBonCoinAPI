using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class EquipementManager : IRepository<Equipement>
    {
        readonly DataContext? dataContext;
        public EquipementManager() { }
        public EquipementManager(DataContext context)
        {
            dataContext = context;
        }
        public async Task<ActionResult<IEnumerable<Equipement>>> GetAll()
        {
            return await dataContext.Equipements.ToListAsync();
        }

        public async Task<ActionResult<Equipement>> GetById(int id)
        {
            return await dataContext.Equipements.FindAsync(id);
        }
        public async Task Add(Equipement entity)
        {
            await dataContext.Equipements.AddAsync(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(Equipement equipement, Equipement entity)
        {
            dataContext.Entry(equipement).State = EntityState.Modified;            
            equipement.TypeEquipementId = entity.TypeEquipementId;
            equipement.Nom = entity.Nom;

            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(Equipement equipement)
        {
            dataContext.Equipements.Remove(equipement);
            await dataContext.SaveChangesAsync();
        }
    }
}
