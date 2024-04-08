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
        public async Task<ActionResult<IEnumerable<TypeLogement>>> GetAll()
        {
            return await dataContext.TypeLogements.ToListAsync();
        }

        public async Task<ActionResult<TypeLogement>> GetById(int id)
        {
            return await dataContext.TypeLogements.FirstOrDefaultAsync(u => u.TypeLogementId == id);
        }
        public async Task Add(TypeLogement entity)
        {
            await dataContext.TypeLogements.AddAsync(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(TypeLogement typeLogement, TypeLogement entity)
        {
            dataContext.Entry(typeLogement).State = EntityState.Modified;            
            typeLogement.Nom = entity.Nom;            

            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(TypeLogement typeLogement)
        {
            dataContext.TypeLogements.Remove(typeLogement);
            await dataContext.SaveChangesAsync();
        }
    }
}
