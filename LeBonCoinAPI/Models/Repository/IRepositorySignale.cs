using Microsoft.AspNetCore.Mvc;

namespace LeBonCoinAPI.Models.Repository
{
    public interface IRepositorySignale<TEntity>
    {
        ActionResult<IEnumerable<TEntity>> GetAll();
        ActionResult<TEntity> GetByIds(int idAnnonce, int idProfil);
        ActionResult<TEntity> GetByIdProfil(int idProfil);
        ActionResult<TEntity> GetByIdAnnonce(int idAnnonce);
        void Add(TEntity entity);
        void Update(TEntity entityToUpdate, TEntity entity);
        void Delete(TEntity entity);
    }
}
