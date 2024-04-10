using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class AdresseManager : IRepository<Adresse>
    {
        readonly DataContext? dataContext;
        public AdresseManager() { }
        public AdresseManager(DataContext context)
        {
            dataContext = context;
        }
        public async Task<ActionResult<IEnumerable<Adresse>>> GetAll()
        {
            List<Adresse> adresses = await dataContext.Adresses.ToListAsync();
            List<Ville> villes = (await new VilleManager(dataContext).GetAll()).Value.ToList();
            foreach (Ville v in villes)
            {
                v.AdressesVille = null;
            }
            return adresses;
        }

        public async Task<ActionResult<Adresse>> GetById(int id)
        {
            return await dataContext.Adresses.FindAsync(id);
        }
        public async Task Add(Adresse entity)
        {
            await dataContext.Adresses.AddAsync(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(Adresse adresse, Adresse entity)
        {
            dataContext.Entry(adresse).State = EntityState.Modified;            
            adresse.CodeInsee = entity.CodeInsee;
            adresse.Numero = entity.Numero;
            adresse.Rue = entity.Rue;
            
            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(Adresse adresse)
        {
            dataContext.Adresses.Remove(adresse);
            await dataContext.SaveChangesAsync();
        }
    }
}
