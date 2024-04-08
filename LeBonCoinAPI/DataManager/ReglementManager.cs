using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class ReglementManager : IRepositoryReglement<Reglement>
    {
        readonly DataContext? dataContext;
        public ReglementManager() { }
        public ReglementManager(DataContext context)
        {
            dataContext = context;
        }
        public async Task<ActionResult<IEnumerable<Reglement>>> GetAll()
        {
            return await dataContext.Reglements.ToListAsync();
        }

        public async Task<ActionResult<Reglement>> GetByString(string id)
        {
            return await dataContext.Reglements.FirstOrDefaultAsync(u => u.ReglementId.ToUpper() == id.ToUpper());
        }
        public async Task Add(Reglement entity)
        {
            await dataContext.Reglements.AddAsync(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(Reglement reglement, Reglement entity)
        {
            dataContext.Entry(reglement).State = EntityState.Modified;            
            reglement.ReservationId = entity.ReservationId;
           
            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(Reglement reglement)
        {
            dataContext.Reglements.Remove(reglement);
            await dataContext.SaveChangesAsync();
        }
    }
}
