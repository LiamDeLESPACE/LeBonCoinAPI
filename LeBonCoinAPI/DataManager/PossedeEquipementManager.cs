using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class PossedeEquipementManager : IRepositoryPossedeEquipement<PossedeEquipement>
    {
        readonly DataContext? dataContext;
        public PossedeEquipementManager() { }
        public PossedeEquipementManager(DataContext context)
        {
            dataContext = context;
        }
        public ActionResult<IEnumerable<PossedeEquipement>> GetAll()
        {
            return dataContext.PossedeEquipements.ToList();
        }

        public ActionResult<PossedeEquipement> GetByIds(int idAnnonce, int idEquipement)
        {
            return dataContext.PossedeEquipements.FirstOrDefault(c => c.AnnonceId == idAnnonce && c.EquipementId == idEquipement);
        }

        public ActionResult<PossedeEquipement> GetByIdAnnonce(int idAnnonce)
        {
            return dataContext.PossedeEquipements.FirstOrDefault(u => u.AnnonceId == idAnnonce);
        }
        public ActionResult<PossedeEquipement> GetByIdEquipement(int idEquipement)
        {
            return dataContext.PossedeEquipements.FirstOrDefault(u => u.EquipementId == idEquipement);
        }
        public void Add(PossedeEquipement entity)
        {
            dataContext.PossedeEquipements.Add(entity);
            dataContext.SaveChanges();
        }
        public void Update(PossedeEquipement possedeEquipement, PossedeEquipement entity)
        {
            dataContext.Entry(possedeEquipement).State = EntityState.Modified;            

            dataContext.SaveChanges();
        }
        public void Delete(PossedeEquipement possedeEquipement)
        {
            dataContext.PossedeEquipements.Remove(possedeEquipement);
            dataContext.SaveChanges();
        }
    }
}
