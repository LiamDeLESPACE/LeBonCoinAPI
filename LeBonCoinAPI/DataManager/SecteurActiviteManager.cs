using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class SecteurActiviteManager : IRepositorySecteurActivite<SecteurActivite>
    {
        readonly DataContext? dataContext;
        public SecteurActiviteManager() { }
        public SecteurActiviteManager(DataContext context)
        {
            dataContext = context;
        }
        public ActionResult<IEnumerable<SecteurActivite>> GetAll()
        {
            return dataContext.SecteurActivites.ToList();
        }

        public ActionResult<SecteurActivite> GetById(int id)
        {
            return dataContext.SecteurActivites.FirstOrDefault(u => u.SecteurId == id);
        }
        public void Add(SecteurActivite entity)
        {
            dataContext.SecteurActivites.Add(entity);
            dataContext.SaveChanges();
        }
        public void Update(SecteurActivite secteurActivite, SecteurActivite entity)
        {
            dataContext.Entry(secteurActivite).State = EntityState.Modified;            
            secteurActivite.NomSecteur = entity.NomSecteur;            
            
            dataContext.SaveChanges();
        }
        public void Delete(SecteurActivite secteurActivite)
        {
            dataContext.SecteurActivites.Remove(secteurActivite);
            dataContext.SaveChanges();
        }
    }
}
