using Microsoft.AspNetCore.Mvc;

namespace LeBonCoinAPI.Models.Repository
{
    public interface IRepositorySignale<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAll();
        Task<ActionResult<TEntity>> GetByIds(int idAnnonce, int idProfil);
        Task<ActionResult<IEnumerable<TEntity>>> GetByIdProfil(int idProfil);
        Task<ActionResult<IEnumerable<TEntity>>> GetByIdAnnonce(int idAnnonce);
        Task Add(TEntity entity);
        Task Update(TEntity entityToUpdate, TEntity entity);
        Task Delete(TEntity entity);
    }
}
