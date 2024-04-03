using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class AdresseManager : IRepositoryAdresse<Adresse>
    {
        readonly DataContext? dataContext;
        public AdresseManager() { }
        public AdresseManager(DataContext context)
        {
            dataContext = context;
        }
        public ActionResult<IEnumerable<Adresse>> GetAll()
        {
            return dataContext.Adresses.ToList();
        }

        public ActionResult<Adresse> GetById(int id)
        {
            return dataContext.Adresses.FirstOrDefault(u => u.AdresseId == id);
        }
        public void Add(Adresse entity)
        {
            dataContext.Adresses.Add(entity);
            dataContext.SaveChanges();
        }
        public void Update(Adresse adresse, Adresse entity)
        {
            dataContext.Entry(adresse).State = EntityState.Modified;            
            adresse.CodeInsee = entity.CodeInsee;
            adresse.Numero = entity.Numero;
            adresse.Rue = entity.Rue;
            
            dataContext.SaveChanges();
        }
        public void Delete(Adresse adresse)
        {
            dataContext.Adresses.Remove(adresse);
            dataContext.SaveChanges();
        }
    }
}
