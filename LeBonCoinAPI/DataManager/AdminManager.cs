using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class AdminManager : IRepositoryAdmin<Admin>
    {
        readonly DataContext? dataContext;
        public AdminManager() { }
        public AdminManager(DataContext context)
        {
            dataContext = context;
        }
        public async Task<ActionResult<IEnumerable<Admin>>> GetAll()
        {
            return await dataContext.Admins.ToListAsync();
        }

        public ActionResult<Admin> GetById(int id)
        {
            return dataContext.Admins.FirstOrDefault(u => u.ProfilId == id);
        }
        public async Task Add(Admin entity)
        {
            await dataContext.Admins.AddAsync(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(Admin admin, Admin entity)
        {
            dataContext.Entry(admin).State = EntityState.Modified;
            
            admin.HashMotDePasse = entity.HashMotDePasse;
            admin.Telephone = entity.Telephone;
            admin.Service = entity.Service;
            admin.Email = entity.Email;

            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(Admin admin)
        {
            dataContext.Admins.Remove(admin);
            await dataContext.SaveChangesAsync();
        }
    }
}
