using LeBonCoinAPI.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace LeBonCoinAPI.Models.Repository
{
    public interface IRepositoryDepartement<Tentity>
    {
        Task<ActionResult<IEnumerable<Tentity>>> GetAll();
        Task<ActionResult<Tentity>> GetByString(string str);
        Task Add(Tentity entity);
        Task Update(Tentity entityToUpdate, Tentity entity);
        Task Delete(Tentity entity);
    }
}
