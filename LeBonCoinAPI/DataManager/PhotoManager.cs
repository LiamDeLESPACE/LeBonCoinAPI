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
        public ActionResult<IEnumerable<Photo>> GetAll()
        {
            return dataContext.Photos.ToList();
        }

        public ActionResult<Photo> GetById(int id)
        {
            return dataContext.Photos.FirstOrDefault(u => u.PhotoId == id);
        }

        public ActionResult<Photo> GetByIdProfil(int idProfil)
        {
            return dataContext.Photos.FirstOrDefault(u => u.ProfilId == idProfil);
        }

        public ActionResult<Photo> GetByIdAnnonce(int idAnnonce)
        {
            return dataContext.Photos.FirstOrDefault(u => u.AnnonceId == idAnnonce);
        }
        public void Add(Photo entity)
        {
            dataContext.Photos.Add(entity);
            dataContext.SaveChanges();
        }
        public void Update(Photo photo, Photo entity)
        {
            dataContext.Entry(photo).State = EntityState.Modified;            
            photo.ProfilId = entity.ProfilId;
            photo.AnnonceId = entity.AnnonceId;
            photo.URL = entity.URL;
            
            dataContext.SaveChanges();
        }
        public void Delete(Photo photo)
        {
            dataContext.Photos.Remove(photo);
            dataContext.SaveChanges();
        }
    }
}
