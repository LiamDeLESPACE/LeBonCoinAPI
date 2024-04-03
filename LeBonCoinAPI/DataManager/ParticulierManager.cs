using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class ParticulierManager : IRepositoryParticulier<Particulier>
    {
        readonly DataContext? dataContext;
        public ParticulierManager() { }
        public ParticulierManager(DataContext context)
        {
            dataContext = context;
        }
        public ActionResult<IEnumerable<Particulier>> GetAll()
        {
            return dataContext.Particuliers.ToList();
        }

        public ActionResult<Particulier> GetById(int id)
        {
            return dataContext.Particuliers.FirstOrDefault(u => u.ProfilId == id);
        }
        public void Add(Particulier entity)
        {
            dataContext.Particuliers.Add(entity);
            dataContext.SaveChanges();
        }
        public void Update(Particulier particulier, Particulier entity)
        {
            dataContext.Entry(particulier).State = EntityState.Modified;            
            particulier.HashMotDePasse = entity.HashMotDePasse;
            particulier.Telephone = entity.Telephone;
            particulier.Email = entity.Email;
            particulier.Civilite = entity.Civilite;
            particulier.Nom = entity.Nom;
            particulier.Prenom = entity.Prenom;
            particulier.DateNaissance = entity.DateNaissance;
            particulier.AdresseId = entity.AdresseId;

            dataContext.SaveChanges();
        }
        public void Delete(Particulier particulier)
        {
            dataContext.Particuliers.Remove(particulier);
            dataContext.SaveChanges();
        }
    }
}
