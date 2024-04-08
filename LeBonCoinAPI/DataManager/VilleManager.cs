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
        public async Task<ActionResult<IEnumerable<Ville>>> GetAll()
        {
            return await dataContext.Villes.ToListAsync();
        }

        public async Task<ActionResult<Ville>> GetByString(string codeInsee)
        {
            return await dataContext.Villes.FirstOrDefaultAsync(u => u.CodeInsee.ToUpper() == codeInsee.ToUpper());
        }
        public async Task Add(Ville entity)
        {
            await dataContext.Villes.AddAsync(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(Ville ville, Ville entity)
        {
            dataContext.Entry(ville).State = EntityState.Modified;
            ville.DepartementCode = entity.DepartementCode;
            ville.Nom = entity.Nom;
            ville.CodePostal = entity.CodePostal; 
                        
            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(Ville ville)
        {
            dataContext.Villes.Remove(ville);
            await dataContext.SaveChangesAsync();
        }
    }
}
