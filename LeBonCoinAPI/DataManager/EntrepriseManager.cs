using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class EntrepriseManager : IRepositoryEntreprise<Entreprise>
    {
        readonly DataContext? dataContext;
        public EntrepriseManager() { }
        public EntrepriseManager(DataContext context)
        {
            dataContext = context;
        }
        public ActionResult<IEnumerable<Entreprise>> GetAll()
        {
            return dataContext.Entreprises.ToList();
        }

        public ActionResult<Entreprise> GetById(int id)
        {
            return dataContext.Entreprises.FirstOrDefault(u => u.ProfilId == id);
        }
        public void Add(Entreprise entity)
        {
            dataContext.Entreprises.Add(entity);
            dataContext.SaveChanges();
        }
        public void Update(Entreprise entreprise, Entreprise entity)
        {
            dataContext.Entry(entreprise).State = EntityState.Modified;
            entreprise.ProfilId = entity.ProfilId;
            entreprise.HashMotDePasse = entity.HashMotDePasse;
            entreprise.Telephone = entity.Telephone;
            entreprise.SecteurId = entity.SecteurId;
            entreprise.Siret = entity.Siret;
            entreprise.AdresseId = entity.AdresseId;
            entreprise.Nom = entity.Nom;
            
            dataContext.SaveChanges();
        }
        public void Delete(Entreprise entreprise)
        {
            dataContext.Entreprises.Remove(entreprise);
            dataContext.SaveChanges();
        }
    }
}
