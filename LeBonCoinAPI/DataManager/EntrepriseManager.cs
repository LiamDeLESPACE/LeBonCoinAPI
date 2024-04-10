using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

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
            StringBuilder sb = new StringBuilder();
            byte[] hashValue = SHA512.HashData(Encoding.UTF8.GetBytes(entity.HashMotDePasse));
            foreach (byte b in hashValue)
            {
                sb.Append($"{b:X2}");
            }
            entity.HashMotDePasse = sb.ToString().ToUpper();

            await dataContext.Entreprises.AddAsync(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(Entreprise entreprise, Entreprise entity)
        {
            dataContext.Entry(entreprise).State = EntityState.Modified;

            StringBuilder sb = new StringBuilder();
            byte[] hashValue = SHA512.HashData(Encoding.UTF8.GetBytes(entity.HashMotDePasse));
            foreach (byte b in hashValue)
            {
                sb.Append($"{b:X2}");
            }
            entreprise.HashMotDePasse = sb.ToString().ToUpper();

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
