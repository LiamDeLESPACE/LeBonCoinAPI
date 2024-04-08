using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class AdminManager : IRepository<Admin>
    {
        readonly DataContext? dataContext;
        public AdminManager() { }
        public AdminManager(DataContext context)
        {
            dataContext = context;
        }
        public ActionResult<IEnumerable<Admin>> GetAll()
        {
            return dataContext.Admins.ToList();
        }

        public async Task<ActionResult<Admin>> GetById(int id)
        {
            return await dataContext.Admins.FindAsync(id);
        }
        public void Add(Admin entity)
        {
            dataContext.Admins.Add(entity);
            dataContext.SaveChanges();
        }
        public void Update(Admin admin, Admin entity)
        {
            dataContext.Entry(admin).State = EntityState.Modified;
            
            admin.HashMotDePasse = entity.HashMotDePasse;
            admin.Telephone = entity.Telephone;
            admin.Service = entity.Service;
            admin.Email = entity.Email;

            dataContext.SaveChanges();
        }
        public void Delete(Admin admin)
        {
            dataContext.Admins.Remove(admin);
            dataContext.SaveChanges();
        }
    }
}
