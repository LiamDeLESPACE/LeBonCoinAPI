using Microsoft.AspNetCore.Mvc;

namespace LeBonCoinAPI.Models.Repository
{
    public interface IRepositoryPossedeEquipement<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAll();
        Task<ActionResult<TEntity>> GetByIds(int idAnnonce, int idEquipement);
        Task<ActionResult<TEntity>> GetByIdAnnonce(int idAnnonce);
        Task<ActionResult<TEntity>> GetByIdEquipement(int idEquipement);
        Task Add(TEntity entity);
        Task Update(TEntity entityToUpdate, TEntity entity);
        Task Delete(TEntity entity);
    }
}
