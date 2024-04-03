using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class CarteBancaireManager : IRepositoryCarteBancaire<CarteBancaire>
    {
        readonly DataContext? dataContext;
        public CarteBancaireManager() { }
        public CarteBancaireManager(DataContext context)
        {
            dataContext = context;
        }
        public ActionResult<IEnumerable<CarteBancaire>> GetAll()
        {
            return dataContext.CarteBancaires.ToList();
        }

        public ActionResult<CarteBancaire> GetById(int id)
        {
            return dataContext.CarteBancaires.FirstOrDefault(u => u.CarteId == id);
        }
        public void Add(CarteBancaire entity)
        {
            dataContext.CarteBancaires.Add(entity);
            dataContext.SaveChanges();
        }
        public void Update(CarteBancaire carteBancaire, CarteBancaire entity)
        {
            dataContext.Entry(carteBancaire).State = EntityState.Modified;
            carteBancaire.CarteId = entity.CarteId;
            carteBancaire.ProfilId = entity.ProfilId;
            carteBancaire.Numero = entity.Numero;
            
            dataContext.SaveChanges();
        }
        public void Delete(CarteBancaire carteBancaire)
        {
            dataContext.CarteBancaires.Remove(carteBancaire);
            dataContext.SaveChanges();
        }
    }
}
