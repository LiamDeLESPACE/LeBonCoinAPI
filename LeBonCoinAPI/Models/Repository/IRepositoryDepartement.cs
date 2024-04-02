using LeBonCoinAPI.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace LeBonCoinAPI.Models.Repository
{
    public interface IRepositoryDepartement<Tentity>
    {
        ActionResult<IEnumerable<Tentity>> GetAll();
        ActionResult<Tentity> GetByString(string str);
        void Add(Tentity entity);
        void Update(Tentity entityToUpdate, Tentity entity);
        void Delete(Tentity entity);
    }
}
