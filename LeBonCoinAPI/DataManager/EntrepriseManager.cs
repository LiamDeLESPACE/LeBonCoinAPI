using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class EntrepriseManager : IRepository<Entreprise>
    {
        readonly DataContext? dataContext;
        public EntrepriseManager() { }
        public EntrepriseManager(DataContext context)
        {
            dataContext = context;
        }
        public async Task<ActionResult<IEnumerable<Entreprise>>> GetAll()
        {
            return await dataContext.Entreprises.ToListAsync();
        }

        public async Task<ActionResult<Entreprise>> GetById(int id)
        {
            return await dataContext.Entreprises.FindAsync(id);
        }
        public async Task Add(Entreprise entity)
        {
            await dataContext.Entreprises.AddAsync(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(Entreprise entreprise, Entreprise entity)
        {
            dataContext.Entry(entreprise).State = EntityState.Modified;            
            entreprise.HashMotDePasse = entity.HashMotDePasse;
            entreprise.Telephone = entity.Telephone;
            entreprise.SecteurId = entity.SecteurId;
            entreprise.Siret = entity.Siret;
            entreprise.AdresseId = entity.AdresseId;
            entreprise.Nom = entity.Nom;
            
            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(Entreprise entreprise)
        {
            dataContext.Entreprises.Remove(entreprise);
            await dataContext.SaveChangesAsync();
        }
    }
}
