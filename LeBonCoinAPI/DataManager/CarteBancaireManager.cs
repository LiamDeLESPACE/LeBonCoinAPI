using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class CarteBancaireManager : IRepository<CarteBancaire>
    {
        readonly DataContext? dataContext;

        public CarteBancaireManager(DataContext context)
        {
            dataContext = context;
        }
        public async Task<ActionResult<IEnumerable<CarteBancaire>>> GetAll()
        {
            return await dataContext.CarteBancaires.ToListAsync();
        }

        public async Task<ActionResult<CarteBancaire>> GetById(int id)
        {
            return await dataContext.CarteBancaires.FindAsync(id);
        }
        public async Task Add(CarteBancaire entity)
        {
            await dataContext.CarteBancaires.AddAsync(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(CarteBancaire carteBancaire, CarteBancaire entity)
        {
            dataContext.Entry(carteBancaire).State = EntityState.Modified;            
            carteBancaire.ProfilId = entity.ProfilId;
            carteBancaire.Numero = entity.Numero;
            
            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(CarteBancaire carteBancaire)
        {
            dataContext.CarteBancaires.Remove(carteBancaire);
            await dataContext.SaveChangesAsync();
        }
    }
}
