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
        public ActionResult<IEnumerable<Reglement>> GetAll()
        {
            return dataContext.Reglements.ToList();
        }

        public ActionResult<Reglement> GetByString(string id)
        {
            return dataContext.Reglements.FirstOrDefault(u => u.ReglementId.ToUpper() == id.ToUpper());
        }
        public void Add(Reglement entity)
        {
            dataContext.Reglements.Add(entity);
            dataContext.SaveChanges();
        }
        public void Update(Reglement reglement, Reglement entity)
        {
            dataContext.Entry(reglement).State = EntityState.Modified;            
            reglement.ReservationId = entity.ReservationId;
           
            dataContext.SaveChanges();
        }
        public void Delete(Reglement reglement)
        {
            dataContext.Reglements.Remove(reglement);
            dataContext.SaveChanges();
        }
    }
}
