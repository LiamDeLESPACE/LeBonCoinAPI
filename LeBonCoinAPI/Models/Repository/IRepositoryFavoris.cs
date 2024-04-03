using Microsoft.AspNetCore.Mvc;

namespace LeBonCoinAPI.Models.Repository
{
    public interface IRepositoryFavoris<TEntity>
    {
        ActionResult<IEnumerable<TEntity>> GetAll();
        ActionResult<TEntity> GetByIds(int idProfil, int idAnnonce);
        ActionResult<TEntity> GetByIdProfil(int idProfil);
        void Add(TEntity entity);
        void Update(TEntity entityToUpdate, TEntity entity);
        void Delete(TEntity entity);
    }
}
