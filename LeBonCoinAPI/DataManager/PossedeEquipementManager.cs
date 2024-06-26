﻿using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class PossedeEquipementManager : IRepositoryPossedeEquipement<PossedeEquipement>
    {
        readonly DataContext? dataContext;
        public PossedeEquipementManager() { }
        public PossedeEquipementManager(DataContext context)
        {
            dataContext = context;
        }
        public async Task<ActionResult<IEnumerable<PossedeEquipement>>> GetAll()
        {
            return await dataContext.PossedeEquipements.ToListAsync();
        }

        public async Task<ActionResult<PossedeEquipement>> GetByIds(int idAnnonce, int idEquipement)
        {
            return await dataContext.PossedeEquipements.FirstOrDefaultAsync(c => c.AnnonceId == idAnnonce && c.EquipementId == idEquipement);
        }

        public async Task<ActionResult<IEnumerable<PossedeEquipement>>> GetByIdAnnonce(int idAnnonce)
        {
            return await (from f in dataContext.PossedeEquipements where f.AnnonceId == idAnnonce select f).ToListAsync();
        }
        public async Task<ActionResult<IEnumerable<PossedeEquipement>>> GetByIdEquipement(int idEquipement)
        {
            return await (from f in dataContext.PossedeEquipements where f.EquipementId == idEquipement select f).ToListAsync();
        }
        public async Task Add(PossedeEquipement entity)
        {
            await dataContext.PossedeEquipements.AddAsync(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(PossedeEquipement possedeEquipement, PossedeEquipement entity)
        {
            dataContext.Entry(possedeEquipement).State = EntityState.Modified;            

            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(PossedeEquipement possedeEquipement)
        {
            dataContext.PossedeEquipements.Remove(possedeEquipement);
            await dataContext.SaveChangesAsync();
        }
    }
}
