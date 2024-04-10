using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

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
        public async Task<ActionResult<IEnumerable<Admin>>> GetAll()
        {
            return await dataContext.Admins.ToListAsync();
        }

        public async Task<ActionResult<Admin>> GetById(int id)
        {
            return await dataContext.Admins.FindAsync(id);
        }
        public async Task Add(Admin entity)
        {
            StringBuilder sb = new StringBuilder();
            byte[] hashValue = SHA512.HashData(Encoding.UTF8.GetBytes(entity.HashMotDePasse));
            foreach (byte b in hashValue)
            {
                sb.Append($"{b:X2}");
            }
            entity.HashMotDePasse = sb.ToString().ToUpper();

            await dataContext.Admins.AddAsync(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(Admin admin, Admin entity)
        {
            dataContext.Entry(admin).State = EntityState.Modified;

            StringBuilder sb = new StringBuilder();
            byte[] hashValue = SHA512.HashData(Encoding.UTF8.GetBytes(entity.HashMotDePasse));
            foreach (byte b in hashValue)
            {
                sb.Append($"{b:X2}");
            }
            admin.HashMotDePasse = sb.ToString().ToUpper();

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
