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
        public async Task<ActionResult<IEnumerable<TypeEquipement>>> GetAll()
        {
            return await dataContext.TypeEquipements.ToListAsync();
        }

        public async Task<ActionResult<TypeEquipement>> GetById(int id)
        {
            return await dataContext.TypeEquipements.FirstOrDefaultAsync(u => u.TypeEquipementId == id);
        }
        public async Task Add(TypeEquipement entity)
        {
            await dataContext.TypeEquipements.AddAsync(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(TypeEquipement typeEquipement, TypeEquipement entity)
        {
            dataContext.Entry(typeEquipement).State = EntityState.Modified;            
            typeEquipement.Nom = entity.Nom;            
            
            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(TypeEquipement typeEquipement)
        {
            dataContext.TypeEquipements.Remove(typeEquipement);
            await dataContext.SaveChangesAsync();
        }
    }
}
