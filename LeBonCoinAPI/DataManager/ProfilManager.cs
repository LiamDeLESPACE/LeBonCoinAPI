using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class ProfilManager : IRepository<Profil>
    {
        readonly DataContext? dataContext;
        public ProfilManager() { }
        public ProfilManager(DataContext context)
        {
            dataContext = context;
        }
        public async Task<ActionResult<IEnumerable<Profil>>> GetAll()
        {
            return await dataContext.Profils.ToListAsync();
        }

        public async Task<ActionResult<Profil>> GetById(int id)
        {
            return await dataContext.Profils.FindAsync(id);
        }
        public async Task Add(Profil entity)
        {
            await dataContext.Profils.AddAsync(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(Profil profil, Profil entity)
        {
            dataContext.Entry(profil).State = EntityState.Modified;            
            profil.HashMotDePasse = entity.HashMotDePasse;
            profil.Telephone = entity.Telephone;           

            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(Profil profil)
        {
            dataContext.Profils.Remove(profil);
            await dataContext.SaveChangesAsync();
        }
    }
}
