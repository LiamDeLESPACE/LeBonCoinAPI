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
        public async Task<ActionResult<IEnumerable<Departement>>> GetAll()
        {
            return await dataContext.Departements.ToListAsync();
        }

        public async Task<ActionResult<Departement>> GetByString(string nom)
        {
            return await dataContext.Departements.FirstOrDefaultAsync(u => u.Nom.ToUpper() == nom.ToUpper());
        }
        public async Task Add(Departement entity)
        {
            await dataContext.Departements.AddAsync(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(Departement departement, Departement entity)
        {
            dataContext.Entry(departement).State = EntityState.Modified;            
            departement.Nom = entity.Nom;
            
            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(Departement departement)
        {
            dataContext.Departements.Remove(departement);
            await dataContext.SaveChangesAsync();
        }
    }
}
