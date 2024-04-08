using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class PhotoManager : IRepositoryPhoto<Photo>
    {
        readonly DataContext? dataContext;
        public PhotoManager() { }
        public PhotoManager(DataContext context)
        {
            dataContext = context;
        }
        public async  Task<ActionResult<IEnumerable<Photo>>> GetAll()
        {
            return await dataContext.Photos.ToListAsync();
        }

        public async Task<ActionResult<Photo>> GetById(int id)
        {
            return await dataContext.Photos.FindAsync(id);
        }

        public async Task<ActionResult<Photo>> GetByIdProfil(int idProfil)
        {
            return await dataContext.Photos.FirstOrDefaultAsync(u => u.ProfilId == idProfil);
        }

        public async Task<ActionResult<Photo>> GetByIdAnnonce(int idAnnonce)
        {
            return await dataContext.Photos.FirstOrDefaultAsync(u => u.AnnonceId == idAnnonce);
        }
        public async Task Add(Photo entity)
        {
            await dataContext.Photos.AddAsync(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(Photo photo, Photo entity)
        {
            dataContext.Entry(photo).State = EntityState.Modified;            
            photo.ProfilId = entity.ProfilId;
            photo.AnnonceId = entity.AnnonceId;
            photo.URL = entity.URL;
            
            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(Photo photo)
        {
            dataContext.Photos.Remove(photo);
            await dataContext.SaveChangesAsync();
        }
    }
}
