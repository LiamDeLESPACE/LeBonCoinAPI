using Microsoft.AspNetCore.Mvc;

namespace LeBonCoinAPI.Models.Repository
{
    public interface IRepositoryPhoto<TEntity>
    {
        ActionResult<IEnumerable<TEntity>> GetAll();
        ActionResult<TEntity> GetById(int id);
        ActionResult<TEntity> GetByIdProfil(int idProfil);
        ActionResult<TEntity> GetByIdAnnonce(int idAnnonce);
        void Add(TEntity entity);
        void Update(TEntity entityToUpdate, TEntity entity);
        void Delete(TEntity entity);
    }
}
