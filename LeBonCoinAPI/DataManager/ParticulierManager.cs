using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class ParticulierManager : IRepository<Particulier>
    {
        readonly DataContext? dataContext;
        public ParticulierManager() { }
        public ParticulierManager(DataContext context)
        {
            dataContext = context;
        }
        public async Task<ActionResult<IEnumerable<Particulier>>> GetAll()
        {
            return await dataContext.Particuliers.ToListAsync();
        }

        public async Task<ActionResult<Particulier>> GetById(int id)
        {
            return await dataContext.Particuliers.FindAsync(id);
        }
        public async Task Add(Particulier entity)
        {
            dataContext.Particuliers.Add(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(Particulier particulier, Particulier entity)
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

            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(Particulier particulier)
        {
            dataContext.Particuliers.Remove(particulier);
            await dataContext.SaveChangesAsync();
        }
    }
}
