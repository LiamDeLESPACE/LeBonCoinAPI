using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class AnnonceManager : IRepositoryAnnonce<Annonce>
    {
        readonly DataContext? dataContext;
        public AnnonceManager() { }
        public AnnonceManager(DataContext context)
        {
            dataContext = context;
        }
        public ActionResult<IEnumerable<Annonce>> GetAll()
        {
            return dataContext.Annonces.ToList();
        }

        public ActionResult<Annonce> GetById(int id)
        {
            return dataContext.Annonces.FirstOrDefault(u => u.AnnonceId == id);
        }
        public void Add(Annonce entity)
        {
            dataContext.Annonces.Add(entity);
            dataContext.SaveChanges();
        }
        public void Update(Annonce annonce, Annonce entity)
        {
            dataContext.Entry(annonce).State = EntityState.Modified;            
            annonce.AdresseId = entity.AdresseId;
            annonce.TypeLogementId = entity.TypeLogementId;
            annonce.ProfilId = entity.ProfilId;
            annonce.Titre = entity.Titre;
            annonce.DureeMinimumSejour = entity.DureeMinimumSejour;
            annonce.Active = entity.Active;
            annonce.DatePublication = entity.DatePublication;
            annonce.Description = entity.Description;
            annonce.Etoile = entity.Etoile;
            annonce.NombrePersonnesMax = entity.NombrePersonnesMax;
            annonce.PrixParNuit = entity.PrixParNuit;
            annonce.NombreChambres = entity.NombreChambres;            

            dataContext.SaveChanges();
        }
        public void Delete(Annonce annonce)
        {
            dataContext.Annonces.Remove(annonce);
            dataContext.SaveChanges();
        }
    }
}
