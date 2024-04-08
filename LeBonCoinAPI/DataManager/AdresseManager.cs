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
        public async Task<ActionResult<IEnumerable<Adresse>>> GetAll()
        {
            return await dataContext.Adresses.ToListAsync();
        }

        public async Task<ActionResult<Adresse>> GetById(int id)
        {
            return await dataContext.Adresses.FirstOrDefaultAsync(u => u.AdresseId == id);
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
